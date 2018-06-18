<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmretsumm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbrepttyp = New System.Windows.Forms.ComboBox()
        Me.grids = New System.Windows.Forms.DataGridView()
        Me.grid = New System.Windows.Forms.DataGridView()
        Me.gridr = New System.Windows.Forms.DataGridView()
        Me.gridb = New System.Windows.Forms.DataGridView()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdReport = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Fraretndetail = New System.Windows.Forms.GroupBox()
        Me.cmbCNM = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Fraretnsumm = New System.Windows.Forms.GroupBox()
        Me.CmbCoName = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fracompdetail = New System.Windows.Forms.GroupBox()
        CType(Me.grids, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Fraretndetail.SuspendLayout()
        Me.Fraretnsumm.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Report Type"
        '
        'cmbrepttyp
        '
        Me.cmbrepttyp.FormattingEnabled = True
        Me.cmbrepttyp.Location = New System.Drawing.Point(84, 6)
        Me.cmbrepttyp.Name = "cmbrepttyp"
        Me.cmbrepttyp.Size = New System.Drawing.Size(177, 21)
        Me.cmbrepttyp.TabIndex = 1
        '
        'grids
        '
        Me.grids.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grids.Location = New System.Drawing.Point(284, 12)
        Me.grids.Name = "grids"
        Me.grids.Size = New System.Drawing.Size(418, 336)
        Me.grids.TabIndex = 2
        '
        'grid
        '
        Me.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid.Location = New System.Drawing.Point(17, 78)
        Me.grid.Name = "grid"
        Me.grid.Size = New System.Drawing.Size(249, 270)
        Me.grid.TabIndex = 3
        '
        'gridr
        '
        Me.gridr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridr.Location = New System.Drawing.Point(17, 383)
        Me.gridr.Name = "gridr"
        Me.gridr.Size = New System.Drawing.Size(118, 40)
        Me.gridr.TabIndex = 4
        Me.gridr.Visible = False
        '
        'gridb
        '
        Me.gridb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridb.Location = New System.Drawing.Point(284, 22)
        Me.gridb.Name = "gridb"
        Me.gridb.Size = New System.Drawing.Size(418, 339)
        Me.gridb.TabIndex = 5
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(97, 383)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(75, 23)
        Me.cmdClear.TabIndex = 6
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(216, 383)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 7
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdReport
        '
        Me.cmdReport.Location = New System.Drawing.Point(327, 383)
        Me.cmdReport.Name = "cmdReport"
        Me.cmdReport.Size = New System.Drawing.Size(75, 23)
        Me.cmdReport.TabIndex = 8
        Me.cmdReport.Text = "Report"
        Me.cmdReport.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Select Head To Export"
        '
        'Fraretndetail
        '
        Me.Fraretndetail.Controls.Add(Me.cmbCNM)
        Me.Fraretndetail.Controls.Add(Me.Label3)
        Me.Fraretndetail.Location = New System.Drawing.Point(9, 118)
        Me.Fraretndetail.Name = "Fraretndetail"
        Me.Fraretndetail.Size = New System.Drawing.Size(252, 55)
        Me.Fraretndetail.TabIndex = 10
        Me.Fraretndetail.TabStop = False
        Me.Fraretndetail.Text = "Return Detail"
        '
        'cmbCNM
        '
        Me.cmbCNM.FormattingEnabled = True
        Me.cmbCNM.Location = New System.Drawing.Point(88, 29)
        Me.cmbCNM.Name = "cmbCNM"
        Me.cmbCNM.Size = New System.Drawing.Size(158, 21)
        Me.cmbCNM.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Company Name"
        '
        'Timer1
        '
        '
        'Timer2
        '
        '
        'Fraretnsumm
        '
        Me.Fraretnsumm.Controls.Add(Me.CmbCoName)
        Me.Fraretnsumm.Controls.Add(Me.Label4)
        Me.Fraretnsumm.Location = New System.Drawing.Point(9, 75)
        Me.Fraretnsumm.Name = "Fraretnsumm"
        Me.Fraretnsumm.Size = New System.Drawing.Size(252, 39)
        Me.Fraretnsumm.TabIndex = 11
        Me.Fraretnsumm.TabStop = False
        Me.Fraretnsumm.Text = "Return Summary"
        '
        'CmbCoName
        '
        Me.CmbCoName.FormattingEnabled = True
        Me.CmbCoName.Location = New System.Drawing.Point(88, 16)
        Me.CmbCoName.Name = "CmbCoName"
        Me.CmbCoName.Size = New System.Drawing.Size(158, 21)
        Me.CmbCoName.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Company Name"
        '
        'fracompdetail
        '
        Me.fracompdetail.Location = New System.Drawing.Point(9, 52)
        Me.fracompdetail.Name = "fracompdetail"
        Me.fracompdetail.Size = New System.Drawing.Size(252, 20)
        Me.fracompdetail.TabIndex = 12
        Me.fracompdetail.TabStop = False
        Me.fracompdetail.Text = "Company Detail"
        '
        'frmretsumm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 413)
        Me.Controls.Add(Me.grid)
        Me.Controls.Add(Me.Fraretnsumm)
        Me.Controls.Add(Me.fracompdetail)
        Me.Controls.Add(Me.Fraretndetail)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdReport)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.gridb)
        Me.Controls.Add(Me.gridr)
        Me.Controls.Add(Me.grids)
        Me.Controls.Add(Me.cmbrepttyp)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmretsumm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Return Summary"
        CType(Me.grids, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Fraretndetail.ResumeLayout(False)
        Me.Fraretndetail.PerformLayout()
        Me.Fraretnsumm.ResumeLayout(False)
        Me.Fraretnsumm.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbrepttyp As System.Windows.Forms.ComboBox
    Friend WithEvents grids As DataGridView
    Friend WithEvents grid As DataGridView
    Friend WithEvents gridr As DataGridView
    Friend WithEvents gridb As DataGridView
    Friend WithEvents cmdClear As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents cmdReport As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Fraretndetail As GroupBox
    Friend WithEvents cmbCNM As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Fraretnsumm As GroupBox
    Friend WithEvents CmbCoName As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents fracompdetail As GroupBox
End Class
