Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.IO

Public Class FRM16Detail
    Dim oCoObj As New clsCoMst
    Dim WithEvents oForm16 As New clsForm16Details
    Dim WithEvents oForm16More As New clsForm16MoreDetails
    Dim WithEvents oF16Challan As New clsF16Challan
    'Dim oAllowances As New Collection_Allowances
    'Dim oOthIncomes As New Collection_OtherIncomes
    'Dim oSec80CDeductions As New Collection_Sec80CDed
    'Dim oChapter6ADeductions As New Collection_VI_A_Deductions
    'Dim oSec80CCFDeductions As New Collection_Sec80CCFDed
    'Dim oSec80CCGDeductions As New Collection_Sec80CCGDed
    Public xMode As String
    Dim taxcal As Boolean
    Dim transaction As OleDb.OleDbTransaction


    Dim frm As New frmdeduteeTDSMST

    Dim AllowCboTxt As String, Sec80CcgCboTxt, OthIncCbotxt As String ' StlvwChallan.Items(0).SubItems(1).Textring, Sec80CcgCboTxt As String
    Dim Sec80CCboTxt As String, Sec80CcfCboTxt As String, Chp6aCboTxt As String, DataRead
    Private Sub frm16Details_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        frmCoMst.EnterTab(e)
    End Sub

    Private Sub cmbName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo16DedName.Enter
        cbo16DedName.BackColor = Color.LightYellow
    End Sub

    Private Sub cbo16DedName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo16DedName.Leave
        cbo16DedName.BackColor = Color.White
        fill()
        'If cbo16DedName.Text = vbNullString Then Exit Sub
        'Dim nds As New DataSet
        'Dim i As Integer, DName As String
        'DName = UCase(cbo16DedName.Text)
        'nds = FetchDataSet("SELECT * FROM DeductMst WHERE CoId = " & selectedcoid & " And DName='" & DName & "'")
        'If nds.Tables(0).Rows.Count <= 0 Then
        '    'not found., open deductee detail form..
        '    frmDeducteeTDS.Show()
        '    frmDeducteeTDS.Frm_typ = 24
        '    'frmDeducteeTDS.Move(Me.Left + cbo16DedName.Left) + 100, (Me.Top + cbo16DedName.Top + cbo16DedName.Height + 650)
        '    frmDeducteeTDS.txtDName.Text = cbo16DedName.Text
        '    frmDeducteeTDS.optCo.Visible = False
        '    frmDeducteeTDS.optCo.TabStop = False
        '    frmDeducteeTDS.Show()
        '    FillDeducteeCombo(xMode)
        '    i = cbo16DedName.FindString(DName)
        '    If i >= 0 Then
        '        cbo16DedName.SelectedIndex = i
        '    End If
        '    'For i = 0 To cbo16DedName.ListCount - 1
        '    '    If cbo16DedName.list(i) = DName Then
        '    '        cbo16DedName.ListIndex = i
        '    '        Exit Sub
        '    '    End If
        '    'Next i
        '    If i = cbo16DedName.Items.Count Then cbo16DedName.SelectedIndex = -1      'Not Found
        'Else

        '    txt16pan.Text = nds.Tables(0).Rows(0)("DPan") & ""
        '    txt16pan.Tag = nds.Tables(0).Rows(0)("DType")
        'End If
        'txtdesig.Text = nds.Tables(0).Rows(0)("DDesgn") & ""
        'txtDstatus.Text = nds.Tables(0).Rows(0)("rs!Category")
        'Call Autotaxcal()

    End Sub
    Private Sub txtName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoName.Enter
        txt16CoName.BackColor = Color.LightYellow
    End Sub

    Private Sub txtName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoName.Leave
        txt16CoName.BackColor = Color.White
    End Sub

    Private Sub txtadress1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd1.Enter
        txt16CoAdd1.BackColor = Color.LightYellow
    End Sub
    Private Sub txtadress1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd1.Leave
        txt16CoAdd1.BackColor = Color.White
    End Sub

    Private Sub txtadress2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd2.Enter
        txt16CoAdd2.BackColor = Color.LightYellow
    End Sub
    Private Sub txtadress2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd2.Leave
        txt16CoAdd2.BackColor = Color.White
    End Sub

    Private Sub txtadress3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd3.Enter
        txt16CoAdd3.BackColor = Color.LightYellow
    End Sub
    Private Sub txtadress3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd3.Leave
        txt16CoAdd3.BackColor = Color.White
    End Sub

    Private Sub txtadress4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd4.Enter
        txt16CoAdd4.BackColor = Color.LightYellow
    End Sub
    Private Sub txtadress4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd4.Leave
        txt16CoAdd4.BackColor = Color.White
    End Sub

    Private Sub txtadress5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd5.Enter
        txt16CoAdd5.BackColor = Color.LightYellow
    End Sub
    Private Sub txtadress5_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoAdd5.Leave
        txt16CoAdd5.BackColor = Color.White
    End Sub

    Private Sub cmbState_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo16CoState.Enter
        cbo16CoState.BackColor = Color.LightYellow
    End Sub
    Private Sub cmbState_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo16CoState.Leave
        cbo16CoState.BackColor = Color.White
    End Sub

    Private Sub txtPCode_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoPin.Enter
        txt16CoPin.BackColor = Color.LightYellow
    End Sub
    Private Sub txtPCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoPin.Leave
        txt16CoPin.BackColor = Color.White
    End Sub

    Private Sub txtDedTAN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoTan.Enter
        txt16CoTan.BackColor = Color.LightYellow
    End Sub
    Private Sub txtDedTAN_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoTan.Leave
        txt16CoTan.BackColor = Color.White
    End Sub

    Private Sub txtDedPan_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoPan.Enter
        txt16CoPan.BackColor = Color.LightYellow
    End Sub
    Private Sub txtDedPan_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16CoPan.Leave
        txt16CoPan.BackColor = Color.White
    End Sub

    Private Sub txtDesig_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdesig.Enter
        txtdesig.BackColor = Color.LightYellow
    End Sub
    Private Sub txtDesig_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdesig.Leave
        txtdesig.BackColor = Color.White
    End Sub

    Private Sub txtEmpPAN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16pan.Enter
        txt16pan.BackColor = Color.LightYellow
    End Sub
    Private Sub txtEmpPAN_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16pan.Leave
        txt16pan.BackColor = Color.White
    End Sub

    Private Sub txtAssigYear_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16AY.Enter
        txt16AY.BackColor = Color.LightYellow
    End Sub
    Private Sub txtAssigYear_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16AY.Leave
        txt16AY.BackColor = Color.White
    End Sub

    Private Sub txt171_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16gross1.Enter
        txt16gross1.BackColor = Color.LightYellow
    End Sub
    Private Sub txt171_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16gross1.Leave
        txt16gross1.BackColor = Color.White
    End Sub

    Private Sub txt172_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16gross2.Enter
        txt16gross2.BackColor = Color.LightYellow
    End Sub
    Private Sub txt172_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16gross2.Leave
        txt16gross2.BackColor = Color.White
    End Sub

    Private Sub txt173_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16gross3.Enter
        txt16gross3.BackColor = Color.LightYellow
    End Sub
    Private Sub txt173_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16gross3.Leave
        txt16gross3.BackColor = Color.White
    End Sub

    Private Sub txtTotSalCur_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16grosstotCurEmp.Enter
        txt16grosstotCurEmp.BackColor = Color.LightYellow
    End Sub
    Private Sub txtTotSalCur_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16grosstotCurEmp.Leave
        txt16grosstotCurEmp.BackColor = Color.White
    End Sub

    Private Sub txtTotSalPre_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16grosstotPreEmp.Enter
        txt16grosstotPreEmp.BackColor = Color.LightYellow
    End Sub
    Private Sub txtTotSalPre_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16grosstotPreEmp.Leave
        txt16grosstotPreEmp.BackColor = Color.White
    End Sub

    Private Sub txtTotGrossSal_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16grosstotBoth.Enter
        txt16grosstotBoth.BackColor = Color.LightYellow
    End Sub
    Private Sub txtTotGrossSal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16grosstotBoth.Leave
        txt16grosstotBoth.BackColor = Color.White
    End Sub

    Private Sub txtAllowTotal_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtallow.Enter
        txtallow.BackColor = Color.LightYellow
    End Sub
    Private Sub txtAllowTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtallow.Leave
        txtallow.BackColor = Color.White
    End Sub

    Private Sub txtBal_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16bal.Enter
        txt16bal.BackColor = Color.LightYellow
    End Sub
    Private Sub txtBal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16bal.Leave
        txt16bal.BackColor = Color.White
    End Sub

    Private Sub txtEntAllow_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16EntAllow.Enter
        txt16EntAllow.BackColor = Color.LightYellow
    End Sub
    Private Sub txtEntAllow_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16EntAllow.Leave
        txt16EntAllow.BackColor = Color.White
    End Sub

    Private Sub txtTaxEmp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16ProfTax.Enter
        txt16ProfTax.BackColor = Color.LightYellow
    End Sub
    Private Sub txtTaxEmp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16ProfTax.Leave
        txt16ProfTax.BackColor = Color.White
    End Sub

    Private Sub txtTotDec_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalDedct.Enter
        txtTotalDedct.BackColor = Color.LightYellow
    End Sub
    Private Sub txtTotDec_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalDedct.Leave
        txtTotalDedct.BackColor = Color.White
    End Sub

    Private Sub txtTaxSal_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TaxableSalary.Enter
        txt16TaxableSalary.BackColor = Color.LightYellow
    End Sub
    Private Sub txtTaxSal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TaxableSalary.Leave
        txt16TaxableSalary.BackColor = Color.White
    End Sub

    Private Sub txtOtherInc_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16otherIncome.Enter
        txt16otherIncome.BackColor = Color.LightYellow
    End Sub
    Private Sub txtOtherInc_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16otherIncome.Leave
        txt16otherIncome.BackColor = Color.White
    End Sub

    Private Sub txtGrossIncome_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16GTI.Enter
        txt16GTI.BackColor = Color.LightYellow
    End Sub
    Private Sub txtGrossIncome_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16GTI.Leave
        txt16GTI.BackColor = Color.White
    End Sub

    Private Sub txt80cccTot_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1680c.Enter
        txt1680c.BackColor = Color.LightYellow
    End Sub
    Private Sub txt80cccTot_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1680c.Leave
        txt1680c.BackColor = Color.White
    End Sub

    Private Sub txt80CCFTot_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1680CCF.Enter
        txt1680CCF.BackColor = Color.LightYellow
    End Sub
    Private Sub txt80CCFTot_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1680CCF.Leave
        txt1680CCF.BackColor = Color.White
    End Sub

    Private Sub txt80CCGTot_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1680CCG.Enter
        txt1680CCG.BackColor = Color.LightYellow
    End Sub
    Private Sub txt80CCGTot_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1680CCG.Leave
        txt1680CCG.BackColor = Color.White
    End Sub

    Private Sub txtVIATot_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtT16OtherIVA.Enter
        txtT16OtherIVA.BackColor = Color.LightYellow
    End Sub
    Private Sub txtVIATot_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtT16OtherIVA.Leave
        txtT16OtherIVA.BackColor = Color.White
    End Sub


    Private Sub txtVIADeduc_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16OtherIVA.Enter
        txt16OtherIVA.BackColor = Color.LightYellow
    End Sub
    Private Sub txtVIADeduc_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16OtherIVA.Leave
        txt16OtherIVA.BackColor = Color.White
    End Sub


    Private Sub txtTotInco_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TotalTaxableIncome.Enter
        txt16TotalTaxableIncome.BackColor = Color.LightYellow
    End Sub
    Private Sub txtTotInco_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TotalTaxableIncome.Leave
        txt16TotalTaxableIncome.BackColor = Color.White
    End Sub


    Private Sub txtincome_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtincome.Leave
        txtincome.BackColor = Color.White
    End Sub

    Private Sub txtincome_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtincome.Enter
        txtincome.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16Tax_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16Tax.Enter
        txt16Tax.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16Tax_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16Tax.Leave
        txt16Tax.BackColor = Color.White
    End Sub

    Private Sub txt16Surcharge_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16Surcharge.Leave
        txt16Surcharge.BackColor = Color.White
    End Sub

    Private Sub txt16Surcharge_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16Surcharge.Enter
        txt16Surcharge.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16EduCess_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16EduCess.Leave
        txt16EduCess.BackColor = Color.White
    End Sub

    Private Sub txt16EduCess_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16EduCess.Enter
        txt16Surcharge.BackColor = Color.LightYellow
    End Sub

    Private Sub txtTax_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTax.Leave
        txtTax.BackColor = Color.White
    End Sub

    Private Sub txtTax_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTax.Enter
        txtTax.BackColor = Color.LightYellow
    End Sub

    Private Sub txtSurcharge_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSurcharge.Leave
        txtSurcharge.BackColor = Color.White
    End Sub

    Private Sub txtSurcharge_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSurcharge.Enter
        txtSurcharge.BackColor = Color.LightYellow
    End Sub

    Private Sub txtEd_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEd.Enter
        txtEd.BackColor = Color.LightYellow
    End Sub

    Private Sub txtEd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEd.Leave
        txtEd.BackColor = Color.White
    End Sub

    Private Sub txt16TotalTax_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TotalTax.Leave
        txt16TotalTax.BackColor = Color.White
    End Sub

    Private Sub txt16TotalTax_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TotalTax.Enter
        txt16TotalTax.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16Relief_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16Relief.Enter
        txt16Relief.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16Relief_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16Relief.Leave
        txt16Relief.BackColor = Color.White
    End Sub

    Private Sub txt16NetTax_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16NetTax.Leave
        txt16NetTax.BackColor = Color.White
    End Sub

    Private Sub txt16NetTax_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16NetTax.Enter
        txt16NetTax.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16TDS1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TDS1.Leave
        txt16TDS1.BackColor = Color.White
    End Sub

    Private Sub txt16TDS1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TDS1.Enter
        txt16TDS1.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16TDS2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TDS2.Leave
        txt16TDS1.BackColor = Color.White
    End Sub

    Private Sub txt16TDS2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TDS2.Enter
        txt16TDS1.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16totalTDS_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16totalTDS.Leave
        txt16totalTDS.BackColor = Color.White
    End Sub

    Private Sub txt16totalTDS_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16totalTDS.Enter
        txt16totalTDS.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16TaxPreEmp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TaxPreEmp.Enter
        txt16TaxPreEmp.BackColor = Color.LightYellow
    End Sub

    Private Sub txt16TaxPreEmp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt16TaxPreEmp.Leave
        txt16TaxPreEmp.BackColor = Color.White

    End Sub

    Private Sub txtPayRef_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayRef.Leave
        txtPayRef.BackColor = Color.White
    End Sub

    Private Sub txtPayRef_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPayRef.Enter
        txtPayRef.BackColor = Color.LightYellow
    End Sub

    Private Sub frm16Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rst As New DataSet
        Dim R As Integer ', a As String
        ' Dim oForm16 As New clsForm16Details
        txt16FrmDt.Text = Format(FromDate, "dd/MM/yy")
        txt16ToDt.Text = Format(ToDate, "dd/MM/yy")
        txt16AY.Text = AY
        CopyCoDetails()
        Fill24PRNNo()
        fill()
        'tabForm16.Tab = 0
        'tabForm16.Enabled = False
        'Set the grids right...



        'grd16allow.Columns(2).Visible = False
        'grd1680c.Columns(2).Visible = False
        'grd1680CCF.Columns(3).Visible = False
        'grd1680CCG.Columns(3).Visible = False
        'grd16OtherIVA.Columns(4).Visible = False
        FillParaData()

        txtDstatus.Text = ""

        For R = 1 To grd16ManualTax.Rows.Count - 1
            grd16ManualTax.Rows(R).Cells(0).Value = R
        Next R


    End Sub
    Private Sub FillParaData()
        'read parameter data from the text file and fill the respective combos..
        Dim ReadStream As StreamReader
        Dim DataRead

        Dim cmb As New DataGridViewComboBoxColumn()
        cmb.Name = "Particulars"
        cmb.Width = 200
        cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        Dim cmbOthInc As New DataGridViewComboBoxColumn()
        cmbOthInc.Name = "Particulars"
        cmbOthInc.Width = 200
        cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        Dim cmbSec80C As New DataGridViewComboBoxColumn()
        cmbSec80C.Name = "Particulars"
        cmbSec80C.Width = 200
        cmbSec80C.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        Dim cmbSec80Ccf As New DataGridViewComboBoxColumn()
        cmbSec80Ccf.Name = "Particulars"
        cmbSec80Ccf.Width = 200
        cmbSec80Ccf.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        Dim cmbSec80Ccg As New DataGridViewComboBoxColumn()
        cmbSec80Ccg.Name = "Particulars"
        cmbSec80Ccg.Width = 200
        cmbSec80Ccg.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        Dim cmbChp6a As New DataGridViewComboBoxColumn()
        cmbChp6a.Name = "Particulars"
        cmbChp6a.Width = 200
        cmbChp6a.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        oCoObj = oCoObj.FetchCo(selectedcoid)
        grd16allow.ColumnCount = 0
        grd16otherIncome.ColumnCount = 0
        grd1680c.ColumnCount = 0
        grd1680CCG.ColumnCount = 0
        grd1680CCF.ColumnCount = 0
        grd16OtherIVA.ColumnCount = 0

        If File.Exists(Application.StartupPath & "\Database\Form16Parameters.txt") Then
            ReadStream = File.OpenText(Application.StartupPath & "\Database\Form16Parameters.txt")
            Do While Not ReadStream.EndOfStream

                DataRead = Split(ReadStream.ReadLine, ",")
                If DataRead(0) = "A" Then
                    If DataRead(2) = "T" Then
                        'AllowCboTxt = AllowCboTxt & "|" & DataRead(1)
                        cmb.Items.Add(DataRead(1))
                    End If
                ElseIf DataRead(0) = "O" Then
                    If DataRead(2) = "T" Then
                        'OthIncCbotxt = OthIncCbotxt & "|" & DataRead(1)
                        cmbOthInc.Items.Add(DataRead(1))
                    End If
                ElseIf DataRead(0) = "E" Then
                    If DataRead(2) = "T" Then
                        ' Sec80CCboTxt = Sec80CCboTxt & "|" & DataRead(1)
                        cmbSec80C.Items.Add(DataRead(1))
                    End If
                ElseIf DataRead(0) = "V" Then
                    If DataRead(2) = "T" Then
                        'Chp6aCboTxt = Chp6aCboTxt & "|" & DataRead(1)
                        cmbChp6a.Items.Add(DataRead(1))
                    End If
                ElseIf DataRead(0) = "F" Then
                    If DataRead(2) = "T" Then
                        'Sec80CcfCboTxt = Sec80CcfCboTxt & "|" & DataRead(1)
                        cmbSec80Ccf.Items.Add(DataRead(1))
                    End If
                ElseIf DataRead(0) = "G" Then
                    If DataRead(2) = "T" Then
                        cmbSec80Ccg.Items.Add(DataRead(1))
                        'Sec80CcgCboTxt = Sec80CcgCboTxt & "|" & DataRead(1)
                    End If
                End If
            Loop
        End If
        'grd16allow.ColHidden(2) = True
        'grd16otherIncome.ColHidden(2) = True
        'grd1680c.ColHidden(3) = True
        'grd1680CCF.ColHidden(3) = True
        'grd1680CCG.ColHidden(3) = True
        'grd16OtherIVA.ColHidden(4) = True


        grd16allow.Columns.Add(cmb)
        grd16allow.Columns.Add("Rs.", "Rs.")
        grd16allow.Columns.Add("col1", "col1")
        grd16allow.Columns("col1").Visible = False
        grd16allow.Columns.Add("col2", "col2")
        grd16allow.Columns("col2").Visible = False

        grd16otherIncome.Columns.Add(cmbOthInc)
        grd16otherIncome.Columns.Add("Rs.", "Rs.")
        grd16otherIncome.Columns.Add("col3", "col3")
        grd16otherIncome.Columns("col3").Visible = False
        grd16otherIncome.Columns.Add("col4", "col4")
        grd16otherIncome.Columns("col4").Visible = False

        grd1680c.Columns.Add(cmbSec80C)
        grd1680c.Columns.Add("Gross Amount", "Gross Amount")
        grd1680c.Columns.Add("Deductible Amount", "Deductible Amount")
        grd1680c.Columns.Add("col1", "col1")
        grd1680c.Columns("col1").Visible = False

        grd1680CCG.Columns.Add(cmbSec80Ccg)
        grd1680CCG.Columns.Add("Gross Amount", "Gross Amount")
        grd1680CCG.Columns.Add("Deductible Amount", "Deductible Amount")
        grd1680CCG.Columns.Add("col1", "col1")
        grd1680CCG.Columns("col1").Visible = False

        grd1680CCF.Columns.Add(cmbSec80Ccf)
        grd1680CCF.Columns.Add("Gross Amount", "Gross Amount")
        grd1680CCF.Columns.Add("Deductible Amount", "Deductible Amount")
        grd1680CCF.Columns.Add("col1", "col1")
        grd1680CCF.Columns("col1").Visible = False

        grd16OtherIVA.Columns.Add(cmbChp6a)
        grd16OtherIVA.Columns.Add("Gross Amount", "Gross Amount")
        grd16OtherIVA.Columns.Add("Qualifying Amount", "Qualifying Amount")
        grd16OtherIVA.Columns.Add("Deductible Amount", "Deductible Amount")

        grd16ManualTax.Columns(0).Frozen = True





    End Sub




    ' Dim fso As New FileSystemObject
    'Public Sub grd16allSubTotal()
    'With grd16allow
    '    .AutoSizeMode = flexAutoSizeRowHeight
    '    .AutoSize 1, 1
    '    .SubtotalPosition = flexSTBelow
    ''    .Subtotal flexSTSum, -1, 1, "NN, NN, NN, NN, NNN.00", , vbRed, True
    '    .Subtotal flexSTSum, -1, 1, "###########.00", , vbRed, True
    '    .Redraw = True
    'End With
    'End Sub


    Public Sub FillDeducteeCombo(Mode As String)
        Dim rst As New DataSet, opnStr As String
        If Mode = "A" Then
            opnStr = "Select * FROM DeductMst WHERE Did Not In (Select dID FROM Form16Details) And CoID = " & selectedcoid & "  ORDER BY DName "
        Else
            opnStr = "Select * FROM DeductMst WHERE CoID=" & selectedcoid & "  ORDER BY DName "
        End If
        rst = FetchDataSet(opnStr)

        cbo16DedName.DataSource = Nothing
        cbo16DedName.Items.Clear()


        'For I = 0 To rst.Tables(0).Rows.Count
        cbo16DedName.DataSource = rst.Tables(0)
        cbo16DedName.DisplayMember = "DName"
        cbo16DedName.ValueMember = "DId"
        ' Next I
        cbo16DedName.SelectedIndex = -1

        rst.Dispose()

    End Sub

    Private Sub Fill24PRNNo()
        Dim R As Integer
        Dim rst As New DataSet
        rst = FetchDataSet("Select * from RetnMst Where CoId = " & selectedcoid & " And  Left(frmtype,2) = '24' ORDER BY FRMTYPE")
        R = 1
        'Do While Not rst.EOF
        For k = 0 To rst.Tables(0).Rows.Count - 1
            grdPRNdet.Rows.Add()
            grdPRNdet.Rows(k).Cells(0).Value = rst.Tables(0).Rows(k)("FrmType").ToString()
            grdPRNdet.Rows(k).Cells(1).Value = IIf(String.IsNullOrEmpty(rst.Tables(0).Rows(k)("NewreceiptNO").ToString()), "", rst.Tables(0).Rows(k)("NewreceiptNO").ToString())
            R = R + 1
            'rst.MoveNext
        Next k
        rst.Dispose()

    End Sub

    Private Sub CopyCoDetails()
        Dim oCompany As New clsCoMst
        oCompany = oCompany.FetchCo(selectedcoid)
        With oCompany
            txt16CoName.Text = .CoName
            txt16CoName.Tag = selectedcoid 'frmCoMst.txtCoName.Tag
            txt16CoAdd1.Text = .CoAdd1
            txt16CoAdd2.Text = .CoAdd2
            txt16CoAdd3.Text = .CoAdd3
            txt16CoAdd4.Text = .CoAdd4
            txt16CoAdd5.Text = .CoAdd5
            txt16CoPin.Text = .CoPin
            cbo16CoState.Text = frmCoMst.cboCoState.Text
            txt16CoTan.Text = .CoTAN
            txt16CoPan.Text = .CoPAN

        End With
    End Sub

    Private Sub cbo16DedName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo16DedName.SelectedIndexChanged

    End Sub

    'Private Sub cmbName_Validated(cancel As Boolean)
    '    Dim rs As New DataSet
    '    If cbo16DedName.SelectedIndex = -1 Or Trim(cbo16DedName.Text) = "" Then
    '        MsgBox("Please select a deductee first..", vbInformation, "No Deductee Selected")
    '        tabForm16.Enabled = False
    '        cancel = True
    '    Else
    '        tabForm16.Enabled = True
    '        Call frmTDS24Q.FillFrm16DataUsingDID(cbo16DedName.SelectedItem(cbo16DedName.SelectedIndex))
    '    End If
    '    rs = New DataSet
    '    rs = FetchDataSet("SELECT SUM(amtofpayment) FROM Deductee24Q WHERE DID=" & cbo16DedName.SelectedItem(cbo16DedName.SelectedIndex))
    '    txt16TaxableSalary.Text = IIf((rs.Tables(0).Rows(0)("amtofpayment").ToString()), 0, rs.Tables(0).Rows(0)("amtofpayment"))

    'End Sub

    Private Sub tabBasicData_Click(sender As Object, e As EventArgs) Handles tabBasicData.Click

    End Sub

    'Private Sub cmd16delete_Click(sender As Object, e As EventArgs) Handles cmd16delete.Click
    '    Dim sql As String
    '    Dim nds As DataSet
    '    Dim cmd As New OleDbCommand
    '    If MsgBox("Are you sure you want to delete this record?", vbQuestion + vbYesNo, "Confirm") = vbYes Then
    '        'lvwChallan.Items(0).SubItems(1).Text
    '        sql = "Delete * from F16Challan where f16ID=" & frmTDS24Q.lvwForm16.SelectedItems(0).SubItems(24).Text
    '        cmd.Connection = cn
    '        cmd.CommandText = sql
    '        transaction = cn.BeginTransaction()
    '        cmd.Transaction = transaction
    '        Try

    '            cmd.ExecuteNonQuery()
    '            transaction.Commit()
    '        Catch ex As Exception
    '            transaction.Rollback()
    '            MessageBox.Show(ex.Message)
    '        End Try
    '        cmd.Dispose()
    '        transaction.Dispose()
    '        ' If cn.Errors.Count = 0 Then
    '        sql = "Delete * from Form16MoreDetails where f16ID=" & frmTDS24Q.lvwForm16.SelectedItems(0).SubItems(24).Text
    '        cmd.Connection = cn
    '        cmd.CommandText = sql
    '        transaction = cn.BeginTransaction()
    '        cmd.Transaction = transaction
    '        Try

    '            cmd.ExecuteNonQuery()
    '            transaction.Commit()
    '        Catch ex As Exception
    '            transaction.Rollback()
    '            MsgBox("Unable to Delete right now..", vbInformation)
    '            ' MessageBox.Show(ex.Message)
    '        End Try
    '        cmd.Dispose()
    '        transaction.Dispose()
    '        ' If cn.Errors.Count = 0 Then
    '        sql = "Delete * from Form16details where f16id= " & frmTDS24Q.lvwForm16.SelectedItems(0).SubItems(24).Text
    '        cmd.Connection = cn
    '        cmd.CommandText = sql
    '        transaction = cn.BeginTransaction()
    '        cmd.Transaction = transaction
    '        Try

    '            cmd.ExecuteNonQuery()
    '            transaction.Commit()
    '        Catch ex As Exception
    '            transaction.Rollback()
    '            MsgBox("Unable to Delete right now..", vbInformation)
    '            ' MessageBox.Show(ex.Message)
    '        End Try
    '        cmd.Dispose()
    '        transaction.Dispose()
    '        'If cn..Count = 0 Then
    '        '            cn.BeginTransaction()
    '        '            Me.Close()
    '        '        Else
    '        '            transaction.Rollback()

    '        'End If
    '    Else

    '    End If
    '    ' End If
    '    '  End If
    'End Sub

    Private Sub cmd16Save_Click(sender As Object, e As EventArgs) Handles cmd16Save.Click
        If Val(txt16TaxableSalary.Text) > Val(txtAllTax.Text) Then
            MsgBox("Taxable Salary is greater than Total Taxable Salary Of All Quarters.")
        End If

        If Val(txt16TaxableSalary.Text) < Val(txtAllTax.Text) Then
            MsgBox("Taxable Salary is less than Total Taxable Salary Of All Quarters.")
        End If
        CHECKTAXCAL()

        '    If taxcal = True Then
        Check80CTotals(Val(txtgrd80CCal.Text))
        Check80CCFTotals(Val(txtgrd80CCFCal.Text))
        Check80CCGTotals(Val(txtgrd80CCGCal.Text))

        If xMode = "A" Then
            If oForm16.Insert(oForm16) = False Then
                'If oForm16.Insert(oForm16, oAllowances, oOthIncomes, oSec80CDeductions, oSec80CCFDeductions, oSec80CCGDeductions, oChapter6ADeductions, grd16ManualTax, Val(txt16grosstotPreEmp.Text), Val(txt16TaxPreEmp.Text), IIf(chkHighRate.Checked = True, True, False)) = False Then
                MsgBox("Unable to save data", vbCritical, "ERROR!!")
            Else
                'Data Saved properly...exit this form...and return to main form
                Dim R As Long


                For R = 0 To grd16allow.Rows.Count - 1
                    If grd16allow.Rows(R).Cells(0).Value <> Nothing Then
                        If oForm16More.Insert(oForm16More, R, grd16allow, "A") = False Then
                            MsgBox("Unable to save data", vbCritical, "ERROR!!")
                            Exit For
                        End If
                    End If
                Next R
                For R = 0 To grd16otherIncome.Rows.Count - 1
                    If grd16otherIncome.Rows(R).Cells(0).Value <> Nothing Then
                        If oForm16More.Insert(oForm16More, R, grd16otherIncome, "O") = False Then
                            MsgBox("Unable to save data", vbCritical, "ERROR!!")
                            Exit For
                        End If
                    End If
                Next R

                For R = 0 To grd1680c.Rows.Count - 1
                    If grd1680c.Rows(R).Cells(0).Value <> Nothing Then
                        If oForm16More.Insert(oForm16More, R, grd1680c, "E") = False Then
                            MsgBox("Unable to save data", vbCritical, "ERROR!!")
                            Exit For
                        End If
                    End If
                Next R

                For R = 0 To grd1680CCF.Rows.Count - 1
                    If grd1680CCF.Rows(R).Cells(0).Value <> Nothing Then
                        If oForm16More.Insert(oForm16More, R, grd1680CCF, "E") = False Then
                            MsgBox("Unable to save data", vbCritical, "ERROR!!")
                            Exit For
                        End If
                    End If
                Next R

                For R = 0 To grd1680CCG.Rows.Count - 1
                    If grd1680CCG.Rows(R).Cells(0).Value <> Nothing Then
                        If oForm16More.Insert(oForm16More, R, grd1680CCG, "G") = False Then
                            MsgBox("Unable to save data", vbCritical, "ERROR!!")
                            Exit For
                        End If
                    End If
                Next R


                For R = 0 To grd16OtherIVA.Rows.Count - 1
                    If grd16OtherIVA.Rows(R).Cells(0).Value <> Nothing Then
                        If oForm16More.Insert(oForm16More, R, grd16OtherIVA, "V") = False Then
                            MsgBox("Unable to save data", vbCritical, "ERROR!!")
                            Exit For
                        End If
                    End If
                Next R

                For R = 1 To grd16ManualTax.Rows.Count - 2
                    If grd16ManualTax.Rows(R).Cells(1).Value <> Nothing Then
                        If oF16Challan.Insert(F16ID, grd16ManualTax.Rows(R).Cells(1).Value, grd16ManualTax.Rows(R).Cells(2).Value, grd16ManualTax.Rows(R).Cells(3).Value, grd16ManualTax.Rows(R).Cells(5).Value, grd16ManualTax.Rows(R).Cells(8).Value, grd16ManualTax.Rows(R).Cells(6).Value, grd16ManualTax.Rows(R).Cells(7).Value, vbNullString) = False Then
                            MsgBox("Unable to save data", vbCritical, "ERROR!!")
                            Exit For
                        End If
                    End If
                Next R

                Me.Close()


            End If
        ElseIf xMode = "E" Then
            '    If oForm16.Update(oForm16, oAllowances, oOthIncomes, oSec80CDeductions, oSec80CCFDeductions, oSec80CCGDeductions, oChapter6ADeductions, grd16ManualTax, Val(txt16grosstotPreEmp), Val(txt16TaxPreEmp), IIf(chkHighRate.Checked = True, True, False)) = False Then
            '        MsgBox("Unable to save data", vbCritical, "ERROR!!")
            '    Else
            '        'Data Saved properly...exit this form...and return to main form
            '        Me.Close()
            '    End If
            'Else
            '    MsgBox("Critical Error - xMode not Set n Save Click, Call JAK and report this error!", vbCritical, "FATAL ERROR!")
        End If


    End Sub
    Private Sub CHECKTAXCAL()
        If Val(txt16Tax.Text) <> Val(txtTax.Text) Or Val(txt16Surcharge.Text) <> Val(txtSurcharge.Text) Or Val(txt16EduCess.Text) <> Val(txtEd.Text) Then
            MsgBox("Form16 Tax calculation not matched with Tax calculation asPer Law")
        End If
    End Sub

    Private Sub Check80CTotals(TotalVal As Double)
        If TotalVal > 150000 Then
            MsgBox("Aggregate amount deductible u/s 80C shall not exceed one lakh Fifty Thousand rupees", 0 + 48, "Caution")
            Exit Sub
        End If

    End Sub

    Private Sub Check80CCFTotals(TotalVal As Double)
        If TotalVal > 20000 Then
            MsgBox("Aggregate amount deductible u/s 80CCF shall not exceed Twenty Thousand", 0 + 48, "Caution")
            Exit Sub
        End If

    End Sub

    Private Sub Check80CCGTotals(TotalVal As Double)
        If TotalVal > 25000 Then
            MsgBox("Aggregate amount deductible u/s 80CCG shall not exceed Twenty five Thousand", 0 + 48, "Caution")
            Exit Sub
        End If
    End Sub

    Private Sub cmdCopyCal_Click(sender As Object, e As EventArgs) Handles cmdCopyCal.Click
        txt16Tax.Text = txtTax.Text
        txt16Surcharge.Text = txtSurcharge.Text
        txt16EduCess.Text = txtEd.Text
        txt16TotalTax.Text = txtTotTax.Text
        txt16Relief.Focus()
    End Sub

    Private Sub cmdDedMast_Click(sender As Object, e As EventArgs) Handles cmdDedMast.Click
        Dim i As Long, DFound As Boolean, OldId As Long, dname As String
        Dim frm As New frmdeduteeTDSMST
        If cbo16DedName.SelectedIndex < 0 Then Exit Sub

        frm.Frm_typ = "frm16"
        frm.Show()
        frm.Hide()
        dname = cbo16DedName.Text
        With frm
            i = .cboDName.FindString(cbo16DedName.Text)
            If i >= 0 Then
                .cboDName.SelectedIndex = i

                DFound = True
            End If

        End With

        If DFound = True Then frm.Show()
        'refill the combo with new data...
        Call FillDeducteeCombo(xMode)


        i = cbo16DedName.FindString(dname)
        If i >= 0 Then
            cbo16DedName.SelectedIndex = i
            'cbo16DedName_Leave(sender, e)

        End If

    End Sub

    Private Sub grd1680c_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680c.CellEndEdit
        Call CalculateSalaryNTax()
    End Sub
    'Public Sub CalculateSalaryNTax()
    '    Dim R As Long, TotalAllow As Double
    '    Dim TotalOthInc As Double
    '    Dim Total80C, TotalChapVIA As Integer
    '    Dim Total80CCG, Total80CCF As Integer
    '    'Calculate the Gross Total Salary
    '    txt16grosstotCurEmp.Text = Val(txt16gross1.Text) + Val(txt16gross2.Text) + Val(txt16gross3.Text) '+ Val(txt16grosstotCurEmp.Text) + Val(txt16grosstotPreEmp.Text)
    '    txt16grosstotBoth.Text = Val(txt16grosstotCurEmp.Text) + Val(txt16grosstotPreEmp.Text)
    '    'Calculate total allowance and deduct from Gross Total Salary...to get Balance...
    '    TotalAllow = 0
    '    For R = 1 To grd16allow.Rows.Count - 1
    '        TotalAllow = TotalAllow + Val(grd16allow.Rows(R).Cells(1).Value)
    '    Next R
    '    txtallow.Text = TotalAllow
    '    txt16bal.Text = Val(txt16grosstotBoth.Text) - TotalAllow
    '    'Calculate Total Deduction of section 16
    '    txtTotalDedct.Text = Val(txt16EntAllow.Text) + Val(txt16ProfTax.Text)
    '    'now, calculate total taxable salary...
    '    txt16TaxableSalary.Text = Val(txt16bal.Text) - Val(txtTotalDedct.Text)
    '    'now calculate the sum of other incomes and to taxable salary to get gross total income
    '    TotalOthInc = 0
    '    For R = 1 To grd16otherIncome.Rows.Count - 1
    '        TotalOthInc = TotalOthInc + Val(grd16otherIncome.Rows(R).Cells(1).Value)
    '    Next R
    '    txt16GTI.Text = Val(txt16TaxableSalary.Text) + TotalOthInc
    '    'now calculate the sum of other incomes and to taxable salary to get gross total income
    '    Total80C = 0
    '    For R = 1 To grd1680c.Rows.Count - 1
    '        Total80C = Total80C + grd1680c.Rows(R).Cells(2).Value.ToString()
    '    Next R
    '    Total80CCF = 0
    '    For R = 1 To grd1680CCF.Rows.Count - 1
    '        Total80CCF = Total80CCF + grd1680CCF.Rows(R).Cells(2).Value.ToString()
    '    Next R
    '    Total80CCG = 0
    '    For R = 1 To grd1680CCG.Rows.Count - 1
    '        Total80CCG = Total80CCG + grd1680CCG.Rows(R).Cells(2).Value.ToString()
    '    Next R
    '    TotalChapVIA = 0
    '    For R = 1 To grd16OtherIVA.Rows.Count - 1
    '        TotalChapVIA = TotalChapVIA + grd16OtherIVA.Rows(R).Cells(3).Value.ToString()
    '    Next R
    '    txt1680c.Text = Total80C
    '    txt1680CCF.Text = Total80CCF
    '    txt1680CCG.Text = Total80CCG
    '    txtT16OtherIVA.Text = TotalChapVIA
    '    txt16otherIncome.Text = TotalOthInc
    '    txt16OtherIVA.Text = Val(txt1680c.Text) + Val(txtT16OtherIVA.Text) + Val(txt1680CCF.Text) + txt1680CCG.Text
    '    txt16TotalTaxableIncome.Text = Val(txt16GTI.Text) - Val(txt16OtherIVA.Text)
    '    txtincome.Text = Val(txt16TotalTaxableIncome.Text)
    '    'Now Calculate the taxes...
    '    ' txt16Tax.Text = Val(txt16TaxCurEmp.Text) + Val(txt16TaxPreEmp.Text)
    '    txt16TotalTax.Text = Val(txt16Tax.Text) + Val(txt16Surcharge.Text) + Val(txt16EduCess.Text)
    '    txt16NetTax.Text = Val(txt16TotalTax.Text) - Val(txt16Relief.Text)
    '    txt16totalTDS.Text = Val(txt16TDS1.Text) + Val(txt16TDS2.Text)
    '    txtPayRef.Text = Val(txt16NetTax.Text) - Val(txt16totalTDS.Text) - Val(txt16TaxPreEmp.Text)
    '    Check80CTotals(Total80C)
    '    'UpdateAutoCalcFields
    '    '   Autotaxcal
    'End Sub

    Private Sub txtgrd80CCFCal_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel

    End Sub

    Private Sub grd1680CCF_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680CCF.CellLeave
        'CalculateSalaryNTax()
    End Sub

    Private Sub grd16otherIncome_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd16otherIncome.CellContentClick

    End Sub

    Private Sub grd1680c_MouseUp(sender As Object, e As MouseEventArgs) Handles grd1680c.MouseUp
        '    If Button = vbRightButton Then
        '        If UCase(Me.ActiveControl.Name) <> "GRD1680CCF" Then grd1680CCF.Focus
        '        grd1680CCF.Select grd1680CCF.MouseRow, grd1680CCF.MouseCol
        '    PopupMenu mnuContext
        'End If
    End Sub

    Private Sub grd1680c_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680c.CellContentClick

    End Sub

    Private Sub grd1680c_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680c.CellLeave
        'CalculateSalaryNTax()
    End Sub

    Private Sub grd1680CCG_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680CCG.CellLeave
        'CalculateSalaryNTax()
    End Sub

    Private Sub grd16ManualTax_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles grd16ManualTax.CellBeginEdit
        Dim i As Integer, col As Integer
        col = e.ColumnIndex
        i = e.RowIndex
        If i <> grd16ManualTax.Rows.GetLastRow(DataGridViewElementStates.None) Then
            grd16ManualTax.Rows(i).Cells(0).Value = i + 1
        End If

        'With grd16ManualTax
        '    If i > -1 And grd16ManualTax.Rows(i).Cells(1).Value > 0 Then
        '        '        If Col = 5 Then
        '        '            If Len(.TextMatrix(Row, 5)) < 6 Or Len(.TextMatrix(Row, 5)) > 6 Then
        '        '                MsgBox "Invalid Cheque number!", 0 + 16
        '        '                .EditText = .TextMatrix(Row, 6)
        '        '                Exit Sub
        '        '            End If
        '        '        End If
        '        If col = 6 Then
        '            If (Len(.Rows(i).Cells(6).Value) < 6 Or Len(.Rows(i).Cells(6).Value) > 7) And Len(.Rows(i).Cells(6).Value) > 0 Then
        '                MsgBox("Length of Bank BSR Code should be 7 Character.", 0 + 16)
        '                '* .EditText = .Rows(i).Cells(6).Value
        '                e.Cancel = True
        '                .Item(6, 1).Selected = True
        '                Exit Sub
        '            End If
        '        End If
        '        If col = 7 Then
        '            If Not IsDate(.Rows(i).Cells(7).Value) Then
        '                MsgBox("Invalid Date!", 0 + 16)
        '                '*.EditText = .Rows(i).Cells(7).Value
        '                e.Cancel = True
        '                Exit Sub
        '            End If
        '            If .Rows(i).Cells(7).Value = "  /  /    " Or (CDate(.Rows(i).Cells(7).Value) < FromDate Or CDate(.Rows(i).Cells(7).Value) > ToDate) Then
        '                MsgBox("Invalid Date!", 0 + 16)
        '                e.Cancel = True
        '                '.EditText = .TextMatrix(Row, 7)
        '                '.EditCell
        '                Exit Sub
        '            End If
        '        End If
        '        grd16ManualTax.Rows(i).Cells(4).Value = grd16ManualTax.Rows(i).Cells(1).Value + grd16ManualTax.Rows(i).Cells(2).Value + grd16ManualTax.Rows(i).Cells(3).Value
        '    End If
        'End With
        'If col < grd16ManualTax.Columns.Count - 1 Then
        '    If col = 3 Then
        '        grd16ManualTax.Columns.Item(col + 2).Selected = True
        '    Else
        '        grd16ManualTax.Columns.Item(col + 1).Selected = True
        '    End If
        'End If

        '' *       grd16ManualTax.Redraw = True
        ''        grd16ManualTax.Subtotal flexSTSum, -1, 1, "##0", , vbRed, False, "Total:"
        ''grd16ManualTax.Subtotal flexSTSum, -1, 2, "##0", , vbRed, False, "Total:"
        ''grd16ManualTax.Subtotal flexSTSum, -1, 3, "##0", , vbRed, False, "Total:"
        ''grd16ManualTax.Subtotal flexSTSum, -1, 4, "##0", , vbRed, False, "Total:"

        'Dim r As Integer = grdchallanDetails.RowCount - 1
        'txt16TDS1.Text = grdchallanDetails.Rows(r).Cells(4).Value + grd16ManualTax.Rows(grd16ManualTax.RowCount - 1).Cells(4).Value
    End Sub

    Private Sub txt16EntAllow_TextChanged(sender As Object, e As EventArgs) Handles txt16EntAllow.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub grd16OtherIVA_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles grd16OtherIVA.CellLeave
        'CalculateSalaryNTax()
    End Sub

    Private Sub grd16otherIncome_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles grd16otherIncome.CellLeave
        'CalculateSalaryNTax()
    End Sub

    Private Sub cbo16DedName_LostFocus(sender As Object, e As EventArgs) Handles cbo16DedName.LostFocus
        Call CtrlLostFocus(cbo16DedName)

    End Sub

    Private Sub fill()
        If cbo16DedName.Text = vbNullString Then Exit Sub
        If cbo16DedName.Text <> vbNullString Then
            '    Dim itm As ListItem
            '    Set itm = lvwDeductee.FindItem(cbo16DedName.Text, 1)
            '    If itm Is Nothing Then
            '
            '    Else
            '        MsgBox "Entry of chosen Deductee is already present"
            '        cbo16DedName.ListIndex = -1
            '        Exit Sub
            '    End If
        End If
        Dim rs As New DataSet
        Dim i As Integer, DName As String
        DName = UCase(cbo16DedName.Text)
        rs = FetchDataSet("SELECT * FROM DeductMst WHERE CoId = " & selectedcoid & " And DName='" & DName & "'") ', Cnn
        If rs.Tables(0).Rows.Count <= 0 Then
            'not found., open deductee detail form..
            frmDeducteeTDS.Show()
            frmDeducteeTDS.Frm_typ = 24
            '*frmDeducteeTDS.Move((Me.Left + cbo16DedName.Left) + 100, (Me.Top + cbo16DedName.Top + cbo16DedName.Height + 650))
            frmDeducteeTDS.txtDName.Text = cbo16DedName.Text
            frmDeducteeTDS.optCo.Visible = False
            frmDeducteeTDS.optCo.TabStop = False
            frmDeducteeTDS.Show()
            FillDeducteeCombo(xMode)
            For i = 0 To cbo16DedName.Items.Count - 1
                If cbo16DedName.SelectedItem(i) = DName Then
                    cbo16DedName.SelectedIndex = i
                    Exit Sub
                End If
            Next i
            If i = cbo16DedName.Items.Count Then cbo16DedName.SelectedIndex = -1      'Not Found
        Else
            txt16pan.Text = rs.Tables(0).Rows(0)("DPan").ToString() & ""
            txt16pan.Tag = rs.Tables(0).Rows(0)("DType").ToString()
        End If
        txtdesig.Text = rs.Tables(0).Rows(0)("DDesgn").ToString() & ""
        txtDstatus.Text = rs.Tables(0).Rows(0)("Category").ToString()
        Call Autotaxcal()
        rs.Dispose()
        rs = Nothing
    End Sub
    Private Sub txt16gross1_TextChanged(sender As Object, e As EventArgs) Handles txt16gross1.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16gross2_TextChanged(sender As Object, e As EventArgs) Handles txt16gross2.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16gross3_TextChanged(sender As Object, e As EventArgs) Handles txt16gross3.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub Autotaxcal()
        Dim tax1 As Double : Dim tax2 As Double : Dim tax3 As Double
        Dim tax, i, itax, amt, amt1 As Double
        itax = 0
        amt = 0 : amt1 = 0 : tax = 0

        If txtDstatus.Text = "W" Then
            tax1 = 250000
            tax2 = 500000
            tax3 = 1000000
        ElseIf txtDstatus.Text = "S" Then
            tax1 = 300000
            tax2 = 500000
            tax3 = 1000000
        ElseIf txtDstatus.Text = "G" Then
            tax1 = 250000
            tax2 = 500000
            tax3 = 1000000
        ElseIf txtDstatus.Text = "O" Then
            tax1 = 500000
            tax2 = 1000000
            'tax3 = 800000
        End If

        If Not txtincome.Text = "" Then
            If (txtincome.Text <= tax1) Then
                i = 1
            ElseIf (txtincome.Text <= tax2) Then
                i = 2
            ElseIf (txtincome.Text <= tax3) Then
                i = 3
            Else
                i = 4
            End If
        End If

        amt = Val(txtincome.Text)
        While Not i = 0
            Select Case i
                Case 1
                    itax = itax
                Case 2
                    If txtDstatus.Text = "O" Then
                        amt1 = amt - tax1
                        amt = amt - amt1
                        tax = (amt1 * 20) / 100
                        itax = itax + tax
                    Else
                        amt1 = amt - tax1
                        amt = amt - amt1
                        tax = (amt1 * 10) / 100
                        itax = itax + tax
                    End If
                Case 3
                    If txtDstatus.Text = "O" Then
                        amt1 = amt - tax2
                        amt = amt - amt1
                        tax = (amt1 * 30) / 100
                        itax = itax + tax
                    Else
                        amt1 = amt - tax2
                        amt = amt - amt1
                        tax = (amt1 * 20) / 100
                        itax = itax + tax
                    End If
                Case 4
                    If txtDstatus.Text <> "O" Then
                        amt1 = amt - tax3
                        amt = amt - amt1
                        tax = (amt1 * 30) / 100
                        itax = itax + tax
                    End If
            End Select
            i = i - 1
        End While

        If Val(txtincome.Text) <= 500000 Then
            If itax > 5000 Then
                itax = itax - 5000
            Else
                itax = 0
            End If
        End If
        If (txtincome.Text <> "") Then
            If (txtincome.Text > 10000000) Then
                txtSurcharge.Text = Math.Round((itax * 15) / 100, 0)
            Else
                txtSurcharge.Text = 0
            End If
        End If
        txtTax.Text = Math.Round(itax, 0)
        'txtSurcharge = 0
        txtEd.Text = Math.Round(((itax + Val(txtSurcharge.Text)) * 3) / 100, 0)
        txtTotTax.Text = Val(txtTax.Text) + Val(txtSurcharge.Text) + Val(txtEd.Text)
    End Sub

    Private Sub cbo16DedName_GotFocus(sender As Object, e As EventArgs) Handles cbo16DedName.GotFocus
    End Sub

    Private Sub tabForm16_Click(sender As Object, e As EventArgs) Handles tabForm16.Click
        Call Autotaxcal()
    End Sub

    Private Sub txt16EntAllow_Layout(sender As Object, e As LayoutEventArgs) Handles txt16EntAllow.Layout

    End Sub

    Private Sub txt16EntAllow_GotFocus(sender As Object, e As EventArgs) Handles txt16EntAllow.GotFocus
        Call CtrlGotFocus(txt16EntAllow)
    End Sub

    Private Sub txt16EntAllow_LostFocus(sender As Object, e As EventArgs) Handles txt16EntAllow.LostFocus
        Call CtrlLostFocus(txt16EntAllow)
    End Sub

    Private Sub txt16grosstotCurEmp_TextChanged(sender As Object, e As EventArgs) Handles txt16grosstotCurEmp.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16gross3_GotFocus(sender As Object, e As EventArgs) Handles txt16gross3.GotFocus
        Call CtrlGotFocus(txt16gross3)
    End Sub

    Private Sub txt16gross3_LostFocus(sender As Object, e As EventArgs) Handles txt16gross3.LostFocus
        Call CtrlLostFocus(txt16gross3)
    End Sub

    Private Sub txt16grosstotPreEmp_TextChanged(sender As Object, e As EventArgs) Handles txt16grosstotPreEmp.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16gross2_GotFocus(sender As Object, e As EventArgs) Handles txt16gross2.GotFocus
        Call CtrlGotFocus(txt16gross2)
    End Sub

    Private Sub txt16gross2_LostFocus(sender As Object, e As EventArgs) Handles txt16gross2.LostFocus
        Call CtrlLostFocus(txt16gross2)
    End Sub

    Private Sub txt16ProfTax_TextChanged(sender As Object, e As EventArgs) Handles txt16ProfTax.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16gross1_GotFocus(sender As Object, e As EventArgs) Handles txt16gross1.GotFocus
        Call CtrlGotFocus(txt16gross1)
    End Sub

    Private Sub txt16gross1_LostFocus(sender As Object, e As EventArgs) Handles txt16gross1.LostFocus
        Call CtrlLostFocus(txt16gross1)
    End Sub

    Private Sub txt16bal_TextChanged(sender As Object, e As EventArgs) Handles txt16bal.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16TotalTaxableIncome_TextChanged(sender As Object, e As EventArgs) Handles txt16TotalTaxableIncome.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16grosstotCurEmp_LostFocus(sender As Object, e As EventArgs) Handles txt16grosstotCurEmp.LostFocus
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16grosstotCurEmp_GotFocus(sender As Object, e As EventArgs) Handles txt16grosstotCurEmp.GotFocus
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16grosstotPreEmp_GotFocus(sender As Object, e As EventArgs) Handles txt16grosstotPreEmp.GotFocus
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16grosstotPreEmp_LostFocus(sender As Object, e As EventArgs) Handles txt16grosstotPreEmp.LostFocus
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16TaxableSalary_TextChanged(sender As Object, e As EventArgs) Handles txt16TaxableSalary.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16ProfTax_LostFocus(sender As Object, e As EventArgs) Handles txt16ProfTax.LostFocus
        Call CtrlLostFocus(txt16ProfTax)
    End Sub

    Private Sub txt16ProfTax_GotFocus(sender As Object, e As EventArgs) Handles txt16ProfTax.GotFocus
        Call CtrlGotFocus(txt16ProfTax)
    End Sub

    Private Sub txt16GTI_TextChanged(sender As Object, e As EventArgs) Handles txt16GTI.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16TotalTaxableIncome_GotFocus(sender As Object, e As EventArgs) Handles txt16TotalTaxableIncome.GotFocus

    End Sub

    Private Sub txt16TotalTaxableIncome_LostFocus(sender As Object, e As EventArgs) Handles txt16TotalTaxableIncome.LostFocus

    End Sub

    Private Sub txt16OtherIVA_TextChanged(sender As Object, e As EventArgs) Handles txt16OtherIVA.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txtTotalDedct_GotFocus(sender As Object, e As EventArgs) Handles txtTotalDedct.GotFocus
        Call CtrlGotFocus(txtTotalDedct)
    End Sub

    Private Sub txtTotalDedct_LostFocus(sender As Object, e As EventArgs) Handles txtTotalDedct.LostFocus
        Call CtrlLostFocus(txtTotalDedct)
    End Sub

    Private Sub txt16Tax_TextChanged(sender As Object, e As EventArgs) Handles txt16Tax.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16TaxableSalary_LostFocus(sender As Object, e As EventArgs) Handles txt16TaxableSalary.LostFocus
        Call CtrlLostFocus(txt16TaxableSalary)
    End Sub

    Private Sub txt16TaxableSalary_GotFocus(sender As Object, e As EventArgs) Handles txt16TaxableSalary.GotFocus
        Call CtrlGotFocus(txt16TaxableSalary)
    End Sub

    Private Sub txt16Surcharge_TextChanged(sender As Object, e As EventArgs) Handles txt16Surcharge.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16GTI_LostFocus(sender As Object, e As EventArgs) Handles txt16GTI.LostFocus
        Call CtrlLostFocus(txt16GTI)
    End Sub

    Private Sub txt16GTI_GotFocus(sender As Object, e As EventArgs) Handles txt16GTI.GotFocus
        Call CtrlGotFocus(txt16GTI)
    End Sub

    Private Sub txt16EduCess_TextChanged(sender As Object, e As EventArgs) Handles txt16EduCess.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16OtherIVA_LostFocus(sender As Object, e As EventArgs) Handles txt16OtherIVA.LostFocus
        Call CtrlLostFocus(txt16OtherIVA)
    End Sub

    Private Sub txt16OtherIVA_GotFocus(sender As Object, e As EventArgs) Handles txt16OtherIVA.GotFocus
        Call CtrlGotFocus(txt16OtherIVA)
    End Sub

    Private Sub txt16Relief_TextChanged(sender As Object, e As EventArgs) Handles txt16Relief.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16Tax_LostFocus(sender As Object, e As EventArgs) Handles txt16Tax.LostFocus
        Call CtrlLostFocus(txt16Tax)
    End Sub

    Private Sub txt16Tax_GotFocus(sender As Object, e As EventArgs) Handles txt16Tax.GotFocus
        Call CtrlGotFocus(txt16Tax)
    End Sub

    Private Sub txt16TotalTax_TextChanged(sender As Object, e As EventArgs) Handles txt16TotalTax.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16Surcharge_GotFocus(sender As Object, e As EventArgs) Handles txt16Surcharge.GotFocus
        Call CtrlGotFocus(txt16Surcharge)
    End Sub

    Private Sub txt16Surcharge_LostFocus(sender As Object, e As EventArgs) Handles txt16Surcharge.LostFocus
        Call CtrlLostFocus(txt16Surcharge)
    End Sub

    Private Sub txt16EduCess_LostFocus(sender As Object, e As EventArgs) Handles txt16EduCess.LostFocus
        Call CtrlLostFocus(txt16EduCess)
    End Sub

    Private Sub txt16TDS1_TextChanged(sender As Object, e As EventArgs) Handles txt16TDS1.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16EduCess_GotFocus(sender As Object, e As EventArgs) Handles txt16EduCess.GotFocus
        Call CtrlGotFocus(txt16EduCess)
    End Sub

    Private Sub txt16Relief_LostFocus(sender As Object, e As EventArgs) Handles txt16Relief.LostFocus
        Call CtrlLostFocus(txt16Relief)
    End Sub

    Private Sub txt16TDS2_TextChanged(sender As Object, e As EventArgs) Handles txt16TDS2.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16Relief_GotFocus(sender As Object, e As EventArgs) Handles txt16Relief.GotFocus
        Call CtrlGotFocus(txt16Relief)
    End Sub

    Private Sub txt16TotalTax_LostFocus(sender As Object, e As EventArgs) Handles txt16TotalTax.LostFocus
        Call CtrlLostFocus(txt16TotalTax)
    End Sub

    Private Sub txt16NetTax_TextChanged(sender As Object, e As EventArgs) Handles txt16NetTax.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16TotalTax_MouseHover(sender As Object, e As EventArgs) Handles txt16TotalTax.MouseHover

    End Sub

    Private Sub txt16TotalTax_GotFocus(sender As Object, e As EventArgs) Handles txt16TotalTax.GotFocus
        Call CtrlGotFocus(txt16TotalTax)
    End Sub

    Private Sub txt16totalTDS_TextChanged(sender As Object, e As EventArgs) Handles txt16totalTDS.TextChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub txt16TDS1_GotFocus(sender As Object, e As EventArgs) Handles txt16TDS1.GotFocus
        Call CtrlGotFocus(txt16TDS1)
    End Sub

    Private Sub txt16TDS1_LostFocus(sender As Object, e As EventArgs) Handles txt16TDS1.LostFocus
        Call CtrlLostFocus(txt16TDS1)
    End Sub

    Private Sub txt16TDS2_GotFocus(sender As Object, e As EventArgs) Handles txt16TDS2.GotFocus
        Call CtrlGotFocus(txt16TDS2)
    End Sub

    Private Sub txt16TDS2_LostFocus(sender As Object, e As EventArgs) Handles txt16TDS2.LostFocus
        Call CtrlLostFocus(txt16TDS2)
    End Sub

    Private Sub txt16NetTax_LostFocus(sender As Object, e As EventArgs) Handles txt16NetTax.LostFocus
        Call CtrlLostFocus(txt16NetTax)
    End Sub

    Private Sub mnucontext_Click(sender As Object, e As EventArgs) Handles mnucontext.Click
        If UCase(Me.ActiveControl.Name) = "GRD16ALLOW" Then
            If grd16allow.Rows.Count = 0 Then Exit Sub
            grd16allow.Rows.Add()        ', grd16allow.Row
        ElseIf UCase(Me.ActiveControl.Name) = "GRD16OTHERINCOME" Then
            If grd16otherIncome.Rows.Count = 0 Then Exit Sub
            grd16otherIncome.Rows.Add()    ', grd16otherIncome.Row
        ElseIf UCase(Me.ActiveControl.Name) = "GRD16OTHERIVA" Then
            If grd16OtherIVA.Rows.Count = 0 Then Exit Sub
            grd16OtherIVA.Rows.Add()    ', grd16OtherIVA.Row
        ElseIf UCase(Me.ActiveControl.Name) = "GRD1680C" Then
            If grd1680c.Rows.Count = 0 Then Exit Sub
            grd1680c.Rows.Add()   ', grd1680c.Row
        ElseIf UCase(Me.ActiveControl.Name) = "GRD16MANUALTAX" Then
            If grd16ManualTax.Rows.Count = 0 Then Exit Sub
            grd16ManualTax.Rows.Add()  ', grd16ManualTax.Row
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If UCase(Me.ActiveControl.Name) = "GRD16ALLOW" Then
            If grd16allow.Rows.Count = 0 Then Exit Sub
            grd16allow.Rows.Remove(grd16allow.SelectedRows(0))
            If grd16allow.Rows.Count = 1 Then
                grd16allow.Rows.Add()

            End If
        ElseIf UCase(Me.ActiveControl.Name) = "GRD16OTHERINCOME" Then
            If grd16otherIncome.Rows.Count = 0 Then Exit Sub
            grd16otherIncome.Rows.Remove(grd16otherIncome.SelectedRows(0))
            If grd16otherIncome.Rows.Count = 1 Then
                grd16otherIncome.Rows.Add()
            End If
        ElseIf UCase(Me.ActiveControl.Name) = "GRD16OTHERIVA" Then
            If grd16OtherIVA.Rows.Count = 0 Then Exit Sub
            grd16OtherIVA.Rows.Remove(grd16OtherIVA.SelectedRows(0))
            If grd16OtherIVA.Rows.Count = 1 Then
                grd16OtherIVA.Rows.Add()
            End If
        ElseIf UCase(Me.ActiveControl.Name) = "GRD1680C" Then
            If grd1680c.Rows.Count = 0 Then Exit Sub
            grd1680c.Rows.Remove(grd1680c.SelectedRows(0))
            If grd1680c.Rows.Count = 1 Then
                grd1680c.Rows.Add()
            End If
        ElseIf UCase(Me.ActiveControl.Name) = "GRD16MANUALTAX" Then
            If grd16ManualTax.Rows.Count = 0 Or grd16ManualTax.Rows.Count = grd16ManualTax.Rows.Count - 1 Then Exit Sub
            grd16ManualTax.Rows.Remove(grd16ManualTax.SelectedRows(0))
            If grd16ManualTax.Rows.Count = 1 Then
                grd16ManualTax.Rows.Add()
            End If
        ElseIf UCase(Me.ActiveControl.Name) = "GRD1680CCF" Then
            If grd1680CCF.Rows.Count = 0 Then Exit Sub
            grd1680CCF.Rows.Remove(grd1680CCF.SelectedRows(0))
            If grd1680CCF.Rows.Count = 1 Then
                grd1680CCF.Rows.Add()
            End If
        End If
    End Sub

    Private Sub txt16NetTax_GotFocus(sender As Object, e As EventArgs) Handles txt16NetTax.GotFocus
        Call CtrlGotFocus(txt16NetTax)
    End Sub

    Private Sub txt16totalTDS_GotFocus(sender As Object, e As EventArgs) Handles txt16totalTDS.GotFocus
        Call CtrlGotFocus(txt16totalTDS)
    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click

    End Sub

    Private Sub Label47_Click(sender As Object, e As EventArgs) Handles Label47.Click

    End Sub

    Private Sub txt16totalTDS_LostFocus(sender As Object, e As EventArgs) Handles txt16totalTDS.LostFocus
        Call CtrlLostFocus(txt16totalTDS)
    End Sub

    Private Sub txtgrd80CCFCal_TextChanged(sender As Object, e As EventArgs) Handles txtgrd80CCFCal.TextChanged

    End Sub

    Private Sub grd16OtherIVA_KeyUp(sender As Object, e As KeyEventArgs) Handles grd16OtherIVA.KeyUp
        If e.KeyCode = Keys.Down Then
            '* If grd16OtherIVA.Row = grd16OtherIVA.Rows.Count - 1 Then
            'If Trim(grd16OtherIVA.TextMatrix(grd16OtherIVA.Rows.Count - 1, 0)) <> vbNullString And Trim(grd16OtherIVA.TextMatrix(grd16OtherIVA.Rows - 1, 3)) <> vbNullString Then
            'grd16OtherIVA.Rows = grd16OtherIVA.Rows.Count + 1
            'grd16OtherIVA.Select grd16OtherIVA.Rows - 1, 0
            '  End If
            'End If
        End If
    End Sub

    Private Sub grd16allow_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd16allow.CellContentClick

    End Sub

    Private Sub grd16OtherIVA_MouseUp(sender As Object, e As MouseEventArgs) Handles grd16OtherIVA.MouseUp
        'If e.Button = MouseButtons.Right Then
        '    If UCase(Me.ActiveControl.Name) <> "GRD16OTHERIVA" Then grd16OtherIVA.Focus()
        '    grd16OtherIVA.Select grd16OtherIVA.MouseRow, grd16OtherIVA.MouseCol
        '         popupmenu mnucontext
        '    End If
    End Sub


    Private Sub oForm16_BeforeSave(Cancel As Boolean) Handles oForm16.BeforeSave
        'check validatation before we save the data...if ok the use event prepareData for Save...
        If Trim(txtdesig.Text) = vbNullString Then
            MsgBox("Please enter proper designation of the employee", vbInformation, "Data Required")
            'tabForm16.Tab = 0
            Cancel = True
            Exit Sub
        ElseIf Val(txt16gross1.Text) = 0 Then
            MsgBox("Please enter some salary amount", vbInformation, "Data Required")
            txt16gross1.Focus()
            Cancel = True
            Exit Sub
        End If
        With grd16ManualTax
            For R = 1 To grd16ManualTax.Rows.Count - 2
                If Val(grd16ManualTax.Rows(R).Cells(1).Value) > 0 Then

                    If Len(.Rows(R).Cells(6).Value) < 6 Or Len(.Rows(R).Cells(6).Value) > 7 Then
                        Cancel = True
                        tabForm16.SelectedIndex = 5
                    End If
                    If Not IsDate(.Rows(R).Cells(7).Value) Then
                        Cancel = True
                        'tabForm16.Tab = 5
                        tabForm16.SelectedIndex = 5
                    Else
                        If CDate(.Rows(R).Cells(7).Value) < FromDate Or CDate(.Rows(R).Cells(7).Value) > ToDate Then
                            Cancel = True
                            'tabForm16.Tab = 5
                            tabForm16.SelectedIndex = 5
                        End If
                    End If

                    If .Rows(R).Cells(8).Value <= 0 Or .Rows(R).Cells(8).Value = vbNullString Then
                        Cancel = True
                        'tabForm16.Tab = 5
                        tabForm16.SelectedIndex = 5
                    End If
                End If
            Next R
        End With
    End Sub


    Private Sub oForm16_PrepareDataForSave(Cancel As Boolean) Handles oForm16.PrepareDataForSave
        With oForm16
            If xMode = "E" Then
                .F16ID = frmTDS24Q.lvwForm16.SelectedItems(0).SubItems(24).Text
            End If

            .did = cbo16DedName.SelectedValue
            .RetnID = frmTDS24Q.Tag
            .DDesgn = txtdesig.Text
            Dim dt As Date
            dt = txt16FrmDt.Text
            .EmpFromDt = dt.ToString("dd/MMM/yyyy")
            dt = txt16ToDt.Text
            .EmpToDt = dt.ToString("dd/MMM/yyyy")

            .Gross1 = Val(txt16gross1.Text)
            .Gross2 = Val(txt16gross2.Text)
            .Gross3 = Val(txt16gross3.Text)
            .Sec16ii = Val(txt16EntAllow.Text)
            .Sec16iii = Val(txt16ProfTax.Text)
            .TaxAmt = Val(txt16Tax.Text)
            .Surcharge = Val(txt16Surcharge.Text)
            .ECess = Val(txt16EduCess.Text)
            .Relief89 = Val(txt16Relief.Text)

            .TDSOnPerks = Val(txt16TDS2.Text)
            .TotalSalaryPreEmp = Val(txt16grosstotPreEmp.Text)
            .TDSAmtPreEmp = Val(txt16TaxPreEmp.Text)
            .HighRatePAN = IIf(chkHighRate.Checked = True, True, False)
        End With

        'Fill the allowances Collection..
        'Dim R As Long, c As Long
        'oAllowances.Clear()


        'For R = 1 To grd16allow.Rows.Count - 1
        '    oAllowances.Add(IIf(Val(grd16allow.Rows(R).Cells(2).ValueType) = 0, 0, grd16allow.Rows(R).Cells(2).Value), oForm16.F16ID, "A", grd16allow.Rows(R).Cells(0).Value, Val(grd16allow.Rows(R).Cells(1).Value), 0, 0)
        'Next R
        ''Fill the Other Income Collection..
        'oOthIncomes.Clear()

        'For R = 1 To grd16otherIncome.Rows.Count - 1
        '    oOthIncomes.Add(IIf(Val(grd16otherIncome.Rows(R).Cells(2).Value) = 0, 0, grd16otherIncome.Rows(R).Cells(2).Value), oForm16.F16ID, "O", grd16otherIncome.Rows(R).Cells(0).Value, Val(grd16otherIncome.Rows(R).Cells(1).Value), 0, 0)
        'Next R
        ''Fill the 80C_deduction Collection..
        'oSec80CDeductions.Clear()

        'For R = 1 To grd1680c.Rows.Count - 1
        '    oSec80CDeductions.Add(IIf(Val(grd1680c.Rows(R).Cells(3).Value) = 0, 0, grd1680c.Rows(R).Cells(3).Value), oForm16.F16ID, "E", grd1680c.Rows(R).Cells(0).Value, Val(grd1680c.Rows(R).Cells(1).Value), 0, Val(grd1680c.Rows(R).Cells(2).Value))
        'Next R

        ''Fill the 80CCF_deduction Collection..
        'oSec80CCFDeductions.Clear()

        'For R = 1 To grd1680CCF.Rows.Count - 1
        '    oSec80CCFDeductions.Add(IIf(Val(grd1680CCF.Rows(R).Cells(3).Value) = 0, 0, grd1680CCF.Rows(R).Cells(3).Value), "E", grd1680CCF.Rows(R).Cells(0).Value, Val(grd1680CCF.Rows(R).Cells(1).Value), 0, Val(grd1680CCF.Rows(R).Cells(2).Value), oForm16.F16ID)
        'Next R

        ''Fill the 80CCG_deduction Collection..
        'oSec80CCGDeductions.Clear()

        'For R = 1 To grd1680CCG.Rows.Count - 1
        '    oSec80CCGDeductions.Add(IIf(Val(grd1680CCG.Rows(R).Cells(3).Value) = 0, 0, grd1680CCG.Rows(R).Cells(3).Value), oForm16.F16ID, "G", grd1680CCG.Rows(R).Cells(0).Value, Val(grd1680CCG.Rows(R).Cells(1).Value), 0, Val(grd1680CCG.Rows(R).Cells(2).Value))
        'Next R

        ''Fill the Other VI-A_deduction Collection..
        'R = 1
        'oChapter6ADeductions.Clear()

        'For R = 1 To grd16OtherIVA.Rows.Count - 1
        '    oChapter6ADeductions.Add(IIf(Val(grd16OtherIVA.Rows(R).Cells(4)) = 0, 0, grd16OtherIVA.Rows(R).Cells(4).Value), oForm16.F16ID, "V", grd16OtherIVA.Rows(R).Cells(0).Value, Val(grd16OtherIVA.Rows(R).Cells(1).Value), Val(grd16OtherIVA.Rows(R).Cells(2).Value), Val(grd16OtherIVA.Rows(R).Cells(3).Value))
        'Next R
    End Sub

    Private Sub oForm16More_PrepareDataForSave(Cancel As Boolean, R As Long, grd As DataGridView) Handles oForm16More.PrepareDataForSave
        With oForm16More
            If xMode = "E" Then
                .F16ID = frmTDS24Q.lvwForm16.SelectedItems(0).SubItems(24).Text
            Else
                .F16ID = oForm16.F16ID
            End If

            '.ID = IIf(Val(grd16allow.Rows(R).Cells(2).Value) = 0, 0, grd16allow.Rows(R).Cells(2).Value)
            '.TypeOfDetail = TypeOfDetail
            .Particulars = grd.Rows(R).Cells(0).Value
            .GrossAmt = Val(grd.Rows(R).Cells(1).Value)
            .QualifyAmt = Val(grd.Rows(R).Cells(2).Value)
            .DeductibleAmt = Val(grd.Rows(R).Cells(3).Value)

        End With
    End Sub
    Public Sub CalculateSalaryNTax()
        Dim R As Long, TotalAllow As Double, TotalOthInc As Double, Total80CCF As Double
        Dim Total80C As Double, TotalChapVIA As Double, Total80CCG As Double
        'Calculate the Gross Total Salary
        txt16grosstotCurEmp.Text = Val(txt16gross1.Text) + Val(txt16gross2.Text) + Val(txt16gross3.Text) '+ Val(txt16grosstotCurEmp.Text) + Val(txt16grosstotPreEmp.Text)
        txt16grosstotBoth.Text = Val(txt16grosstotCurEmp.Text) + Val(txt16grosstotPreEmp.Text)
        'Calculate total allowance and deduct from Gross Total Salary...to get Balance...
        TotalAllow = 0
        For R = 0 To grd16allow.Rows.Count - 1
            TotalAllow = TotalAllow + grd16allow.Rows(R).Cells(1).Value
        Next R
        txtallow.Text = TotalAllow
        txt16bal.Text = Val(txt16grosstotBoth.Text) - TotalAllow
        'Calculate Total Deduction of section 16
        txtTotalDedct.Text = Val(txt16EntAllow.Text) + Val(txt16ProfTax.Text)
        'now, calculate total taxable salary...
        txt16TaxableSalary.Text = Val(txt16bal.Text) - Val(txtTotalDedct.Text)
        'now calculate the sum of other incomes and to taxable salary to get gross total income
        TotalOthInc = 0
        For R = 0 To grd16otherIncome.Rows.Count - 1
            TotalOthInc = TotalOthInc + grd16otherIncome.Rows(R).Cells(1).Value
        Next R
        txt16GTI.Text = Val(txt16TaxableSalary.Text) + TotalOthInc
        'now calculate the sum of other incomes and to taxable salary to get gross total income
        Total80C = 0
        For R = 0 To grd1680c.Rows.Count - 1
            Total80C = Total80C + grd1680c.Rows(R).Cells(2).Value
        Next R
        Total80CCF = 0
        For R = 0 To grd1680CCF.Rows.Count - 1
            Total80CCF = Total80CCF + grd1680CCF.Rows(R).Cells(2).Value
        Next R
        Total80CCG = 0
        For R = 0 To grd1680CCG.Rows.Count - 1
            Total80CCG = Total80CCG + grd1680CCG.Rows(R).Cells(2).Value
        Next R
        TotalChapVIA = 0
        For R = 0 To grd16OtherIVA.Rows.Count - 1
            TotalChapVIA = TotalChapVIA + grd16OtherIVA.Rows(R).Cells(3).Value
        Next R
        'Sapna 270510
        txt1680c.Text = Total80C
        txt1680CCF.Text = Total80CCF
        txt1680CCG.Text = Total80CCG
        txtT16OtherIVA.Text = TotalChapVIA
        txt16otherIncome.Text = TotalOthInc
        txt16OtherIVA.Text = Total80C + TotalChapVIA + Val(txt1680CCF.Text) + Val(txt1680CCG.Text)
        txt16TotalTaxableIncome.Text = Val(txt16GTI.Text) - Val(txt16OtherIVA.Text)
        txtincome.Text = Val(txt16TotalTaxableIncome.Text)
        'Now Calculate the taxes...
        ' txt16Tax.Text = Val(txt16TaxCurEmp.Text) + Val(txt16TaxPreEmp.Text)
        txt16TotalTax.Text = Val(txt16Tax.Text) + Val(txt16Surcharge.Text) + Val(txt16EduCess.Text)
        txt16NetTax.Text = Val(txt16TotalTax.Text) - Val(txt16Relief.Text)
        txt16totalTDS.Text = Val(txt16TDS1.Text) + Val(txt16TDS2.Text)
        txtPayRef.Text = Val(txt16NetTax.Text) - Val(txt16totalTDS.Text) - Val(txt16TaxPreEmp.Text)
        Check80CTotals(Total80C)
        'UpdateAutoCalcFields
        '   Autotaxcal
    End Sub


    Private Sub grd16allow_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles grd16allow.CellLeave
        'CalculateSalaryNTax()
    End Sub


    Private Sub grd16allow_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grd16allow.CellValueChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub grd1680c_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680c.CellValueChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub grd1680CCF_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680CCF.CellValueChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub grd16ManualTax_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd16ManualTax.CellContentClick

    End Sub

    Private Sub grd1680CCG_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grd1680CCG.CellValueChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub grd16otherIncome_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grd16otherIncome.CellValueChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub grd16OtherIVA_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grd16OtherIVA.CellValueChanged
        CalculateSalaryNTax()
    End Sub

    Private Sub cbo16DedName_Validating(sender As Object, e As CancelEventArgs) Handles cbo16DedName.Validating
        Dim rs As New DataSet
        If cbo16DedName.SelectedIndex = -1 Or Trim(cbo16DedName.Text) = "" Then
            MsgBox("Please select a deductee first..", vbInformation, "No Deductee Selected")
            tabForm16.Enabled = False
            e.Cancel = True
        Else
            tabForm16.Enabled = True
            Call frmTDS24Q.FillFrm16DataUsingDID(cbo16DedName.SelectedValue)
        End If

        rs = FetchDataSet("SELECT SUM(amtofpayment) FROM Deductee24Q WHERE DID=" & cbo16DedName.SelectedValue)
        txtAllTax.Text = rs.Tables(0).Rows(0)(0).ToString()
        rs.Dispose()
    End Sub

    Private Sub FRM16Detail_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'update the parent form...when we unload this form...so that the parent form reflects updated data...
        If Not frmTDS24Q.Tag = vbNullString Then
            Call frmTDS.Load24QData(frmTDS24Q.Tag)
        End If
        Me.Dispose()
    End Sub

    Private Sub grd16ManualTax_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grd16ManualTax.CellEndEdit
        Dim i As Integer, col As Integer
        col = e.ColumnIndex
        i = e.RowIndex
        With grd16ManualTax

            '    If i > -1 And .Rows(i).Cells(1).Value > 0 Then
            '        '        If Col = 5 Then
            '        '            If Len(.TextMatrix(Row, 5)) < 6 Or Len(.TextMatrix(Row, 5)) > 6 Then
            '        '                MsgBox "Invalid Cheque number!", 0 + 16
            '        '                .EditText = .TextMatrix(Row, 6)
            '        '                Exit Sub
            '        '            End If
            '        '        End If

            '        If col = 6 Then
            '            If Len(.Rows(i).Cells(6).Value) < 6 Or Len(.Rows(i).Cells(6).Value) > 7 Then
            '                MsgBox("Length of Bank BSR Code should be 7 Character.", 0 + 16)
            '                .Rows(i).Cells(6).Value = ""
            '                .CurrentCell = .Rows(i).Cells(6)
            '                .CurrentCell.Selected = True
            '                .BeginEdit(True)

            '                Exit Sub
            '            End If
            '        End If
            '        If col = 7 Then
            '            If Not IsDate(.Rows(i).Cells(7).Value) Then
            '                MsgBox("Invalid Date!", 0 + 16)
            '                .Rows(i).Cells(7).Value = "  /  /    "

            '                .CurrentCell = .Rows(i).Cells(7)
            '                .CurrentCell.Selected = True
            '                .BeginEdit(True)

            '                Exit Sub
            '            End If
            '            If .Rows(i).Cells(7).Value = "  /  /    " Or (CDate(.Rows(i).Cells(7).Value) < FromDate Or CDate(.Rows(i).Cells(7).Value) > ToDate) Then
            '                MsgBox("Invalid Date!", 0 + 16)
            '                .CurrentCell = .Rows(i).Cells(7)
            '                .CurrentCell.Selected = True
            '                .BeginEdit(True)
            '                Exit Sub
            '            End If
            '        End If
            '        .Rows(i).Cells(4).Value = Val(.Rows(i).Cells(1).Value) + Val(.Rows(i).Cells(2).Value) + Val(.Rows(i).Cells(3).Value)
            '    End If

            '    If col < .Columns.Count - 1 Then
            '        If col = 3 Then
            '            .Columns.Item(col + 2).Selected = True
            '        Else
            '            .Columns.Item(col + 1).Selected = True
            '        End If
            '    End If
            Dim totTDS As Double, totSUR As Double, totECESS As Double, tot As Double
            totTDS = 0
            totSUR = 0
            totECESS = 0
            tot = 0
            For i = 0 To .Rows.Count - 2
                totTDS = totTDS + Val(.Rows(i).Cells(1).Value)
                totSUR = totSUR + Val(.Rows(i).Cells(2).Value)
                totECESS = totECESS + Val(.Rows(i).Cells(3).Value)
                tot = tot + Val(.Rows(i).Cells(4).Value)
            Next
            Dim rw As Integer = .Rows.GetLastRow(DataGridViewElementStates.None)
            If .Rows.Count > 1 Then

                .Rows(rw).Cells(0).Style.ForeColor = Color.Red
                .Rows(rw).Cells(0).Value = "Total:"
                .Rows(rw).Cells(1).Style.ForeColor = Color.Red
                .Rows(rw).Cells(1).Value = Format(totTDS, "##0")
                .Rows(rw).Cells(2).Style.ForeColor = Color.Red
                .Rows(rw).Cells(2).Value = Format(totSUR, "##0")
                .Rows(rw).Cells(3).Style.ForeColor = Color.Red
                .Rows(rw).Cells(3).Value = Format(totECESS, "##0")
                .Rows(rw).Cells(4).Value = Format(tot, "##0")
                .Rows(rw).Cells(4).Style.ForeColor = Color.Red
            End If
            txt16TDS1.Text = grdchallanDetails.Rows(grdchallanDetails.Rows.GetLastRow(DataGridViewElementStates.None)).Cells(4).Value + grd16ManualTax.Rows(rw).Cells(4).Value
        End With

    End Sub

    Private Sub grd16ManualTax_Validating(sender As Object, e As CancelEventArgs) Handles grd16ManualTax.Validating
        'Dim i As Integer, totTDS As Double, totSUR As Double, totECESS As Double
        'With grd16ManualTax
        '    For i = 0 To .Rows.Count - 1
        '        totTDS = totTDS + Val(.Rows(i).Cells(1).Value)
        '        totSUR = totSUR + Val(.Rows(i).Cells(2).Value)
        '        totECESS = totECESS + Val(.Rows(i).Cells(3).Value)

        '    Next

        '    Dim rw As Integer = .Rows.GetLastRow(DataGridViewElementStates.None)
        '    .Rows(rw).Cells(0).Style.ForeColor = Color.Red
        '    .Rows(rw).Cells(0).Value = "Total:"
        '    .Rows(rw).Cells(1).Style.ForeColor = Color.Red
        '    .Rows(rw).Cells(1).Value = Format(totTDS, "##0")
        '    .Rows(rw).Cells(2).Style.ForeColor = Color.Red
        '    .Rows(rw).Cells(2).Value = Format(totSUR, "##0")
        '    .Rows(rw).Cells(3).Style.ForeColor = Color.Red
        '    .Rows(rw).Cells(3).Value = Format(totECESS, "##0")
        '    'txt16TDS1.Text = grdchallanDetails.Rows(r).Cells(4).Value + grd16ManualTax.Rows(grd16ManualTax.RowCount - 1).Cells(4).Value

        'End With

    End Sub

    Private Sub grd16ManualTax_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles grd16ManualTax.CellValidating
        Dim i As Integer, col As Integer
        col = e.ColumnIndex
        i = e.RowIndex

        With grd16ManualTax

            If i > -1 And .Rows(i).Cells(1).Value > 0 And .Rows.GetLastRow(DataGridViewElementStates.None) <> i Then


                If col = 6 Then
                    If Len(e.FormattedValue) < 6 Or Len(e.FormattedValue) > 7 Then
                        MsgBox("Length of Bank BSR Code should be 7 Character.", 0 + 16)
                        e.Cancel = True


                        Exit Sub
                    End If
                End If
                If col = 7 Then
                    If Not IsDate(e.FormattedValue) Then
                        MsgBox("Invalid Date!", 0 + 16)
                        e.Cancel = True


                        Exit Sub
                    End If
                    If e.FormattedValue = "  /  /    " Or (CDate(e.FormattedValue) < FromDate Or CDate(e.FormattedValue) > ToDate) Then
                        MsgBox("Invalid Date!", 0 + 16)
                        e.Cancel = True

                        Exit Sub
                    End If
                End If
                .Rows(i).Cells(4).Value = Val(.Rows(i).Cells(1).Value) + Val(.Rows(i).Cells(2).Value) + Val(.Rows(i).Cells(3).Value)
            End If

            If col < .Columns.Count - 1 Then
                If col = 3 Then
                    .Columns.Item(col + 2).Selected = True
                Else
                    .Columns.Item(col + 1).Selected = True
                End If
            End If
        End With
    End Sub

    Private Sub grd16ManualTax_KeyDown(sender As Object, e As KeyEventArgs) Handles grd16ManualTax.KeyDown

    End Sub

    Private Sub grd16ManualTax_KeyUp(sender As Object, e As KeyEventArgs) Handles grd16ManualTax.KeyUp
        Dim R As Integer
        With grd16ManualTax
            R = .CurrentCell.RowIndex
            If e.KeyCode = Keys.Down Then

                If R = .Rows.GetLastRow(DataGridViewElementStates.None) Then

                    If .Rows(R - 1).Cells(1).Value > 0 Then
                        If .Rows(R - 1).Cells(6).Value <> Nothing And .Rows(R - 1).Cells(7).Value <> Nothing And .Rows(R - 1).Cells(8).Value <> Nothing Then
                            .Rows.Add()
                            .CurrentCell = .Rows(.Rows.GetLastRow(DataGridViewElementStates.None) - 1).Cells(1)
                            .Rows(.Rows.GetLastRow(DataGridViewElementStates.None) - 1).Cells(1).Selected = True

                        End If
                    End If
                End If
            End If
        End With
    End Sub


    Private Sub TabPage5_Click(sender As Object, e As EventArgs) Handles TabPage5.Click

    End Sub

    Private Sub TabPage5_Enter(sender As Object, e As EventArgs) Handles TabPage5.Enter
        Dim r As Integer
        r = grd16ManualTax.Rows.GetLastRow(DataGridViewElementStates.None)
        If r > 0 Then
            grd16ManualTax.CurrentCell = grd16ManualTax.Rows(r - 1).Cells(1)
            grd16ManualTax.Rows(r - 1).Cells(1).Selected = True
        Else
            grd16ManualTax.CurrentCell = grd16ManualTax.Rows(r).Cells(1)
            grd16ManualTax.Rows(r).Cells(1).Selected = True
        End If
    End Sub

    Private Sub TabPage5_GotFocus(sender As Object, e As EventArgs) Handles TabPage5.GotFocus

    End Sub
End Class