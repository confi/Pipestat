Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime


Public Class ISOmetric



    <CommandMethod("input")> _
    Public Sub inputInformation()
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Dim i As Boolean = True

        Do While i
            Dim lineOption As PromptEntityOptions = New PromptEntityOptions(ControlChars.Lf & "请选择需要添加信息的直线")
            Dim lineResult As PromptEntityResult = ed.GetEntity(lineOption)

            '如果选择成功，则进行添加信息处理
            If (lineResult.Status = PromptStatus.OK) Then
                Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
                Using trans As Transaction = db.TransactionManager.StartTransaction
                    Dim ent As Object = trans.GetObject(lineResult.ObjectId, OpenMode.ForRead)
                    '判断选择的图形是否为直线
                    If (ent.GetType.Name = "Line") Then
                        Dim line As Line = CType(ent, Line)

                        '如果直线没有字典则显示输入界面并写入（创建)属性
                        If line.ExtensionDictionary.IsNull Then
                            Dim inputUI As Form1 = New Form1
                            '显示输入界面
                            inputUI.ShowDialog()
                            '创建字典
                            writeAtt(line, inputUI.thisPipe)
                            'TODO:显示直线属性
                        Else
                            readModifyAtt(line)
                        End If
                    Else

                    End If
                    trans.Commit()
                End Using
            Else
                i = False
            End If
        Loop

    End Sub

    '读取直线字典并作修改
    Sub readModifyAtt(ByVal ent As Line)
        Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Using trans As Transaction = db.TransactionManager.StartTransaction
            
            Dim dic As DBDictionary = trans.GetObject(ent.ExtensionDictionary, OpenMode.ForRead)
            Dim inputUI As Form1 = New Form1

            If dic.Contains("pipeAtt") Then
                '读取管道属性
                Dim entryId As ObjectId = dic.GetAt("pipeAtt")

                ed.WriteMessage(ControlChars.Lf & "该直线已经设置属性，请核对或修改！" & vbCrLf)
                Dim myXrecord As Xrecord
                myXrecord = trans.GetObject(entryId, OpenMode.ForRead)

                '显示属性
                With inputUI.thisPipe
                    .material = myXrecord.Data(0).value
                    .DN = myXrecord.Data(1).value
                    .length = myXrecord.Data(2).value
                    .endPoint1Name = myXrecord.Data(3).value
                    .endPoint1Spec = myXrecord.Data(4).value
                    .endPoint2Name = myXrecord.Data(5).value
                    .endPoint2Spec = myXrecord.Data(6).value
                End With
                inputUI.display(inputUI.thisPipe)
                writeAtt(ent, inputUI.thisPipe)
            Else

                '显示输入界面
                inputUI.ShowDialog()
                '设置管道属性
                writeAtt(ent, inputUI.thisPipe)

            End If
            trans.Commit()
        End Using

    End Sub

    '写入数据到直线字典
    Sub writeAtt(ByVal ent As Line, ByVal myPipe As pipeAtt)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Using trans As Transaction = db.TransactionManager.StartTransaction

            If ent.ExtensionDictionary.IsNull Then
                ent.UpgradeOpen()
                ent.CreateExtensionDictionary()
            End If
            Dim dic As DBDictionary = trans.GetObject(ent.ExtensionDictionary, OpenMode.ForWrite)
            Dim myXrecord As New Xrecord
            With myPipe
                Dim resBuffer As ResultBuffer = New ResultBuffer( _
                    New TypedValue(DxfCode.Text, .material), _
                    New TypedValue(DxfCode.Text, .DN), _
                    New TypedValue(DxfCode.Real, .length), _
                    New TypedValue(DxfCode.Text, .endPoint1Name), _
                    New TypedValue(DxfCode.Text, .endPoint1Spec), _
                    New TypedValue(DxfCode.Text, .endPoint2Name), _
                    New TypedValue(DxfCode.Text, .endPoint2Spec))
                myXrecord.Data = resBuffer
                dic.SetAt("pipeAtt", myXrecord)
            End With
            trans.AddNewlyCreatedDBObject(myXrecord, True)
            trans.Commit()
            ed.WriteMessage("已经写入管道属性：")
            For Each value As TypedValue In myXrecord.Data
                ed.WriteMessage(value.Value.ToString() & ",")
            Next
            ed.WriteMessage(vbCrLf)
        End Using
    End Sub

    <CommandMethod("output")> _
    Sub extractInfo()
       

    End Sub
End Class
