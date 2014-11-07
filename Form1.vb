Imports System.Windows.Forms


Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub


    'TODO:待数据库正常可用时，删除此处管道规格====================================
    Private steelPipe() As String = {"DN15", "DN20", "DN32", "DN40", "DN50", "DN65", "DN80", "DN100", "DN125", "DN150", "DN200", "DN250", "DN300", "DN350", "DN400"}
    Private PVCPipe() As String = {"De20", "De25", "De32", "De40", "De50", "De63", "De75", "De90", "De110", "De140", "De160", "De225", "De250", "De315"}
    '==============================================================================

    Public thisPipe As pipeAtt

    '将界面中输入的数值传递给结构数据，供Class1调用。
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click

        '管道长度如果不是空值则赋值给结构体成员
        If pipeLength.Text = "" Then
            pipeLength.Text = "-"
            Exit Sub
        Else
            thisPipe.length = CType(pipeLength.Text, Double)
        End If

        '链接数据库

        '确定管道规格
        thisPipe.DN = pipeSpec.SelectedItem


        '===========================端头1===================================
        '确定端头1管件种类

        thisPipe.endPoint1Name = evaluateStruct(GBEnd1)


        '链接管件数据库

        '确定端头管件规格
        thisPipe.endPoint1Spec = endPoint1Spec.SelectedItem

        '===========================端头2===================================
        '确定端头2管件种类
        thisPipe.endPoint2Name = evaluateStruct(GBEnd2)

        '链接管件数据库

        '确定端头管件规格
        thisPipe.endPoint2Spec = endPoint2Spec.SelectedItem



        Me.Close()

    End Sub

    '把界面上radiobutton控制的值赋给结构变量
    Private Function evaluateStruct(ByVal group As GroupBox) As String
        Dim str As String = ""
        For Each elem As RadioButton In group.Controls
            If elem.Checked Then
                str = elem.Text
                Exit For
            End If
        Next
        evaluateStruct = str
    End Function

    '判断管道长度值是否有效

    Private Sub pipeLength_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pipeLength.TextChanged
        Dim length As String = pipeLength.Text

        For Each x As Char In length
            Dim n As Integer = Asc(x)
            If (n < 46 Or n > 57) And n <> 47 Then
                MsgBox("管道长度应为数字，不能为空值", MsgBoxStyle.Critical)
                BtnOK.Enabled = False
                pipeLength.SelectAll()
                Exit For
            Else
                BtnOK.Enabled = True
            End If
        Next
    End Sub



    '链接数据库并填充相应的combobox
    Private Sub fillCombo(ByVal combobox As ComboBox, ByVal material As String, Optional ByVal endType As Integer = -1, Optional ByVal pipeSpec As Single = -1)

        'TODO: 本段程序由于ACCESS数据库64位与32位引擎问题停滞。准备改用其它数据库。出于进度考虑，先使用常量代替。
        'Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =E:\Study_Research\001 卓信软件建设\轴测图统计工具\pipeSpec.accdb; Persist Security Info=False "
        'Using objConnection As OleDb.OleDbConnection = New OleDb.OleDbConnection(connStr)
        '    objConnection.Open()
        '    Dim myCommand1 As OleDb.OleDbCommand = New OleDb.OleDbCommand
        '    myCommand1.Connection = objConnection
        '    myCommand1.CommandText = " SELECT   管子规格表.ID, 管子规格表.公称直径, 管子规格表.外径, 管子规格表.壁厚, 管子规格表.材质ID FROM   (管子规格表 INNER JOIN 材质表 ON 管子规格表.材质ID = 材质表.ID) "
        '    Dim myAdapter As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(" SELECT   管子规格表.ID, 管子规格表.公称直径, 管子规格表.外径, 管子规格表.壁厚, 管子规格表.材质ID FROM   (管子规格表 INNER JOIN 材质表 ON 管子规格表.材质ID = 材质表.ID) ", connStr)
        '    Dim ds As DataSet = New DataSet()
        '    myAdapter.Fill(ds, "管道规格表")
        '    Dim dt As DataTable = ds.Tables("管道规格表")
        '    pipeSpec.DataBindings.Add("Text", dt("公称直径"), "ID")

        'End Using

        Select Case material
            Case "管道材质"

            Case Else

        End Select
    End Sub


    '准备选择管道规格时填充规格combobox

    Private Sub pipeSpec_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles pipeSpec.GotFocus, pipeMaterial.GotFocus, endPoint1Name.GotFocus, endPoint2Name.GotFocus

        '确定管道材质，默认为304不锈钢
        thisPipe.material = pipeMaterial.Text

        Select Case pipeMaterial.SelectedIndex
            Case Is <= 3
                For i As Integer = 0 To steelPipe.Length - 1
                    pipeSpec.Items(i + 1) = steelPipe(i)
                Next
            Case Is > 3
                For i As Integer = 0 To steelPipe.Length - 1
                    pipeSpec.Items(i + 1) = PVCPipe(i)
                Next
        End Select


    End Sub

    
    Private Sub endPoint1Spec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If pipeMaterial.SelectedIndex > 0 Then
            endPoint1Spec.Enabled = True
            endPoint1Spec.ForeColor = Drawing.Color.Black
        Else
            MsgBox("请选择管道规格!", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub pipeSpec_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pipeSpec.MouseClick
        If pipeMaterial.SelectedIndex > 0 Then
            pipeSpec.Enabled = True
            pipeSpec.ForeColor = Drawing.Color.Black
        Else
            MsgBox("请选择管道材质!", MsgBoxStyle.Exclamation)
        End If
    End Sub
End Class