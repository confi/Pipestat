Imports System.Windows.Forms


Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub


    'TODO:待数据库正常可用时，删除此处管道规格====================================
    Private steelPipe() As String = {"OD10", "DN15", "DN20", "DN25", "DN32", "DN40", "DN50", "DN65", "DN80", "DN100", "DN125", "DN150", "DN200", "DN250", "DN300", "DN350", "DN400"}
    Private PVCPipe() As String = {"De20", "De25", "De32", "De40", "De50", "De63", "De75", "De90", "De110", "De140", "De160", "De225", "De250", "De315"}
    '==============================================================================

    Public thisPipe As pipeAtt

    '将界面中输入的数值传递给结构数据，供Class1调用。
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        '确定管道材质
        thisPipe.material = pipeMaterial.Text

        '管道长度如果不是空值则赋值给结构体成员
        If pipeLength.Text = "" Then
            pipeLength.Text = "-"
            Exit Sub
        Else
            thisPipe.length = CType(pipeLength.Text, Double)
        End If

        '确定管道规格
        thisPipe.DN = pipeSpec.SelectedItem

        '===========================端头1===================================
        '确定端头1管件种类

        thisPipe.endPoint1Name = endPoint1Name.SelectedItem

        '确定端头管件规格
        thisPipe.endPoint1Spec = endPoint1Spec.SelectedItem

        '===========================端头2===================================
        '确定端头2管件种类
        thisPipe.endPoint2Name = endPoint2Name.SelectedItem

        '确定端头管件规格
        thisPipe.endPoint2Spec = endPoint2Spec.SelectedItem

        Me.Close()

    End Sub

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


    'TODO: 本段程序由于ACCESS数据库64位与32位引擎问题停滞。准备改用其它数据库。出于进度考虑，先使用常量代替。
    '链接数据库并填充相应的combobox
    Private Sub fillCombo(ByVal combobox As ComboBox, ByVal material As String, Optional ByVal endType As Integer = -1, Optional ByVal pipeSpec As Single = -1)


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

    '确定管道材质时填充规格combobox
    Private Sub pipeMaterial_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pipeMaterial.SelectedIndexChanged
        If pipeMaterial.SelectedIndex >= 0 Then
            
            pipeSpec.Items.Clear()
            pipeSpec.Enabled = True
            pipeSpec.ForeColor = System.Drawing.Color.Black
            Select Case pipeMaterial.SelectedIndex
                Case Is < 3
                    For i As Integer = 0 To steelPipe.Length - 1
                        pipeSpec.Items.Add(steelPipe(i))
                    Next
                Case Is > 2
                    For i As Integer = 0 To PVCPipe.Length - 1
                        pipeSpec.Items.Add(PVCPipe(i))
                    Next
            End Select
            pipeSpec.SelectedIndex = 0
        Else
            MsgBox("请选择管道材质!", MsgBoxStyle.Exclamation)
            pipeMaterial.Focus()
        End If
    End Sub

    '确定端头种类时填充端头规格
    Private Sub endPoint1Name_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles endPoint1Name.SelectedIndexChanged
        fillEndpointCombo(endPoint1Name, endPoint1Spec)
    End Sub
    '选中端头2与端头1相同时自动填充和选定相应选项
    Private Sub endPoint2Name_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles endPoint2Name.SelectedIndexChanged
        fillEndpointCombo(endPoint2Name, endPoint2Spec)
    End Sub

    '管道规格变化时，如已选择端头种类则更新端头规格
    Private Sub pipeSpec_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pipeSpec.SelectedIndexChanged
        If endPoint1Name.SelectedIndex >= 0 Then
            fillEndpointCombo(endPoint1Name, endPoint1Spec)
        End If

        If endPoint2Name.SelectedIndex >= 0 Then
            fillEndpointCombo(endPoint2Name, endPoint2Spec)
        End If
    End Sub


    '填充端头规格方法
    Private Sub fillEndpointCombo(ByVal endpointName As ComboBox, ByVal endpointSpec As ComboBox)

        If (endpointName.SelectedIndex >= 0 And pipeSpec.SelectedIndex >= 0) Then
            endpointSpec.Enabled = True
            endpointSpec.ForeColor = System.Drawing.Color.Black
            endpointSpec.Items.Clear()
            endpointSpec.Text = ""
            Select Case endpointName.SelectedIndex
                Case 0 To 9
                    '序号0-9的管件与管道规格一致

                    For i As Integer = 0 To steelPipe.Length - 1
                        endpointSpec.Items.Add(steelPipe(i))
                    Next
                    endpointSpec.SelectedItem = pipeSpec.SelectedItem

                Case 10 To 12
                    '序号10-13的为变径，需要2个规格组合
                    Dim index As Integer = pipeSpec.SelectedIndex
                    Select Case index
                        '规格组合规则：序号小于4的规格向下2个，向上4个。序号大于4的规格向上向下均为4个规格变化
                        Case 0 To 4

                            Dim mini As Integer
                            If index > 1 Then
                                mini = index - 2
                            Else
                                mini = 0
                            End If
                            Dim i As Integer
                            Dim str As String
                            For i = mini To index - 1
                                str = pipeSpec.Items(i)
                                endpointSpec.Items.Add(pipeSpec.Items(index) & ">" & str.Substring(2))
                            Next

                            For i = index + 1 To index + 4
                                str = pipeSpec.Items(index)
                                endpointSpec.Items.Add(pipeSpec.Items(i) & ">" & str.Substring(2))
                            Next
                        Case Is > 4
                            Dim max As Integer
                            If (index + 4) > (pipeSpec.Items.Count - 1) Then
                                max = pipeSpec.Items.Count - 1
                            Else
                                max = index + 4
                            End If
                            Dim i As Integer
                            Dim str As String
                            For i = index - 4 To index - 1
                                str = pipeSpec.Items(i)
                                endpointSpec.Items.Add(pipeSpec.Items(index) & ">" & str.Substring(2))
                            Next

                            For i = index + 1 To max
                                str = pipeSpec.Items(index)
                                endpointSpec.Items.Add(pipeSpec.Items(i) & ">" & str.Substring(2))
                            Next

                    End Select
                Case Is > 12
                    endpointName.SelectedIndex = -1
                    endpointSpec.Items.Clear()
            End Select
        Else
            MsgBox("请选择端头种类及管道规格！", MsgBoxStyle.Exclamation)
            endpointName.Focus()
        End If
    End Sub

    'sameWith1选中时的动作
    Private Sub sameWith1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sameWith1.CheckedChanged
        If sameWith1.Checked = True Then
            endPoint2Name.Text = endPoint1Name.Text
            endPoint2Name.SelectedIndex = endPoint1Name.SelectedIndex
            endPoint2Spec.Text = endPoint2Spec.Text
            endPoint2Spec.SelectedIndex = endPoint1Spec.SelectedIndex
            endPoint2Name.Enabled = False
            endPoint2Spec.Enabled = False
        Else
            endPoint2Name.Enabled = True
            endPoint2Spec.Enabled = True
        End If
    End Sub


    '公共方法显示图形中的数据
    Public Sub display(ByVal myPipe As pipeAtt)
        '显示材质
        refillColumns(myPipe.material, pipeMaterial)
        '显示管道规格
        refillColumns(myPipe.DN, pipeSpec)
        '显示管道长度
        pipeLength.Text = myPipe.length.ToString
        '显示端头1种类和规格
        refillColumns(myPipe.endPoint1Name, endPoint1Name)
        refillColumns(myPipe.endPoint1Spec, endPoint1Spec)
        '显示端头2种类和规格
        refillColumns(myPipe.endPoint2Name, endPoint2Name)
        refillColumns(myPipe.endPoint2Spec, endPoint2Spec)
        Me.ShowDialog()
    End Sub

    Sub refillColumns(ByVal x As String, ByVal c As ComboBox)
        For Each m As String In c.Items
            If m = x Then
                c.SelectedItem = m
            End If
        Next
    End Sub
    
End Class