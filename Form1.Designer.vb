<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.pipeSpec = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pipeLength = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.endPoint1Spec = New System.Windows.Forms.ComboBox
        Me.BtnOK = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.pipeMaterial = New System.Windows.Forms.ComboBox
        Me.endPoint1Name = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.sameWith1 = New System.Windows.Forms.CheckBox
        Me.endPoint2Name = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.endPoint2Spec = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'pipeSpec
        '
        Me.pipeSpec.Enabled = False
        Me.pipeSpec.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.pipeSpec.FormattingEnabled = True
        Me.pipeSpec.Location = New System.Drawing.Point(87, 58)
        Me.pipeSpec.Name = "pipeSpec"
        Me.pipeSpec.Size = New System.Drawing.Size(121, 20)
        Me.pipeSpec.TabIndex = 2
        Me.pipeSpec.Tag = ""
        Me.pipeSpec.Text = "请先选择管道材质"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "管道规格"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "管道长度(米)"
        '
        'pipeLength
        '
        Me.pipeLength.Location = New System.Drawing.Point(87, 94)
        Me.pipeLength.Name = "pipeLength"
        Me.pipeLength.Size = New System.Drawing.Size(121, 21)
        Me.pipeLength.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 171)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "端头1规格："
        '
        'endPoint1Spec
        '
        Me.endPoint1Spec.Enabled = False
        Me.endPoint1Spec.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.endPoint1Spec.FormattingEnabled = True
        Me.endPoint1Spec.Items.AddRange(New Object() {"DN15", "DN20", "DN32", "DN40", "DN65"})
        Me.endPoint1Spec.Location = New System.Drawing.Point(87, 167)
        Me.endPoint1Spec.Name = "endPoint1Spec"
        Me.endPoint1Spec.Size = New System.Drawing.Size(121, 20)
        Me.endPoint1Spec.TabIndex = 5
        Me.endPoint1Spec.Text = "请先选择端头种类"
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(79, 307)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 25)
        Me.BtnOK.TabIndex = 9
        Me.BtnOK.Text = "完成"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "管道材质"
        '
        'pipeMaterial
        '
        Me.pipeMaterial.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.pipeMaterial.FormattingEnabled = True
        Me.pipeMaterial.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.pipeMaterial.Items.AddRange(New Object() {"SS304", "SS316L", "镀锌碳钢", "PVC", "PP"})
        Me.pipeMaterial.Location = New System.Drawing.Point(87, 22)
        Me.pipeMaterial.Name = "pipeMaterial"
        Me.pipeMaterial.Size = New System.Drawing.Size(121, 20)
        Me.pipeMaterial.TabIndex = 1
        Me.pipeMaterial.Tag = ""
        '
        'endPoint1Name
        '
        Me.endPoint1Name.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.endPoint1Name.FormattingEnabled = True
        Me.endPoint1Name.Items.AddRange(New Object() {"90度弯头", "45度弯头", "等径三通", "外螺纹", "内螺纹", "内牙活接", "对焊活接", "法兰", "管帽", "直通", "变径", "异径三通", "补芯"})
        Me.endPoint1Name.Location = New System.Drawing.Point(87, 131)
        Me.endPoint1Name.Name = "endPoint1Name"
        Me.endPoint1Name.Size = New System.Drawing.Size(121, 20)
        Me.endPoint1Name.TabIndex = 4
        Me.endPoint1Name.Tag = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "端头1种类"
        '
        'sameWith1
        '
        Me.sameWith1.AutoSize = True
        Me.sameWith1.Location = New System.Drawing.Point(8, 203)
        Me.sameWith1.Name = "sameWith1"
        Me.sameWith1.Size = New System.Drawing.Size(144, 16)
        Me.sameWith1.TabIndex = 6
        Me.sameWith1.Text = "端头2与端头1相同配置"
        Me.sameWith1.UseVisualStyleBackColor = True
        '
        'endPoint2Name
        '
        Me.endPoint2Name.ForeColor = System.Drawing.SystemColors.WindowText
        Me.endPoint2Name.FormattingEnabled = True
        Me.endPoint2Name.Items.AddRange(New Object() {"90度弯头", "45度弯头", "等径三通", "外螺纹", "内螺纹", "内牙活接", "对焊活接", "法兰", "管帽", "直通", "变径", "异径三通", "补芯"})
        Me.endPoint2Name.Location = New System.Drawing.Point(85, 235)
        Me.endPoint2Name.Name = "endPoint2Name"
        Me.endPoint2Name.Size = New System.Drawing.Size(121, 20)
        Me.endPoint2Name.TabIndex = 7
        Me.endPoint2Name.Tag = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 239)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 12)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "端头2种类"
        '
        'endPoint2Spec
        '
        Me.endPoint2Spec.Enabled = False
        Me.endPoint2Spec.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.endPoint2Spec.FormattingEnabled = True
        Me.endPoint2Spec.Items.AddRange(New Object() {"DN15", "DN20", "DN32", "DN40", "DN65"})
        Me.endPoint2Spec.Location = New System.Drawing.Point(85, 271)
        Me.endPoint2Spec.Name = "endPoint2Spec"
        Me.endPoint2Spec.Size = New System.Drawing.Size(121, 20)
        Me.endPoint2Spec.TabIndex = 8
        Me.endPoint2Spec.Text = "请先选择端头种类"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 275)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 12)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "端头2规格："
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(230, 351)
        Me.Controls.Add(Me.sameWith1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.pipeLength)
        Me.Controls.Add(Me.endPoint2Spec)
        Me.Controls.Add(Me.endPoint1Spec)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.endPoint2Name)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.endPoint1Name)
        Me.Controls.Add(Me.pipeMaterial)
        Me.Controls.Add(Me.pipeSpec)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "输入管道信息"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pipeSpec As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pipeLength As System.Windows.Forms.TextBox
    Friend WithEvents endPoint1Spec As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pipeMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents endPoint1Name As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents sameWith1 As System.Windows.Forms.CheckBox
    Friend WithEvents endPoint2Name As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents endPoint2Spec As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
