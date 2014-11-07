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
            Dim lineOption As PromptEntityOptions = New PromptEntityOptions(ControlChars.Lf & "请选择需要添加信息的直线：")
            Dim lineResult As PromptEntityResult = ed.GetEntity(lineOption)


            '如果选择成功，则进行添加信息处理
            If (lineResult.Status = PromptStatus.OK) Then
                Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
                Using trans As Transaction = db.TransactionManager.StartTransaction
                    Dim ent As Object = trans.GetObject(lineResult.ObjectId, OpenMode.ForRead)
                    '判断选择的图形是否为直线
                    If (ent.GetType.Name = "Line") Then
                        Dim line As Line = CType(ent, Line)

                        '显示输入界面
                        Dim inputUI As Form1 = New Form1
                        inputUI.ShowDialog()

                        '创建字典并显示在直线旁边
                        createDic(line, inputUI.thisPipe)
                    Else

                    End If
                    trans.Commit()
                End Using
            Else
                i = False
            End If
        Loop

    End Sub

    Sub createDic(ByRef ent As Line, ByVal data As Form1.pipeAtt)
        Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Using trans As Transaction = db.TransactionManager.StartTransaction
            If ent.ExtensionDictionary.IsNull Then
                ent.UpgradeOpen()
                ent.CreateExtensionDictionary()
            End If
            Dim dic As DBDictionary = trans.GetObject(ent.ExtensionDictionary, OpenMode.ForRead)

            If dic.Contains("pipeAtt") Then
                '读取管道属性
                Dim entryId As ObjectId = dic.GetAt("pipeAtt")
                ed.WriteMessage(ControlChars.Lf & "该直线已经设置属性...")
                Dim myXrecord As Xrecord
                myXrecord = trans.GetObject(entryId, OpenMode.ForRead)


                'TODO：此处添加比较属性以及更改确认
                For Each value As TypedValue In myXrecord.Data
                    ed.WriteMessage(ControlChars.Lf & value.TypeCode.ToString() _
                                    & "." & value.Value.ToString())
                Next

            Else
                '设置管道属性
                dic.UpgradeOpen()
                Dim myXrecord As New Xrecord
                With data

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
                For Each value As TypedValue In myXrecord.Data
                    ed.WriteMessage(ControlChars.Lf & value.TypeCode.ToString() _
                                    & "." & value.Value.ToString())
                Next
            End If

        End Using
    End Sub

End Class
