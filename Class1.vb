Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime
Imports Microsoft.Office.Interop
Imports Autodesk.AutoCAD.Windows
Imports System

Public Class ISOmetric
    <CommandMethod("AWSISO")> _
    Public Sub AddContextMenu()

        Dim ce As New ContextMenuExtension
        ce.Title = "AWSISO"
        Dim input As New MenuItem("输入数据(input)")
        AddHandler input.Click, New EventHandler(AddressOf input_click)
        Dim output As New MenuItem("输出到EXCEL（output)")
        AddHandler output.Click, New EventHandler(AddressOf output_click)
        Dim display As New MenuItem("插入管线说明(display)")
        AddHandler display.Click, New EventHandler(AddressOf display_click)
        ce.MenuItems.Add(input)
        ce.MenuItems.Add(output)
        ce.MenuItems.Add(display)
        Autodesk.AutoCAD.ApplicationServices.Application.AddDefaultContextMenuExtension(ce)
    End Sub

    Sub input_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        doc.SendStringToExecute("input ", True, False, True)
    End Sub

    Sub output_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        doc.SendStringToExecute("output ", True, False, True)
    End Sub

    Sub display_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        doc.SendStringToExecute("display ", True, False, True)
    End Sub


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

                        '如果直线没有字典则创建pipeAtt字典。
                        If line.ExtensionDictionary.IsNull Then
                            Dim inputUI As Form1 = New Form1
                            '显示输入界面
                            inputUI.ShowDialog()
                            '创建字典和pipeAtt纪录
                            writeAtt(line, inputUI.thisPipe)

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
                Dim str(7) As String
                For i As Integer = 0 To 7
                    str(i) = myXrecord.Data(i).value
                Next
                inputUI.thisPipe.items = str

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
            '如果直线没有字典，则创建字典
            If ent.ExtensionDictionary.IsNull Then
                ent.UpgradeOpen()
                ent.CreateExtensionDictionary()
            End If

            Dim dic As DBDictionary = trans.GetObject(ent.ExtensionDictionary, OpenMode.ForWrite)
            '创建pipeAtt纪录
            Dim myXrecord As New Xrecord
            myPipe.ID = ent.Handle.Value
            With myPipe
                Dim resBuffer As ResultBuffer = New ResultBuffer( _
                    New TypedValue(DxfCode.Handle, .ID), _
                    New TypedValue(DxfCode.Text, .DN), _
                    New TypedValue(DxfCode.Text, .material), _
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
            ed.WriteMessage(vbCrLf, "已经写入管道属性：")
            For Each value As TypedValue In myXrecord.Data
                If value.Value.ToString <> "" Then
                    ed.WriteMessage(value.Value.ToString() & ";")
                End If
            Next
            ed.WriteMessage(vbCrLf)
        End Using
    End Sub


    '定义过滤类型
    Public Enum filterType
        Line = 0
        Text = 1
        Circle = 2
    End Enum



    '输出图中信息到EXCEL模板中
    <CommandMethod("output")> _
    Public Sub extractInfo()
        Dim certainType() As filterType = {filterType.Line}
        Dim LineCollection As DBObjectCollection = getValidCollection(getSelection(certainType))
        export2Excel(LineCollection)
    End Sub

    '显示所有管道属性
    <CommandMethod("display")> _
    Public Sub showInfo()
        Dim certaintype() As filterType = {filterType.Line}
        Dim LineCollection As DBObjectCollection = getValidCollection(getSelection(certaintype))
        For Each l As Line In LineCollection
            createLabel(l)
        Next
    End Sub


    Function getSelection(ByVal tps As filterType()) As DBObjectCollection
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Dim entity As Entity = Nothing
        Dim entityCollection As New DBObjectCollection
        Dim selops As New PromptSelectionOptions()
        selops.MessageForAdding = "请选择统计范围"

        '建立选择的过滤器内容
        Dim filList(tps.Length + 1) As TypedValue
        filList(0) = New TypedValue(DxfCode.Operator, "<or")
        filList(tps.Length + 1) = New TypedValue(DxfCode.Operator, "or>")
        For i As Integer = 0 To tps.Length - 1
            filList(i + 1) = New TypedValue(DxfCode.Start, tps(i).ToString)
        Next

        '建立过滤器
        Dim filter As New SelectionFilter(filList)

        '按照过滤器进行选择
        Dim ents As PromptSelectionResult = ed.GetSelection(selops, filter)
        If ents.Status = PromptStatus.OK Then
            Using tr As Transaction = db.TransactionManager.StartTransaction
                Dim SS As SelectionSet = ents.Value
                For Each id As ObjectId In SS.GetObjectIds
                    entity = tr.GetObject(id, OpenMode.ForRead, False)
                    If entity <> Nothing Then
                        entityCollection.Add(entity)
                    End If
                Next
            End Using
        End If
        Return entityCollection
    End Function

    '返回含有pipeAtt纪录的实体
    Function getValidCollection(ByVal col As DBObjectCollection) As DBObjectCollection
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim validCollections As New DBObjectCollection
        Using tr As Transaction = db.TransactionManager.StartTransaction
            Dim dic As DBDictionary
            For Each e As Entity In col
                If Not (e.ExtensionDictionary.IsNull) Then
                    dic = tr.GetObject(e.ExtensionDictionary, OpenMode.ForRead)
                    If dic.Contains("pipeAtt") Then
                        validCollections.Add(e)
                    End If
                End If
            Next
        End Using
        Return validCollections
    End Function

    '输出信息至EXCEL表格
    Sub export2Excel(ByVal c As DBObjectCollection)
        Dim saveAs As New System.Windows.Forms.SaveFileDialog
        saveAs.Filter = "Excel 工作薄 (*.xlsx)|*.xlsx|All files (*.*)|*.* "

        Dim myExcel As New Excel.Application
        'TODO:补充找不到模板时的操作。
        Dim myWorkbook As Excel.Workbook = myExcel.Workbooks.Open("D:\ISO\管道管件表格式.xltx", , False)
        Dim myWorksheet As Excel.Worksheet = myWorkbook.Sheets(1)
        Dim ULCell As Excel.Range
        Dim DRCell As Excel.Range
        Const STARTROW As Integer = 7
        Const ENDCOL As Integer = 7
        Dim rowno As Integer = STARTROW
        Dim offSet As Integer = 0

        Dim myPipe As New pipeAtt
        For Each e As Line In c
            myPipe = readAtt(e)
            ULCell = myWorksheet.Cells(rowno, 1)
            DRCell = myWorksheet.Cells(rowno + offSet, ENDCOL - 1)
            For i As Integer = 0 To 5
                myWorksheet.Cells(rowno, i + 1) = myPipe.pipe(i)
            Next
            rowno = rowno + 1
            For i As Integer = 0 To 1
                If myPipe.endPoint(i, 1) <> "" Then
                    For j As Integer = 1 To ENDCOL - 1
                        myWorksheet.Cells(rowno, j) = myPipe.endPoint(i, j - 1)
                    Next
                    rowno = rowno + 1
                End If
            Next
        Next

        myExcel.Visible = True


        If saveAs.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            myWorkbook.SaveAs(saveAs.FileName)
        End If

    End Sub

    '在有属性的直线旁边显示管道属性
    Sub createLabel(ByVal l As Line)
        Dim db As Database = HostApplicationServices.WorkingDatabase

        If l.ExtensionDictionary.IsNull Then
            MsgBox("选择的直线无管道属性！", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Using tr As Transaction = db.TransactionManager.StartTransaction
            Dim dic As DBDictionary = tr.GetObject(l.ExtensionDictionary, OpenMode.ForRead)
            Dim myPipe As New pipeAtt
            If dic.Contains("pipeAtt") Then
                myPipe = readAtt(l)

            Else
                MsgBox("选择的直线无管道属性！", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            '生成管道标签文字
            Dim str1 As String = "#"
            For i As Integer = 0 To 5
                str1 = str1 + myPipe.pipe(i)
                If i < 4 Then str1 = str1 + ","
            Next
            '生成管件标签文字
            Dim str2 As String = ""
            For i = 0 To 1
                If myPipe.endPoint(i, 1) <> "" Then
                    str2 = str2 & vbCrLf & "#"
                    For j = 0 To 5
                        str2 = str2 + myPipe.endPoint(i, j)
                        If j < 4 Then str2 = str2 + ","
                    Next
                End If
            Next

            '创建MTEXT对象并添加
            Dim lLabel As New MText
            Const angleV1 As Double = Math.PI / 2
            Const angleV2 As Double = 3 * Math.PI / 2
            lLabel.TextHeight = 350
            lLabel.Width = 8000
            '判断文字方向
            If l.Angle > angleV1 And l.Angle < angleV2 Then
                lLabel.Location = l.EndPoint
                lLabel.Rotation = l.Angle - Math.PI
            Else
                lLabel.Location = l.StartPoint
                lLabel.Rotation = l.Angle
            End If

            lLabel.Contents = str1 & str2

            Dim entId As ObjectId
            Dim bt As BlockTable = tr.GetObject(db.BlockTableId, OpenMode.ForRead)
            Dim btr As BlockTableRecord = tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            entId = btr.AppendEntity(lLabel)
            tr.AddNewlyCreatedDBObject(lLabel, True)
            tr.Commit()
        End Using
    End Sub

    '读取图形中的纪录写入pipeAtt
    Function readAtt(ByVal ent As Line) As pipeAtt
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim myPipe As New pipeAtt
        '抛出异常
        If ent.ExtensionDictionary.IsNull Then
            MsgBox("图形不含对应属性错误!", MsgBoxStyle.Exclamation)
            Return myPipe
            Exit Function
        End If
        Using tr As Transaction = db.TransactionManager.StartTransaction
            Dim dic As DBDictionary = tr.GetObject(ent.ExtensionDictionary, OpenMode.ForRead)
            '抛出异常
            If Not (dic.Contains("pipeAtt")) Then
                MsgBox("图形不含对应属性错误!", MsgBoxStyle.Exclamation)
                Return myPipe
                Exit Function
            End If
            Dim entryID As ObjectId = dic.GetAt("pipeAtt")
            Dim myXrecord As Xrecord = tr.GetObject(entryID, OpenMode.ForRead)
            Dim str(7) As String
            For i As Integer = 0 To 7
                str(i) = myXrecord.Data(i).value
            Next
            myPipe.items = str
            Return myPipe
        End Using
    End Function
End Class
