﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbQuarter = New System.Windows.Forms.ComboBox()
        Me.cmdc = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbdest = New System.Windows.Forms.ComboBox()
        Me.cmbCNm = New System.Windows.Forms.ComboBox()
        Me.cmbtyp = New System.Windows.Forms.ComboBox()
        Me.chksalary = New System.Windows.Forms.CheckBox()
        Me.chknilrec = New System.Windows.Forms.CheckBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdgen = New System.Windows.Forms.Button()
        Me.chkpan = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Lavender
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Controls.Add(Me.chksalary)
        Me.GroupBox1.Controls.Add(Me.chknilrec)
        Me.GroupBox1.Controls.Add(Me.cmdexit)
        Me.GroupBox1.Controls.Add(Me.cmdgen)
        Me.GroupBox1.Controls.Add(Me.chkpan)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(1, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(394, 297)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Deductee's Name List"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(296, 184)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(92, 74)
        Me.DataGridView2.TabIndex = 25
        Me.DataGridView2.Visible = False
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 217)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(93, 42)
        Me.DataGridView1.TabIndex = 24
        Me.DataGridView1.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.49351!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.50649!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbdest, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbCNm, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbtyp, 1, 2)
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(6, 21)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.54237!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.45763!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(376, 124)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmbQuarter)
        Me.Panel1.Controls.Add(Me.cmdc)
        Me.Panel1.Location = New System.Drawing.Point(124, 96)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(236, 22)
        Me.Panel1.TabIndex = 8
        '
        'cmbQuarter
        '
        Me.cmbQuarter.CausesValidation = False
        Me.cmbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQuarter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQuarter.FormattingEnabled = True
        Me.cmbQuarter.Location = New System.Drawing.Point(0, 0)
        Me.cmbQuarter.MaxLength = 20
        Me.cmbQuarter.Name = "cmbQuarter"
        Me.cmbQuarter.Size = New System.Drawing.Size(210, 21)
        Me.cmbQuarter.TabIndex = 0
        '
        'cmdc
        '
        Me.cmdc.BackgroundImage = Global.WizinTDS2019.My.Resources.Resources._37645
        Me.cmdc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdc.Location = New System.Drawing.Point(209, 0)
        Me.cmdc.Name = "cmdc"
        Me.cmdc.Size = New System.Drawing.Size(28, 22)
        Me.cmdc.TabIndex = 11
        Me.cmdc.TabStop = False
        Me.cmdc.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Lavender
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(6, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Destination"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Lavender
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(6, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Company Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Lavender
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(6, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Form Type"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Lavender
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(6, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Select Quarter"
        '
        'cmbdest
        '
        Me.cmbdest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdest.Location = New System.Drawing.Point(124, 6)
        Me.cmbdest.Name = "cmbdest"
        Me.cmbdest.Size = New System.Drawing.Size(236, 21)
        Me.cmbdest.TabIndex = 0
        '
        'cmbCNm
        '
        Me.cmbCNm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCNm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCNm.FormattingEnabled = True
        Me.cmbCNm.Location = New System.Drawing.Point(124, 37)
        Me.cmbCNm.Name = "cmbCNm"
        Me.cmbCNm.Size = New System.Drawing.Size(236, 21)
        Me.cmbCNm.TabIndex = 1
        '
        'cmbtyp
        '
        Me.cmbtyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtyp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtyp.FormattingEnabled = True
        Me.cmbtyp.Location = New System.Drawing.Point(124, 65)
        Me.cmbtyp.Name = "cmbtyp"
        Me.cmbtyp.Size = New System.Drawing.Size(236, 21)
        Me.cmbtyp.TabIndex = 2
        '
        'chksalary
        '
        Me.chksalary.AutoSize = True
        Me.chksalary.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chksalary.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chksalary.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chksalary.Location = New System.Drawing.Point(194, 145)
        Me.chksalary.Name = "chksalary"
        Me.chksalary.Size = New System.Drawing.Size(167, 34)
        Me.chksalary.TabIndex = 23
        Me.chksalary.TabStop = False
        Me.chksalary.Text = "Only Salary Deductee " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "List"
        Me.chksalary.UseVisualStyleBackColor = True
        Me.chksalary.Visible = False
        '
        'chknilrec
        '
        Me.chknilrec.AutoSize = True
        Me.chknilrec.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chknilrec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chknilrec.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chknilrec.Location = New System.Drawing.Point(6, 184)
        Me.chknilrec.Name = "chknilrec"
        Me.chknilrec.Size = New System.Drawing.Size(179, 19)
        Me.chknilrec.TabIndex = 22
        Me.chknilrec.TabStop = False
        Me.chknilrec.Text = " No Entry in Transaction"
        Me.chknilrec.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Image = Global.WizinTDS2019.My.Resources.Resources.RT_EXIT1
        Me.cmdexit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdexit.Location = New System.Drawing.Point(217, 231)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(73, 60)
        Me.cmdexit.TabIndex = 21
        Me.cmdexit.TabStop = False
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdgen
        '
        Me.cmdgen.Image = Global.WizinTDS2019.My.Resources.Resources.icon_technology
        Me.cmdgen.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdgen.Location = New System.Drawing.Point(105, 231)
        Me.cmdgen.Name = "cmdgen"
        Me.cmdgen.Size = New System.Drawing.Size(83, 60)
        Me.cmdgen.TabIndex = 4
        Me.cmdgen.Text = "Generate"
        Me.cmdgen.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdgen.UseVisualStyleBackColor = True
        '
        'chkpan
        '
        Me.chkpan.AutoSize = True
        Me.chkpan.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkpan.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkpan.Location = New System.Drawing.Point(6, 146)
        Me.chkpan.Name = "chkpan"
        Me.chkpan.Size = New System.Drawing.Size(182, 34)
        Me.chkpan.TabIndex = 3
        Me.chkpan.TabStop = False
        Me.chkpan.Text = "Show Only PAN Missing " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Deductees"
        Me.chkpan.UseVisualStyleBackColor = True
        '
        'frmDeList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(435, 311)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmDeList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Deductee's Name List"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdgen As System.Windows.Forms.Button
    Friend WithEvents chkpan As System.Windows.Forms.CheckBox
    Friend WithEvents cmdc As System.Windows.Forms.Button
    Friend WithEvents cmbtyp As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbQuarter As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbdest As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCNm As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chksalary As System.Windows.Forms.CheckBox
    Friend WithEvents chknilrec As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView2 As DataGridView
End Class
