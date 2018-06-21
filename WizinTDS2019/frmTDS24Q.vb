﻿
Imports System.ComponentModel
Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.Math
Imports System.Drawing.Text

Public Class frmTDS24Q

    Dim Counter As Long
    Dim Counter1 As Long
    Public quter As String
    Public did As Long
    Public CO As String
    Dim WithEvents oChln As ClsChallan24QObj
    Dim WithEvents oDed As clsDeductee24QObj
    Dim WithEvents oSD As ClsSalaryDetail24QObj
    Dim AutoCalcReqd As Boolean
    Dim dedcboIndex As Long
    Dim strFrmCaption As String
    Dim AllowBSREntry As Boolean
    Dim F16ID, RETN As Integer
    'Dim ChqBoxChecked As Integer
    'Dim ChqBoxUnChecked As Integer
    'Dim sec() As String

    Private Sub cmdConvertForm24Q_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConvertForm24Q.Click

        If BeforeConvert() = True Then Exit Sub
        Me.Hide()
        frmConUtility.ShowDialog()
        Me.Show()

    End Sub
    Private Function BeforeConvert() As Boolean
        'Dim fso As New Scripting.FileSystemObject
        Me.Cursor = Cursors.WaitCursor
        If frmRegister.Mylock.RegisteredUser = False Then
            Call MsgBox("You are using an unregistered software so cannot proceed." _
        & vbCrLf & "For registration please contact JAK Infosolutions Pvt. Ltd., Nagpur." & vbCrLf & "Phone: 0712-2250009, 2251515." _
        , vbInformation, Application.ProductName)

            BeforeConvert = True
            GoTo cleanup
        End If
cleanup:
        Me.Cursor = Cursors.Default

        Exit Function
canerr:
        If Err.Number <> 32755 Then
            MsgBox(Err.Description, , Err.Number)
        End If
        GoTo cleanup
    End Function

    Private Sub frmTDS24Q_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        'Me.EnterTab(e)
        frmCoMst.EnterTab(e)
    End Sub
    Public Sub EnterTab(ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub txtAmtDeducted_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtDeducted.TextChanged
        CalcTotalTDS()
    End Sub

    Private Sub txtDedUName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoName.Enter
        txtCoName.BackColor = Color.LightYellow

    End Sub

    Private Sub txtDedUName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoName.Leave
        txtCoName.BackColor = Color.White
    End Sub

    Private Sub txtCoAdd1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd1.Enter
        txtCoAdd1.BackColor = Color.LightYellow
    End Sub

    Private Sub txtCoAdd1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd1.Leave
        txtCoAdd1.BackColor = Color.White
    End Sub

    Private Sub txtCoAdd2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd2.Leave
        txtCoAdd2.BackColor = Color.White
    End Sub

    Private Sub txtCoAdd2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd2.Enter
        txtCoAdd2.BackColor = Color.LightYellow
    End Sub

    Private Sub txtCoAdd3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd3.Leave
        txtCoAdd3.BackColor = Color.White
    End Sub

    Private Sub txtCoAdd3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd3.Enter
        txtCoAdd3.BackColor = Color.LightYellow
    End Sub

    Private Sub txtCoAdd4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd4.Enter
        txtCoAdd4.BackColor = Color.LightYellow
    End Sub

    Private Sub txtCoAdd4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd4.Leave
        txtCoAdd4.BackColor = Color.White
    End Sub

    Private Sub txtCoAdd5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd5.Enter
        txtCoAdd5.BackColor = Color.LightYellow
    End Sub

    Private Sub txtCoAdd5_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoAdd5.Leave
        txtCoAdd5.BackColor = Color.White
    End Sub

    Private Sub cmbdeducState_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCoState.Enter
        cboCoState.BackColor = Color.LightYellow
    End Sub

    Private Sub cmbdeducState_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCoState.Leave
        cboCoState.BackColor = Color.White
    End Sub

    Private Sub txtdeducPin_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoPin.Leave
        txtCoPin.BackColor = Color.White
    End Sub

    Private Sub txtdeducPin_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoPin.Enter
        txtCoPin.BackColor = Color.LightYellow
    End Sub

    Private Sub txtdeducTan_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoTAN.Enter
        txtCoTAN.BackColor = Color.LightYellow
    End Sub

    Private Sub txtdeducTan_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoTAN.Leave
        txtCoTAN.BackColor = Color.White
    End Sub

    Private Sub txtdeducPAN_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoPAN.Leave
        txtCoPAN.BackColor = Color.White
    End Sub

    Private Sub txtdeducPAN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCoPAN.Enter
        txtCoPAN.BackColor = Color.LightYellow
    End Sub

    Private Sub cmbDeduStatus_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGovtDetails.Enter
        CtrlGotFocusC(cboGovtDetails)
    End Sub

    Private Sub cmbDeduStatus_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGovtDetails.Leave
        CtrlLostFocus(cboGovtDetails)
    End Sub

    Private Sub cboChallanSection_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboChallanSection.Enter
        ' cboChallanSection.BackColor = Color.LightYellow
        CtrlGotFocusC(cboChallanSection)
    End Sub

    Private Sub cboChallanSection_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboChallanSection.Leave

    End Sub

    Private Sub txtAmtDeducted_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtDeducted.Leave

    End Sub

    Private Sub txtAmtDeducted_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtDeducted.Enter

        CtrlGotFocus(txtAmtDeducted)
    End Sub

    Private Sub txtSurcharge_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSurcharge.Enter

    End Sub

    Private Sub txtSurcharge_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSurcharge.Leave

    End Sub

    Private Sub txtECess_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtECess.Enter

    End Sub

    Private Sub txtECess_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtECess.Leave

    End Sub

    Private Sub txtIntt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIntt.Enter

    End Sub

    Private Sub txtIntt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIntt.Leave

    End Sub

    Private Sub txtOthers_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOthers.Enter

    End Sub

    Private Sub txtOthers_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOthers.Leave

    End Sub

    Private Sub txtFees_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFees.Enter

    End Sub

    Private Sub txtFees_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFees.Leave

    End Sub

    Private Sub txtTotalTDS_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalTDS.Enter
        txtTotalTDS.BackColor = Color.LightYellow
    End Sub

    Private Sub txtTotalTDS_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalTDS.Leave
        txtTotalTDS.BackColor = Color.White
    End Sub

    Private Sub txtChqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChqNo.Leave
        txtChqNo.BackColor = Color.White
    End Sub

    Private Sub txtChqNo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChqNo.Enter
        'txtChqNo.BackColor = Color.LightYellow
        CtrlGotFocus(txtChqNo)
    End Sub

    Private Sub txtChallanNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChallanNo.Leave

    End Sub

    Private Sub txtChallanNo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChallanNo.Enter

    End Sub


    Private Sub cboBankBrCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBankBrCode.Leave

    End Sub

    Private Sub cboBankBrCode_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBankBrCode.Enter
        'cboBankBrCode.BackColor = Color.LightYellow
        CtrlGotFocusC(cboBankBrCode)
    End Sub

    Private Sub txtTranVouNo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTranVouNo.Enter
        ' txtTranVouNo.BackColor = Color.LightYellow
        CtrlGotFocus(txtTranVouNo)
    End Sub

    Private Sub txtTranVouNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTranVouNo.Leave
        txtTranVouNo.BackColor = Color.White
    End Sub

    Private Sub txtChallanRemark_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChallanRemark.Leave

    End Sub

    Private Sub txtChallanRemark_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtChallanRemark.Enter

    End Sub

    Private Sub txtAIntt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAIntt.Leave

    End Sub

    Private Sub txtAIntt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAIntt.Enter

    End Sub

    Private Sub cboDedSection_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDedSection.Leave
        CtrlLostFocus(cboDedSection)
        Call fillcboDedChallan1(cboDedSection.Text)
    End Sub

    Private Sub cboDedSection_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDedSection.Enter
        'cboDedSection.BackColor = Color.LightYellow
        CtrlGotFocusC(cboDedSection)
    End Sub

    Private Sub txtDedPAN_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDedPAN.Leave
        txtDedPAN.BackColor = Color.White
    End Sub

    Private Sub txtDedPAN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDedPAN.Enter
        txtDedPAN.BackColor = Color.LightYellow

    End Sub

    Private Sub cboRemark_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRemark.Enter
        'cboRemark.BackColor = Color.LightYellow
        CtrlGotFocusC(cboRemark)
    End Sub

    Private Sub cboRemark_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRemark.Leave
        CtrlLostFocus(cboRemark)
    End Sub

    Private Sub txtCertNo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCertNo.Enter

    End Sub

    Private Sub txtCertNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCertNo.Leave

    End Sub

    Private Sub txtAmtPay_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtPay.Leave

    End Sub

    Private Sub txtAmtPay_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtPay.Enter

    End Sub



    Private Sub txtTDSAmt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTDSAmt.Leave

        CalcTotalDeducteeTDS()
    End Sub

    Private Sub txtTDSAmt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTDSAmt.Enter

    End Sub

    Private Sub txtDSurchrge_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDSurchrge.Enter

    End Sub

    Private Sub txtDSurchrge_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDSurchrge.Leave

    End Sub

    Private Sub txtDECess_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDECess.Enter

    End Sub

    Private Sub txtDECess_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDECess.Leave

    End Sub

    Private Sub txtTotalTaxDeducted_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalTaxDeducted.Leave
        txtTotalTaxDeducted.BackColor = Color.White
    End Sub

    Private Sub txtTotalTaxDeducted_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalTaxDeducted.Enter
        txtTotalTaxDeducted.BackColor = Color.LightYellow
    End Sub

    Private Sub txtTotalTaxDeposited_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalTaxDeposited.Leave
        txtTotalTaxDeposited.BackColor = Color.LightYellow
    End Sub

    Private Sub txtTotalTaxDeposited_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalTaxDeposited.Enter
        txtTotalTaxDeposited.BackColor = Color.LightYellow
    End Sub

    Private Sub cboChallanNo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboChallanNo.Enter
        'cboChallanNo.BackColor = Color.LightYellow
        CtrlGotFocusC(cboChallanNo)
    End Sub

    Private Sub cboChallanNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboChallanNo.Leave


        Call CtrlLostFocus(cboChallanNo)
        If cboChallanNo.SelectedIndex = -1 Or cboChallanNo.Text = "" Or dtpTDSDedDt.Text = "  /  /" Then
            Exit Sub
        End If
        Dim ctrchallanNo
        Dim nmonth As Integer
        Dim dt As Date, cdt As Date
        If cboChallanNo.Text <> "" Then
            ctrchallanNo = Split(cboChallanNo.Text, "-")
            If dtpTDSDedDt.Text <> "  /  /" Then
                dt = dtpTDSDedDt.Text
                cdt = Trim(ctrchallanNo(1))
                If cdt < dt Then
                    MsgBox("Date of deduction is greater than date of challan. Please check again", vbOKOnly)
                End If
            End If
            '    If (Month(dtpTDSDedDt.Text) <> Month(ctrchallanNo(1))) Then
            '    If Month(dtpTDSDedDt) <> 3 Then
            '    nmonth = Month(dtpTDSDedDt) + 1
            '    If CDate("07/" & Format(nmonth, "##") & "/" & Year(dtpTDSDedDt)) < CDate(ctrchallanNo(1)) Then
            '        MsgBox "Date of Deposite of TDS is above due dates, resulting in Interest Liability. Please check again", vbOKOnly
            '    End If
            If (Month(dtpTDSDedDt.Text) <> Month(ctrchallanNo(1))) Then
                If Month(dtpTDSDedDt.Text) <> 3 Then
                    If Month(dtpTDSDedDt.Text) <> 12 Then
                        nmonth = Month(dtpTDSDedDt.Text) + 1
                        If CDate("07/" & Format(nmonth, "##") & "/" & Year(dtpTDSDedDt.Text)) < CDate(ctrchallanNo(1)) Then
                            MsgBox("Date of Deposite of TDS is above due dates, resulting in Interest Liability. Please check again", vbOKOnly)
                        End If
                    Else
                        nmonth = 1
                        If CDate("07/" & Format(nmonth, "##") & "/" & Year(dtpTDSDedDt.Text) + 1) < CDate(ctrchallanNo(1)) Then
                            MsgBox("Date of Deposite of TDS is above due dates, resulting in Interest Liability. Please check again", vbOKOnly)
                        End If
                    End If
                Else
                    nmonth = Month(dtpTDSDedDt.Text) + 1
                    If CDate(ctrchallanNo(1)) > CDate("30/" & nmonth & "/" & Year(dtpAmtPayDt.Text)) Then
                        MsgBox("Date of Deposite of TDS is above due dates, resulting in Interest Liability. Please check again", vbOKOnly)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtAOthers_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAOthers.Enter

    End Sub

    Private Sub txtAOthers_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAOthers.Leave
        'txtAOthers.BackColor = Color.White
        'cmdAdd.Focus()
    End Sub

    Private Sub frmTDS24Q_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oCoMst As New clsCoMst
        oCoMst = oCoMst.FetchCo(selectedcoid)
        ' Me.Location = New Point(221, 81)
        Me.Location = New Point(180, 210)
        Dim nds As New DataSet

        'Set the dates..
        dtpAmtPayDt.AutoSize = False
        dtpAmtPayDt.Height = 20
        dtpChallanDate.AutoSize = False
        dtpChallanDate.Height = 10
        dtpTDSDedDt.AutoSize = False
        'dtpAmtPayDt.Height = 20

        dtpChallanDate.Text = (Now().ToString("dd/MM/yy"))
        dtpAmtPayDt.Text = FromDateQ.ToString("dd/MM/yy")
        dtpTDSDedDt.Text = Format(FromDateQ, "dd/MM/yy")
        txtSDEmpFrm.Text = Format(FromDate, "dd/MM/yy")
        txtSDEmpTo.Text = Format(ToDate, "dd/MM/yy")
        txtContriTo.Text = (Now().ToString("dd/MM/yy"))
        txtContriFrm.Text = (Now().ToString("dd/MM/yy"))
        'Set next btn to false of tab0
        'cmdNext.Enabled = False

        'Fill Remark combo
        cboRemark.Items.Add("N")
        cboRemark.Items.Add("A")
        cboRemark.Items.Add("B")
        cboRemark.Items.Add("C")
        'cboRemark.Items.Add("T")
        'cboRemark.Items.Add("Y")
        'cboRemark.Items.Add("S")
        'cboRemark.Items.Add("Z")
        cboRemark.SelectedIndex = 0

        strFrmCaption = "Form No. 24Q "

        FillDeducteeCombo()
        FillGovtDedrType()
        'Code
        NormalMode()
        NormalModeSD()
        NormalModeDeductee()
        ClearChallanCtrls()
        clearDeducteeCtrls()
        ClearSDCtrls()
        CopyCoDetails()
        EnableDisableTabContents()
        lvwchallanHead()
        lvwDeducteeHead()
        lvwSDHead()
        lvwForm16Head()
        chkBookEntry_Click(sender, e)
        Counter = 0
        Select Case oCoMst.CoStatus
            Case "E", "H", "N", "S"
                chkSection1.Checked = True
            Case "B", "F", "J", "K", "M", "P", "Q", "T"
                chkSection2.Checked = True
            Case "A", "D", "G", "L"
                chkSection3.Checked = True
        End Select
        salary()
        SectionChecked()
        If cboDedSection.Items.Count > 0 Then
            cboDedSection.SelectedIndex = 0
            cboDedSection.Select()
        End If
        'SectionChecked()
        cmdBack.Enabled = False
        If cbo16Ded.Text = "" Then
            ChkRent.Enabled = False
            ChkIncomeFromHouseProperty.Enabled = False
            CHkSAFund.Enabled = False
        Else
            ChkRent.Enabled = True
            ChkIncomeFromHouseProperty.Enabled = True
            CHkSAFund.Enabled = True
        End If
        tabMain.SelectedIndex = 0
    End Sub

    Private Sub salary()
        Dim j As Integer
        Dim sumDCount As Double
        Dim SumdTDSDeposited As Double
        sumDCount = lvwSD.Items.Count
        txtSDcount.Text = "Total Deductee's Records =" & sumDCount
        For j = 1 To lvwSD.Items.Count - 1
            SumdTDSDeposited = SumdTDSDeposited + Val(lvwSD.Items(j).SubItems(17).Text)
        Next j
        txttotdepos.Text = "Total Tax Deposited=" & SumdTDSDeposited
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub
    Private Sub FillDeducteeCombo()
        Dim nds As New DataSet
        nds = FetchDataSet("select DName,DId from DeductMst Where CoId = " & selectedcoid & "  ORDER BY DName ")
        cboDedName.DataSource = Nothing
        cboDedName.Items().Clear()
        cboSDDedName.Items().Clear()
        If nds.Tables(0).Rows.Count > 0 Then
            cboDedName.DataSource = nds.Tables(0)
            cboDedName.DisplayMember = "DName"
            cboDedName.ValueMember = "DId"
            cboSDDedName.DataSource = nds.Tables(0)
            cboSDDedName.DisplayMember = "DName"
            cboSDDedName.ValueMember = "DId"
        End If

        nds.Dispose()
    End Sub
    Private Sub FillGovtDedrType()
        Dim nds As New DataSet

        Dim sql As String
        sql = "Select DeductorTypeDescription from DeductorTypeMst "
        nds = FetchDataSet(sql)
        If nds.Tables(0).Rows.Count > 0 Then
            cboGovtDetails.DataSource = Nothing
            cboGovtDetails.Items().Clear()
            cboGovtDetails.DataSource = nds.Tables(0)
            cboGovtDetails.DisplayMember = "DeductorTypeDescription"

        End If
        nds.Dispose()
    End Sub

    Private Sub NormalMode()
        With Me
            .lvwChallan.Enabled = True
            .cmdAdd.Text = "Add"
            .cmdCnlEdit.Enabled = False
        End With
    End Sub

    Private Sub NormalModeDeductee()
        With Me
            .lvwDeductee.Enabled = True
            .cmdDedAdd.Text = "Add"
            .cmdDedCancel.Enabled = False
        End With
    End Sub

    Private Sub ClearChallanCtrls(Optional ClearBankDetails As Boolean = True)
        cboChallanSection.Text = ""
        txtAmtDeducted.Text = 0#
        txtSurcharge.Text = 0
        txtECess.Text = 0
        txtIntt.Text = 0
        txtOthers.Text = 0
        txtFees.Text = 0
        CalcTotalTDS()
        chkMinorHead.Checked = False ' vbUnchecked

        If ClearBankDetails = True Then
            txtChallanNo.Text = ""
            'dtpChallanDate = "__/__/__"
            txtChqNo.Text = ""
            'cboBankBrCode.Text = ""
            chkAddChg.Checked = False '= vbUnchecked
        End If
        txtChallanRemark.Text = vbNullString
        txtAIntt.Text = 0
        txtAOthers.Text = 0
        ChkAllocate.Checked = True '= vbChecked
        'lvwChallan.Items.Clear()
        'lvwDeductee.Items.Clear()
    End Sub

    Private Sub CalcTotalTDS()
        txtTotalTDS.Text = Val(txtAmtDeducted.Text) + Val(txtSurcharge.Text) +
                          Val(txtECess.Text) + Val(txtIntt.Text) + Val(txtOthers.Text) + Val(txtFees.Text)
    End Sub

    Private Sub clearDeducteeCtrls()
        'cboDedSection.Text = ""
        cboDedName.Text = ""
        txtDedPAN.Text = ""
        txtAmtPay.Text = 0
        'txtTDSRate.Text = ""
        txtTDSAmt.Text = 0
        txtDSurchrge.Text = 0
        txtDECess.Text = 0
        txtTotalTaxDeposited.Text = 0
        cboChallanNo.Text = ""
        cboRemark.SelectedIndex = 0
        txtCertNo.Text = ""
        txtTotalTaxDeposited.BackColor = Color.White
        If cboDedSection.Items.Count > 0 Then
            cboDedSection.SelectedIndex = 0
            cboDedSection.Select()
        End If
    End Sub

    Private Sub CopyCoDetails()
        With frmCoMst
            txtCoName.Text = .txtCoName.Text
            txtCoName.Tag = .txtCoName.Tag
            txtCoAdd1.Text = .txtCoAdd1.Text
            txtCoAdd2.Text = .txtCoAdd2.Text
            txtCoAdd3.Text = .txtCoAdd3.Text
            txtCoAdd4.Text = .txtCoAdd4.Text
            txtCoAdd5.Text = .txtCoAdd5.Text
            txtCoPin.Text = .txtCoPin.Text
            cboCoState.Text = .cboCoState.Text
            txtCoTAN.Text = .txtCoTAN.Text
            txtCoPAN.Text = .txtCoPAN.Text
            cboGovtDetails.Text = .cboGovtDetails.Text
            chkAddChg.Checked = .chkAddChg.Checked
        End With
    End Sub

    Private Sub EnableDisableTabContents()
        'Dim i As Long
        'enable disable controls..
        For i = 0 To tabMain.TabPages.Count - 1

            tabMain.TabPages.Item(i).Enabled = False
            tabMain.TabPages.Item(i).Visible = False
        Next i

        tabMain.SelectedTab.Visible = True
        tabMain.SelectedTab.Enabled = True
    End Sub

    Private Sub chkBookEntry_CheckedChanged(sender As Object, e As EventArgs) Handles chkBookEntry.CheckedChanged

    End Sub

    Private Sub chkBookEntry_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chkBookEntry.KeyPress
        'KeyAscii = CtrlKeyPress(chkBookEntry, KeyAscii, KeyPressDefault)
    End Sub

    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged
        On Error Resume Next
        Dim i As Long
        If tabMain.SelectedIndex <> 0 Then


            If SectionChecked() = False Then
                'No section was ticked in tab 0
                Call MsgBox("Cannot proceed if no section is selected from Deductor details." _
        & vbCrLf & "Please select at least one section under which you have deducted" _
        & vbCrLf & "tax at source and for which you are preparing this form." _
        , vbExclamation, "SELECT SECTION")
                tabMain.SelectTab(0)
                chkSection1.Focus()
                Counter = 10
                Timer1.Enabled = True
                Timer1.Interval = 100
                Timer1.Start()
            Else
                'Fill the section combos..
                cboChallanSection.Items.Clear()
                cboDedSection.Items.Clear()
                For Each chk As CheckBox In PanelCheckBox.Controls
                    i = i + 1
                    If chk.Checked = True Then
                        cboChallanSection.Items.Add(chk.Text)
                        cboChallanSection.SelectedIndex = i
                        cboDedSection.Items.Add(chk.Text)
                        cboDedSection.SelectedIndex = i

                    End If
                    'If i = 23 Then
                    '    Exit For
                    'End If
                Next chk

                'Timer2.Enabled = True
                'Timer2.Interval = 300
                'Timer2.Start()
            End If
        End If
        EnableDisableTabContents()
        cmdBack.Enabled = True
        Select Case tabMain.SelectedIndex
            Case 0
                chkSection1.Focus()
                cmdBack.Enabled = False
                cmdNext.Enabled = True
            Case 1
                'Update Challan Colors...
                Call RangDeChallans()
                cboChallanSection.Focus()
            Case 2
                cboDedSection.Focus()
                'cboDedSection.SelectedIndex = -1
                If cboDedSection.Items.Count > 0 Then
                    cboDedSection.SelectedIndex = 0
                End If
                cboDedName.SelectedIndex = -1
            Case 3
                cboSDDedName.Focus()
                cmdNext.Enabled = False
        End Select
    End Sub

    Private Function SectionChecked() As Boolean
        For Each chk As CheckBox In PanelCheckBox.Controls

            If chk.Checked = True Then

                cmdNext.Enabled = True

                SectionChecked = True

                Exit Function
                'End If
            End If
        Next chk
        SectionChecked = False
    End Function

    Private Sub RangDeChallans()
        On Error GoTo 0
        Dim Itm As ListViewItem
        'Dim rs As New ADODB.Recordset
        Dim nds As New DataSet
        For Each Itm In lvwChallan.Items
            nds = FetchDataSet("select sum(TotalTaxDeposited) as TTaxDep from Deductee24Q where ChallanID =" & Val(Itm.SubItems.Item(16).Text))
            If nds.Tables(0).Rows.Count > 0 Then
                'Records are present
                If (Val(Itm.SubItems.Item(1).Text) + Val(Itm.SubItems.Item(2).Text) + Val(Itm.SubItems.Item(3).Text)) <
            (IIf((nds.Tables(0).Rows(0)("TTaxDep").ToString() = ""), 0, nds.Tables(0).Rows(0)("TTaxDep"))) Then
                    'Challan amount is less than deductee amt...
                    Itm.ForeColor = Color.Red         'More amount allocated than reqd.
                    For Each subItm In Itm.SubItems
                        subItm.ForeColor = Color.Red        'color the sub items also
                    Next
                ElseIf (Val(Itm.SubItems.Item(1).Text) + Val(Itm.SubItems.Item(2).Text) + Val(Itm.SubItems.Item(3).Text)) >
            (IIf((nds.Tables(0).Rows(0)("TTaxDep").ToString() = ""), 0, nds.Tables(0).Rows(0)("TTaxDep"))) Then
                    'Challan amount is more than deductee amt...
                    Itm.ForeColor = Color.Green 'RGB(0, 128, 0) 'Unallocated amount pending in challan
                    For Each subItm In Itm.SubItems
                        subItm.ForeColor = Color.Green 'RGB(0, 128, 0)    'color the sub items also
                    Next
                Else
                    'Challan amount is equal to deductee amt...
                    Itm.ForeColor = Color.Black
                    For Each subItm In Itm.SubItems
                        subItm.ForeColor = Color.Black       'color the sub items also
                    Next
                End If
            End If
            nds.Dispose()
        Next
    End Sub

    Private Sub chkBookEntry_Click(sender As Object, e As EventArgs) Handles chkBookEntry.Click
        If cboGovDetIndex > -1 And cboGovDetIndex <= 7 Then
            If chkBookEntry.Checked = True Then
                txtChallanNo.Text = vbNullString
                txtChallanNo.Enabled = False
                cboBankBrCode.Enabled = True    'changed from false to true on 28/01/2013 for FVU 3.3
                cboBankBrCode.Text = ""
                txtChqNo.Enabled = False
                txtChqNo.Text = ""
                txtTranVouNo.Enabled = True
                AllowBSREntry = True
                Label23.Text = "Form 24G Rcpt No"
            Else
                txtTranVouNo.Text = vbNullString
                txtTranVouNo.Enabled = False
                txtChallanNo.Enabled = True
                cboBankBrCode.Enabled = True
                ' txtChqNo.Enabled = True
                AllowBSREntry = False
                Label23.Text = "Bank Br Code (BSR)"
            End If
        Else
            txtTranVouNo.Enabled = False
            chkBookEntry.Enabled = False
        End If
    End Sub

    Private Sub frmTDS24Q_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus

    End Sub

    'Private Sub chkSection0_CheckedChanged(sender As Object, e As EventArgs)
    '    If chkSection0.Checked = False Then

    '        chkSection0.Checked = chkSectionClick(chkSection0.Text)
    '    End If
    'End Sub


    Private Function chkSectionClick(txt As String) As Boolean

        If lvwChallan.FindItemWithText(txt) IsNot Nothing Or lvwDeductee.FindItemWithText(txt) IsNot Nothing Then
            Call MsgBox("You cannot uncheck this section, this is used either in challan or in deductee details." _
            & vbCrLf & "If you want to remove it anyway, please delete all its records in challan details or " _
            & vbCrLf & "deductee details, and come back to remove it." _
            , vbInformation, "CANNOT REMOVE")
            chkSectionClick = True
        Else
            chkSectionClick = False
        End If



        'Next i
        'If i = chkSection.Count Then
        '    cmdNext(0).Enabled = False
        'End If
        'Return True
    End Function

    'Private Sub chkSection1_CheckedChanged(sender As Object, e As EventArgs) Handles chkSection1.CheckedChanged
    '    If chkSection1.Checked = False Then
    '        chkSection1.Checked = chkSectionClick(chkSection1.Text)
    '    End If
    'End Sub



    'Private Sub chkSection2_CheckedChanged(sender As Object, e As EventArgs) Handles chkSection2.CheckedChanged
    '    If chkSection2.Checked = False Then
    '        chkSection2.Checked = chkSectionClick(chkSection2.Text)
    '    End If
    'End Sub



    Private Sub txtSurcharge_TextChanged(sender As Object, e As EventArgs) Handles txtSurcharge.TextChanged
        CalcTotalTDS()
    End Sub

    Private Sub txtECess_TextChanged(sender As Object, e As EventArgs) Handles txtECess.TextChanged
        CalcTotalTDS()
    End Sub

    Private Sub txtIntt_TextChanged(sender As Object, e As EventArgs) Handles txtIntt.TextChanged
        CalcTotalTDS()

        If ChkAllocate.Checked = True Then
            txtAIntt.Text = txtIntt.Text
            txtAOthers.Text = txtOthers.Text
        Else
            If txtAIntt.Text <> txtIntt.Text Then
                txtAIntt.ForeColor = Color.Red
            Else
                txtAIntt.ForeColor = Color.Black
            End If
            If txtAOthers.Text <> txtOthers.Text Then
                txtAOthers.ForeColor = Color.Red
            Else
                txtAOthers.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub txtOthers_TextChanged(sender As Object, e As EventArgs) Handles txtOthers.TextChanged
        CalcTotalTDS()

        If ChkAllocate.Checked = True Then
            txtAIntt.Text = txtIntt.Text
            txtAOthers.Text = txtOthers.Text
        Else
            If txtAIntt.Text <> txtIntt.Text Then
                txtAIntt.ForeColor = Color.Red
            Else
                txtAIntt.ForeColor = Color.Black
            End If
            If txtAOthers.Text <> txtOthers.Text Then
                txtAOthers.ForeColor = Color.Red
            Else
                txtAOthers.ForeColor = Color.Black
            End If
        End If

    End Sub

    Private Sub txtFees_TextChanged(sender As Object, e As EventArgs) Handles txtFees.TextChanged
        CalcTotalTDS()
    End Sub

    Private Sub txtTotalTDS_TextChanged(sender As Object, e As EventArgs) Handles txtTotalTDS.TextChanged

    End Sub



    Private Sub txtTotalTDS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalTDS.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtTotalTDS_LostFocus(sender As Object, e As EventArgs) Handles txtTotalTDS.LostFocus
        Call CtrlLostFocus(txtTotalTDS)
    End Sub

    Private Sub txtFees_EnabledChanged(sender As Object, e As EventArgs) Handles txtFees.EnabledChanged

    End Sub

    Private Sub txtOthers_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOthers.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIntt.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtECess_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtECess.KeyPress

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtSurcharge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSurcharge.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtAmtDeducted_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmtDeducted.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub txtChqNo_TextChanged(sender As Object, e As EventArgs) Handles txtChqNo.TextChanged

    End Sub

    Private Sub txtChqNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtChqNo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub


    Private Sub txtChallanNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtChallanNo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        'Dim Itm As New ListViewItemCollection
        Dim setdates As String
        setdates = ""
        oChln = New ClsChallan24QObj
        Dim dt As DateTime
        'dt = DateTime.Parse(dtpChallanDate.Text).ToString("dd/MM/yy")
        'dt = Convert.ToDateTime(dtpChallanDate.Text, "dd/MMM/yyyy")
        'changed on 06/07/2013 to allow blank section as per new FVU 3.8
        If txtTotalTDS.Text = vbNullString Or Not IsDate(dtpChallanDate.Text) Then
            Call MsgBox("Some field Is left blank, please fill up all the fields.", vbExclamation, "INCOMPLETE DATA")
            '    txtTotalTDS.SetFocus
            Exit Sub
        End If

        If cboGovDetIndex > -1 And cboGovDetIndex <= 7 Then
            If chkBookEntry.Checked = True Then
                Select Case Month(dtpChallanDate.Text)
                    Case 1, 3, 5, 7, 8, 10, 12
                        setdates = "31"
                    Case 4, 6, 9, 11
                        setdates = "30"
                    Case 2
                        If (Year(dtpChallanDate.Text) Mod 4) = 0 Then
                            setdates = "29"
                        Else
                            setdates = "28"
                        End If
                End Select
                If MsgBox("Do you want To change the Date To" & setdates & "/" & Month(dtpChallanDate.Text) & "/" & Year(dtpChallanDate.Text) & "?", vbYesNo) = vbYes Then
                    dtpChallanDate.Text = Format(setdates & "/" & Month(dtpChallanDate.Text) & "/" & Year(dtpChallanDate.Text), "dd/mm/yy")
                End If
            End If
        End If



        'Check if nil challan, if no then check details..
        If Val(txtTotalTDS.Text) <> 0# Then
            'Check DatesDim FromLastYear As Date
            Dim FromLastYear As Date
            FromLastYear = CDate(FromDate.Day & "/" & FromDate.Month & "/" & (FromDate.Year - 1))
            If CDate(dtpChallanDate.Text) < FromLastYear Then
                MsgBox("Challan Date cannot be less than " & Format(FromDate, "dd/MM/yyyy"), vbExclamation, "Date Error")
                dtpChallanDate.Focus()
                Exit Sub
            ElseIf CDate(dtpChallanDate.Text) < FromDate Then
                MsgBox("You have entered challan Of last year, please check")

            ElseIf CDate(dtpChallanDate.Text) > Now Then
                MsgBox("Challan Date cannot beyond today's Date, ie " & Format(Now, "dd/MM/yyyy"), vbExclamation, "Date Error")
                dtpChallanDate.Focus()
                Exit Sub
            ElseIf IsDate(dtpChallanDate.Text) = False Then
                MsgBox("Challan Date cannot Blank", vbExclamation, "Date Error")
                dtpChallanDate.Focus()
                Exit Sub
            End If


            If cboGovDetIndex > -1 And cboGovDetIndex <= 7 Then
                If chkBookEntry.Checked = True Then
                    If txtTranVouNo.Text = vbNullString Then
                        Call MsgBox("Pleaes enter Valid Transfer Voucher No.", vbExclamation, "INCOMPLETE DATA")
                        Exit Sub
                    End If
                Else

                    If txtChallanNo.Text = vbNullString Then
                        Call MsgBox("Challan No cannot be left blank.", vbExclamation, "INCOMPLETE DATA")
                        Exit Sub
                    End If
                    If cboBankBrCode.Text = vbNullString Then
                        Call MsgBox("BankBrCode cannot be left blank.", vbExclamation, "INCOMPLETE DATA")
                        Exit Sub
                    End If
                End If
            Else
                ' cheque no remove from compulsory w.e.f. 06/07/2013
                If cboBankBrCode.Text = vbNullString Or txtChallanNo.Text = vbNullString Then
                    Call MsgBox("Some field is left blank, please fill up all the fields.", vbExclamation, "INCOMPLETE DATA")
                    Exit Sub
                End If
            End If
            'To check challanNo & BankVouch No
            If Len(Trim(txtChallanNo.Text)) > 0 Then
                If Val(txtChallanNo.Text) = 0 Then
                    MsgBox("Challan No. should not be zero", vbCritical, Me.Text)
                    txtChallanNo.Focus()
                    Exit Sub
                End If
            ElseIf Len(Trim(txtTranVouNo.Text)) > 0 Then
                If Val(txtTranVouNo.Text) = 0 Then
                    MsgBox("Transfer voucher No. should not be zero", vbCritical, Me.Text)
                    txtTranVouNo.Focus()
                    Exit Sub
                End If
            End If
        ElseIf MsgBox("Are you creating a NIL Challan", vbYesNo, "SURE ON NIL CHALLAN") = vbYes Then      'blank challan..
            txtChallanNo.Text = vbNullString
            txtChqNo.Text = vbNullString
        Else    'user has pressed 'No';.
            Exit Sub
        End If
        If cmdAdd.Text = "Add" Then
            'Add item..
            If oChln.Insert(oChln) = False Then
                MsgBox("Unable to Insert Challan Detail in Database" & vbCrLf & "Call JAK Infosolutions", vbCritical, "CANNOT ADD NOW")
            Else
                Dim newitem As New ListViewItem()
                newitem.Text = cboChallanSection.Text 'first column
                newitem.SubItems.Add(txtAmtDeducted.Text) 'second column
                newitem.SubItems.Add(txtSurcharge.Text)
                newitem.SubItems.Add(txtECess.Text)
                newitem.SubItems.Add(txtIntt.Text)
                newitem.SubItems.Add(txtOthers.Text)
                newitem.SubItems.Add(txtFees.Text)
                newitem.SubItems.Add(txtTotalTDS.Text)
                newitem.SubItems.Add(txtChqNo.Text)
                newitem.SubItems.Add(chkBookEntry.Checked)
                newitem.SubItems.Add(txtChallanNo.Text)
                newitem.SubItems.Add(txtTranVouNo.Text)
                newitem.SubItems.Add(cboBankBrCode.Text)

                dt = dtpChallanDate.Text

                newitem.SubItems.Add(dt.ToString("dd/MMM/yyyy"))
                newitem.SubItems.Add(txtChallanRemark.Text)
                newitem.SubItems.Add(IIf(chkMinorHead.Checked = True, "400", "200"))
                newitem.SubItems.Add(oChln.ChallanID)
                newitem.SubItems.Add(txtAIntt.Text)
                newitem.SubItems.Add(txtAOthers.Text)
                'newitem.SubItems.Add(txtTotalTDS.Text)

                lvwChallan.Items.Add(newitem)
                Call NormalMode()
            End If
        Else
            'Edit Item..
            If oChln.Update(oChln) = False Then
                MsgBox("Unable to update challan details in database" & vbCrLf & "Call JAK Infosolutions", vbCritical, "CANNOT UPDATE NOW")
            Else
                lvwChallan.SelectedItems(0).SubItems(0).Text = cboChallanSection.Text
                lvwChallan.SelectedItems(0).SubItems(1).Text = txtAmtDeducted.Text
                lvwChallan.SelectedItems(0).SubItems(2).Text = txtSurcharge.Text
                lvwChallan.SelectedItems(0).SubItems(3).Text = txtECess.Text
                lvwChallan.SelectedItems(0).SubItems(4).Text = txtIntt.Text 'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(4)
                lvwChallan.SelectedItems(0).SubItems(5).Text = txtOthers.Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(5)
                lvwChallan.SelectedItems(0).SubItems(6).Text = txtFees.Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(6)
                lvwChallan.SelectedItems(0).SubItems(7).Text = txtTotalTDS.Text
                lvwChallan.SelectedItems(0).SubItems(8).Text = txtChqNo.Text  'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(8)
                lvwChallan.SelectedItems(0).SubItems(9).Text = chkBookEntry.Checked  ' IIf(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(9) = True, vbChecked, vbUnchecked)
                lvwChallan.SelectedItems(0).SubItems(10).Text = txtChallanNo.Text 'Format(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(10), "00000")
                lvwChallan.SelectedItems(0).SubItems(11).Text = txtTranVouNo.Text  ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(11)
                lvwChallan.SelectedItems(0).SubItems(12).Text = cboBankBrCode.Text  ' Format(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(12), "0000000")

                dt = dtpChallanDate.Text

                lvwChallan.SelectedItems(0).SubItems(13).Text = Format(dt, "dd/MMM/yyyy") ' Format(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(13), "dd/mm/yy")
                lvwChallan.SelectedItems(0).SubItems(14).Text = txtChallanRemark.Text 'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(14)
                lvwChallan.SelectedItems(0).SubItems(15).Text = IIf(chkMinorHead.Checked = True, "400", "200")
                lvwChallan.SelectedItems(0).SubItems(16).Text = cboChallanSection.Tag  ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(16)
                lvwChallan.SelectedItems(0).SubItems(17).Text = txtAIntt.Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(17)
                lvwChallan.SelectedItems(0).SubItems(18).Text = txtAOthers.Text 'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(18)
                'lvwChallan.SelectedItems(0).SubItems(19).Text = txtTotalTDS.Text
                Call NormalMode()
            End If
        End If
        Call RangDeChallans()
        Call ClearChallanCtrls(True)
        Call fillcboDedChallan()
        cboChallanSection.SelectedIndex = -1
        cboChallanSection.Focus()
    End Sub
    Private Sub fillcboDedChallan()
        Dim nds As New DataSet, i As Integer
        'Filling Challan in Deductee Detail

        nds = FetchDataSet("select distinct challanId,BankChallanNo,DtOfChallan from Challan24Q WHERE RetnID=" & Me.Tag & " order by ChallanID")
        ' cboChallanNo.Items.Clear()
        Dim newDataset As New DataSet
        Dim dt As DataTable
        Dim dr As DataRow
        Dim icol As DataColumn
        Dim namecol As DataColumn
        dt = New DataTable
        newDataset = New DataSet
        icol = New DataColumn("ChallanID", Type.GetType("System.Int32"))
        namecol = New DataColumn("ChallanNo", Type.GetType("System.String"))
        dt.Columns.Add(icol)
        dt.Columns.Add(namecol)
        For i = 0 To nds.Tables(0).Rows.Count - 1
            dr = dt.NewRow()
            dr("ChallanID") = nds.Tables(0).Rows(i)(0)
            dr("ChallanNo") = SetFormat("00000", nds.Tables(0).Rows(i)("BankChallanNo").ToString()) & " - " & Format(nds.Tables(0).Rows(i)("DtOfChallan"), "dd/MM/yy")
            dt.Rows.Add(dr)


        Next
        newDataset.Tables.Add(dt)
        nds.Dispose()
        cboChallanNo.DataSource = Nothing
        cboChallanNo.Items.Clear()
        cboChallanNo.DataSource = newDataset.Tables(0)
        cboChallanNo.DisplayMember = "ChallanNo"
        cboChallanNo.ValueMember = "ChallanID"

        newDataset.Dispose()
        cboChallanNo.SelectedIndex = -1
        'dt.Dispose()

        'For i = 0 To nds.Tables(0).Rows.Count - 1
        '    cboChallanNo.Items.Add(Format(nds.Tables(0).Rows(i)("BankChallanNo"), "00000") & " - " & Format(nds.Tables(0).Rows(i)("DtOfChallan"), "dd/MM/yy"))
        '    cboChallanNo.SelectedValue = nds.Tables(0).Rows(i)("ChallanID")

        'Next
        'nds.Dispose()
    End Sub
    Public Sub lvwchallanHead()
        With lvwChallan
            .Columns.Clear()
            .Columns.Add("Section", 50, HorizontalAlignment.Left)
            .Columns.Add("Amount Deducted", 50, HorizontalAlignment.Right)
            .Columns.Add("Surcharge", 50, HorizontalAlignment.Right)
            .Columns.Add("Edu. Cess", 50, HorizontalAlignment.Right)
            .Columns.Add("Interest", 50, HorizontalAlignment.Right)
            .Columns.Add("Others", 50, HorizontalAlignment.Right)
            .Columns.Add("Fees", 50, HorizontalAlignment.Right)
            .Columns.Add("Total Tax", 50, HorizontalAlignment.Right)
            .Columns.Add("Chq./DD No.", 50, HorizontalAlignment.Left)
            .Columns.Add("By book entry", 50, HorizontalAlignment.Left)
            .Columns.Add("Challan No.", 50, HorizontalAlignment.Left)
            .Columns.Add("Tran. Vou. No. ", 50, HorizontalAlignment.Left)
            .Columns.Add("BSR code", 50, HorizontalAlignment.Left)
            .Columns.Add("Dt. of Challan", 50, HorizontalAlignment.Left)
            .Columns.Add("Remark", 50, HorizontalAlignment.Left)
            .Columns.Add("MinorHead", 50, HorizontalAlignment.Left)
            .Columns.Add("ChallanID", 50, HorizontalAlignment.Left)
            .Columns.Add("Allocated Interest", 50, HorizontalAlignment.Right)
            .Columns.Add("Allocated Other Amt", 50, HorizontalAlignment.Right)
            'Display listview in details view
            .View = View.Details
            'display grid lines
            .GridLines = True
            'allow full row selection
            .FullRowSelect = True
        End With
    End Sub
    Public Sub lvwDeducteeHead()
        With lvwDeductee

            .Columns.Clear()

            .Columns.Add("Section", 50, HorizontalAlignment.Left)
            .Columns.Add("Name of Deductee", 100, HorizontalAlignment.Left)
            .Columns.Add("PAN of Deductee", 60, HorizontalAlignment.Left)
            .Columns.Add("Amt of Payment/Credit", 60, HorizontalAlignment.Right)
            .Columns.Add("Dt of Payment/Credit", 60, HorizontalAlignment.Left)
            .Columns.Add("", 0, HorizontalAlignment.Left)
            .Columns.Add("", 0, HorizontalAlignment.Right)
            .Columns.Add("TDS", 60, HorizontalAlignment.Right)
            .Columns.Add("Surchage", 60, HorizontalAlignment.Right)
            .Columns.Add("Edu. Cess", 60, HorizontalAlignment.Right)
            .Columns.Add("Total Tax Deducted", 60, HorizontalAlignment.Right)
            .Columns.Add("Date of Deduction", 60, HorizontalAlignment.Left)
            .Columns.Add("Total Tax Deposited", 60, HorizontalAlignment.Right)
            .Columns.Add("Challan Id", 0, HorizontalAlignment.Left)
            .Columns.Add("Challan No.", 60, HorizontalAlignment.Right)
            .Columns.Add("Remark", 60, HorizontalAlignment.Left)
            .Columns.Add("Certificate No", 60, HorizontalAlignment.Left)
            .Columns.Add("Id24Q", 0, HorizontalAlignment.Left)

            'Display listview in details view
            .View = View.Details
            'display grid lines
            .GridLines = True
            'allow full row selection
            .FullRowSelect = True
        End With
    End Sub
    Public Sub lvwSDHead()
        With lvwSD
            .Columns.Clear()
            .Columns.Add("Name of Deductee", 100, HorizontalAlignment.Left)
            .Columns.Add("PAN of Deductee", 60, HorizontalAlignment.Left)
            .Columns.Add("Category", 60, HorizontalAlignment.Right)
            .Columns.Add("From Dt.", 60, HorizontalAlignment.Left)
            .Columns.Add("To Dt.", 60, HorizontalAlignment.Right)
            .Columns.Add("Total Salary", 60, HorizontalAlignment.Right)
            .Columns.Add("Ent. Allowances", 60, HorizontalAlignment.Right)
            .Columns.Add("Tax On Emp.", 60, HorizontalAlignment.Right)
            .Columns.Add("", 0, HorizontalAlignment.Left)
            .Columns.Add("Taxable Salary", 60, HorizontalAlignment.Left)
            .Columns.Add("Other Income", 60, HorizontalAlignment.Right)
            .Columns.Add("Gross Total Income", 60, HorizontalAlignment.Right)
            .Columns.Add("80CCE Amt.", 60, HorizontalAlignment.Left)
            .Columns.Add("80CCF Amt.", 60, HorizontalAlignment.Left)
            .Columns.Add("80CCG Amt.", 60, HorizontalAlignment.Left)
            .Columns.Add("Chp. VI-A Amt.", 60, HorizontalAlignment.Left)
            .Columns.Add("Taxable Income", 60, HorizontalAlignment.Left)
            .Columns.Add("Income Tax", 60, HorizontalAlignment.Left)
            .Columns.Add("Surcharge", 60, HorizontalAlignment.Left)
            .Columns.Add("Edu. Cess", 60, HorizontalAlignment.Left)
            .Columns.Add("Total Tax", 60, HorizontalAlignment.Left)
            .Columns.Add("89 Relief", 60, HorizontalAlignment.Left)
            .Columns.Add("Net Tax", 60, HorizontalAlignment.Left)
            .Columns.Add("TDS Amt. Balance", 60, HorizontalAlignment.Left)
            .Columns.Add("Excess/Short", 60, HorizontalAlignment.Left)
            .Columns.Add("Total Salary By Previous Employer", 60, HorizontalAlignment.Left)
            .Columns.Add("TDS Amt. By Previous Employer", 60, HorizontalAlignment.Left)
            .Columns.Add("High Rate PAN", 60, HorizontalAlignment.Left)
            .Columns.Add("TDS Yearly", 60, HorizontalAlignment.Left)

            'Display listview in details view
            .View = View.Details
            'display grid lines
            .GridLines = True
            'allow full row selection
            .FullRowSelect = True
        End With
    End Sub

    Public Sub lvwForm16Head()
        With lvwForm16
            .Columns.Clear()
            .Columns.Add("Name of Deductee", 100, HorizontalAlignment.Left)
            .Columns.Add("PAN of Deductee", 60, HorizontalAlignment.Left)
            .Columns.Add("Category", 60, HorizontalAlignment.Right)
            .Columns.Add("From Dt.", 60, HorizontalAlignment.Left)
            .Columns.Add("To Dt.", 60, HorizontalAlignment.Right)
            .Columns.Add("Total Salary", 60, HorizontalAlignment.Right)
            .Columns.Add("Allowances", 60, HorizontalAlignment.Right)
            .Columns.Add("Ent. Allowances", 60, HorizontalAlignment.Right)
            .Columns.Add("Tax On Emp.", 60, HorizontalAlignment.Right)
            .Columns.Add("Taxable Salary", 60, HorizontalAlignment.Left)
            .Columns.Add("Other Income", 60, HorizontalAlignment.Right)
            .Columns.Add("Gross Total Income", 60, HorizontalAlignment.Right)
            .Columns.Add("80CCE Amt.", 60, HorizontalAlignment.Left)
            .Columns.Add("80CCF Amt.", 60, HorizontalAlignment.Left)
            '.Columns.Add("80CCG Amt.", 60, HorizontalAlignment.Left)
            .Columns.Add("Chp. VI-A Amt.", 60, HorizontalAlignment.Left)
            .Columns.Add("Taxable Income", 60, HorizontalAlignment.Left)
            .Columns.Add("Income Tax", 60, HorizontalAlignment.Left)
            .Columns.Add("Surcharge", 60, HorizontalAlignment.Left)
            .Columns.Add("Edu. Cess", 60, HorizontalAlignment.Left)
            .Columns.Add("Total Tax", 60, HorizontalAlignment.Left)
            .Columns.Add("89 Relief", 60, HorizontalAlignment.Left)
            .Columns.Add("Net Tax", 60, HorizontalAlignment.Left)
            .Columns.Add("TDS Yearly", 60, HorizontalAlignment.Left)
            .Columns.Add("Excess/Short", 60, HorizontalAlignment.Left)
            .Columns.Add("", 0, HorizontalAlignment.Left)
            .Columns.Add("Total Salary By Previous Employer", 60, HorizontalAlignment.Left)
            .Columns.Add("TDS Amt. By Previous Employer", 60, HorizontalAlignment.Left)
            '.Columns.Add("TDS Amt. Balance", 60, HorizontalAlignment.Left)
            .Columns.Add("High Rate PAN", 60, HorizontalAlignment.Left)


            'Display listview in details view
            .View = View.Details
            'display grid lines
            .GridLines = True
            'allow full row selection
            .FullRowSelect = True
        End With
    End Sub

    Private Sub lvwChallan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwChallan.SelectedIndexChanged

    End Sub

    Private Sub lvwChallan_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvwChallan.ColumnClick
        lvwChallan.Sort()
        lvwChallan.Sorting = SortOrder.Ascending 'ColumnHeader.Index - 1
    End Sub


    Private Sub oChln_PrepareDataForSave(Cancel As Boolean) Handles oChln.PrepareDataForSave

        With oChln
            .ChallanID = IIf(Val(cboChallanSection.Tag) = 0, 0, Val(cboChallanSection.Tag))
            .RetnID = Me.Tag
            .Sec = cboChallanSection.Text
            .TaxAmt = IIf(Len(Trim(txtAmtDeducted.Text)) = 0, 0, txtAmtDeducted.Text)
            .Surcharge = IIf(Len(Trim(txtSurcharge.Text)) = 0, 0, txtSurcharge.Text)
            .ECess = IIf(Len(Trim(txtECess.Text)) = 0, 0, txtECess.Text)
            .Interest = IIf(Len(Trim(txtIntt.Text)) = 0, 0, txtIntt.Text)
            .Others = IIf(Len(Trim(txtOthers.Text)) = 0, 0, txtOthers.Text)
            .AInterest = IIf(Len(Trim(txtAIntt.Text)) = 0, 0, txtAIntt.Text)
            .AOthers = IIf(Len(Trim(txtAOthers.Text)) = 0, 0, txtAOthers.Text)
            .TotalTax = IIf(Len(Trim(txtTotalTDS.Text)) = 0, 0, txtTotalTDS.Text)
            .BankChallanNo = IIf(Val(txtChallanNo.Text) = 0, -1, txtChallanNo.Text)
            '      .BankChallanNo = IIf(Len(Trim(txtChallanNo)) = 0, "Null", txtChallanNo.Text)
            Dim dt As Date
            dt = dtpChallanDate.Text

            .DtOfChallan = dt.ToString("dd/MMM/yyyy")
            .BankBrCode = IIf(Len(Trim(cboBankBrCode.Text)) = 0, -1, cboBankBrCode.Text)
            .TranVouNo = IIf(Len(Trim(txtTranVouNo.Text)) = 0, -1, txtTranVouNo.Text)
            .IsBookEntry = chkBookEntry.Checked
            .ChqDDNo = IIf(Len(Trim(txtChqNo.Text)) = 0, -1, txtChqNo.Text)
            .Remark = txtChallanRemark.Text
            .AFees = IIf(Len(Trim(txtFees.Text)) = 0, 0, txtFees.Text)
            .MinorHead = IIf(chkMinorHead.Checked = True, "400", "200")
        End With
    End Sub


    Private Sub oDed_BeforeSave(Cancel As Boolean) Handles oDed.BeforeSave

        If cboDedName.SelectedIndex < 0 Then
            MsgBox("Please select the deductee before save", vbInformation, "No Deductee Selected")
            Cancel = True
        End If

    End Sub

    Private Sub oDed_PrepareDataForSave(Cancel As Boolean) Handles oDed.PrepareDataForSave
        With oDed
            If lvwDeductee.Items.Count > 0 Then
                If lvwDeductee.SelectedIndices.Count > 0 Then
                    'SelectedId = lvwCo.SelectedItems(0).SubItems(1).Text
                    'selectedcoid = lvwCo.SelectedItems(0).SubItems(17).Text
                    .ID24Q = lvwDeductee.SelectedItems(0).SubItems(17).Text
                End If
            Else
                .ID24Q = 0
            End If
            Dim dt As Date
            .RetnID = Me.Tag
            .did = cboDedName.SelectedValue
            .DCode = GetDCode(.did)
            .Sec = cboDedSection.Text
            .AmtOfPayment = Val(txtAmtPay.Text)
            dt = dtpAmtPayDt.Text
            .DtOfPayment = dt.ToString("dd/MMM/yyyy")
            '.RateOfTDS = Val(txtTDSRate.Text)
            .TaxAmt = Val(txtTDSAmt.Text)
            .Surcharge = Val(txtDSurchrge.Text)
            .ECess = Val(txtDECess.Text)
            .TotalTaxDeposited = Val(txtTotalTaxDeposited.Text)
            .TotalTaxDeducted = Val(txtTotalTaxDeducted.Text)

            If dtpTDSDedDt.Text = "  /  /" Then

            Else

                dt = dtpTDSDedDt.Text
                .DtOfDeduction = dt.ToString("dd/MMM/yyyy")
            End If
            ' .DtOfDeduction = IIf(dtpTDSDedDt.Text = "  /  /", vbEmpty, dtpTDSDedDt.Text)
            '.IsBookEntry = chkDedBookEntry.Checked
            .Remark = IIf(Strings.Left(cboRemark.Text, 1) = "N", " ", Strings.Left(cboRemark.Text, 1))
            .CertNo = txtCertNo.Text & ""
            '.ChallanID = IIf(cboChallanNo.ListIndex = -1, 0, cboChallanNo.ItemData(cboChallanNo.ListIndex))
            Dim item14 As String = Strings.Left(cboChallanNo.Text, InStr(1, cboChallanNo.Text, "- Rs.", vbTextCompare) - 1)
            Dim cno As String = Strings.Left(item14, InStr(1, cboChallanNo.Text, " - ", vbTextCompare) - 1)
            Dim cdt As Date = Strings.Mid(item14, Len(cno) + 4)
            If cboChallanNo.SelectedIndex < 0 Then
                .ChallanID = 0

            Else
                .ChallanID = cboChallanNo.SelectedValue ' oDed.GetChallanID(cno, cdt.ToString("dd/MM/yyyy"))
            End If
        End With
    End Sub
    Private Function GetDCode(ID As Long) As String
        Dim nds As New DataSet
        Dim sql As String
        sql = "Select DType From DeductMst Where  Did = " & ID
        nds = FetchDataSet(sql)
        If nds.Tables(0).Rows.Count > 0 Then
            GetDCode = nds.Tables(0).Rows(0)(0).ToString()
        Else
            GetDCode = ""
        End If
        nds.Dispose()

    End Function

    Private Sub lvwChallan_DoubleClick(sender As Object, e As EventArgs) Handles lvwChallan.DoubleClick
        Call EditRow("C")
        Call EditMode()
        'End If
    End Sub

    Private Sub lvwChallan_KeyUp(sender As Object, e As KeyEventArgs) Handles lvwChallan.KeyUp
        If lvwChallan.SelectedIndices Is Nothing Then
            Call MsgBox("Please select the row first.  " _
                    & vbCrLf & "Without row selection this action cannot be performed." _
                    , vbExclamation + vbDefaultButton2, "NOTHING SELECTED")
            Exit Sub
        End If
        Select Case e.KeyCode
            Case Keys.D
                If Control.ModifierKeys = Keys.Shift Then 'Keys.ShiftKey = vbCtrlMask Then
                    oChln = New ClsChallan24QObj
                    'lvwChallan.Items(j).SubItems(1).Text

                    If oChln.LinkDed24Q(lvwChallan.SelectedItems.Item(16).Text) = True Then
                        MsgBox("This Record is further used, Cannot Delete", vbInformation, "Caution")
                        Exit Sub
                    End If
                    If MsgBox("Do you want to delete this row?", vbYesNo + vbQuestion + vbDefaultButton2, "DELETE DATA") = vbYes Then
                        'delete the row..

                        If oChln.Delete(lvwChallan.SelectedItems.Item(16).Text) = True Then
                            lvwChallan.Items.Remove(lvwChallan.SelectedItems.Item(16)) '(lvwChallan.SelectedItem.Index)
                        End If
                    End If
                End If
            Case Keys.E
                If Control.ModifierKeys = Keys.Shift Then ' Shift = vbCtrlMask Then
                    'Shift Data for edit
                    Call EditRow("C")
                    Call EditMode()
                End If
        End Select
    End Sub
    Private Sub EditRow(typ As String)
        Dim i As Integer
        Dim dt As DateTime
        Dim sec As Integer
        If typ = "C" Then
            If lvwChallan.Items.Count = 0 Then Exit Sub
            cboChallanSection.SelectedIndex = -1
            If lvwChallan.SelectedItems(0).SubItems(0).Text = "" Then
            Else
                cboChallanSection.SelectedIndex = cboChallanSection.FindString(lvwChallan.SelectedItems(0).SubItems(0).Text)
            End If
            txtAmtDeducted.Text = lvwChallan.SelectedItems(0).SubItems(1).Text
            txtSurcharge.Text = lvwChallan.SelectedItems(0).SubItems(2).Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(2)
            txtECess.Text = lvwChallan.SelectedItems(0).SubItems(3).Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(3)
            txtIntt.Text = lvwChallan.SelectedItems(0).SubItems(4).Text 'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(4)
            txtOthers.Text = lvwChallan.SelectedItems(0).SubItems(5).Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(5)
            txtFees.Text = lvwChallan.SelectedItems(0).SubItems(6).Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(6)
            txtChqNo.Text = lvwChallan.SelectedItems(0).SubItems(8).Text 'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(8)
            chkBookEntry.Checked = lvwChallan.SelectedItems(0).SubItems(9).Text ' IIf(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(9) = True, vbChecked, vbUnchecked)
            txtChallanNo.Text = lvwChallan.SelectedItems(0).SubItems(10).Text 'Format(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(10), "00000")
            txtTranVouNo.Text = lvwChallan.SelectedItems(0).SubItems(11).Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(11)
            cboBankBrCode.Text = lvwChallan.SelectedItems(0).SubItems(12).Text ' Format(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(12), "0000000")

            dt = lvwChallan.SelectedItems(0).SubItems(13).Text

            dtpChallanDate.Text = Format(dt, "dd/MM/yy") ' Format(lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(13), "dd/mm/yy")
            txtChallanRemark.Text = lvwChallan.SelectedItems(0).SubItems(14).Text 'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(14)
            chkMinorHead.Checked = IIf(lvwChallan.SelectedItems(0).SubItems(15).Text = "400", True, False)
            cboChallanSection.Tag = lvwChallan.SelectedItems(0).SubItems(16).Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(16)
            txtAIntt.Text = lvwChallan.SelectedItems(0).SubItems(17).Text ' lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(17)
            txtAOthers.Text = lvwChallan.SelectedItems(0).SubItems(18).Text 'lvwChallan.ListItems(lvwChallan.SelectedItem.Index).SubItems(18)
            'ChkAllocate.Checked = False
            cmdAdd.Text = "&Save"
            cmdCnlEdit.Enabled = True
        ElseIf typ = "D" Then
            If lvwDeductee.Items.Count = 0 Then Exit Sub
            cboDedSection.SelectedIndex = -1
            cboDedSection.SelectedIndex = IIf(lvwDeductee.SelectedItems(0).SubItems(0).Text = "", -1, cboDedSection.FindString(lvwDeductee.SelectedItems(0).SubItems(0).Text))

            'check if section saved is not in list (possible only when data saved under old sections and software accepting only new sections
            'eg. when changed 194I to 194IA and 914IB and removed 194I....
            If cboDedSection.SelectedIndex = -1 Then
                MsgBox("Entry saved with old section." & vbCrLf & "Please select the new section and then save" & vbCrLf &
            "System will automatically select the 1st Section available in the list", vbCritical, "SELECT CORRECT SECTION")
                If cboDedSection.Items.Count > 0 Then
                    cboDedSection.SelectedIndex = 0
                End If
            End If
            ' For i = 0 To cboDedName.Items.Count - 1
            'cboDedName.Items(0) = lvwDeductee.SelectedItems(0).SubItems(1).Text
            Dim DIdNo As Integer

            DIdNo = cboDedName.FindString(lvwDeductee.SelectedItems(0).SubItems(1).Text)
            If DIdNo > -1 Then
                cboDedName.SelectedIndex = -1
                cboDedName.SelectedIndex = DIdNo
                cboDedName.Tag = cboDedName.SelectedValue

                'Exit For
            End If
            'cboDedName.SelectedIndex = i
            'Next i
            On Error Resume Next

            txtDedPAN.Text = lvwDeductee.SelectedItems(0).SubItems(2).Text
            txtAmtPay.Text = lvwDeductee.SelectedItems(0).SubItems(3).Text
            dt = lvwDeductee.SelectedItems(0).SubItems(4).Text
            dtpAmtPayDt.Text = Format(dt, "dd/MM/yy")
            'txtTDSRate.Text = lvwDeductee.SelectedItems(0).SubItems(6).Text
            txtTDSAmt.Text = lvwDeductee.SelectedItems(0).SubItems(7).Text
            txtDSurchrge.Text = lvwDeductee.SelectedItems(0).SubItems(8).Text
            txtDECess.Text = lvwDeductee.SelectedItems(0).SubItems(9).Text
            txtTotalTaxDeducted.Text = lvwDeductee.SelectedItems(0).SubItems(10).Text
            dt = lvwDeductee.SelectedItems(0).SubItems(11).Text
            dtpTDSDedDt.Text = Format(dt, "dd/MM/yy") 'IIf(lvwDeductee.ListItems(lvwDeductee.SelectedItem.Index).SubItems(11) = "", "__/__/__", Format(lvwDeductee.ListItems(lvwDeductee.SelectedItem.Index).SubItems(11), "dd/MM/yy"))
            txtTotalTaxDeposited.Text = lvwDeductee.SelectedItems(0).SubItems(12).Text
            'chkDedBookEntry.Checked = lvwDeductee.SelectedItems(0).SubItems(5).Text
            'cboRemark.ListIndex = IIf(Trim(lvwDeductee.ListItems(lvwDeductee.SelectedItem.Index).SubItems(15)) = vbNullString, 0, IIf(lvwDeductee.ListItems(lvwDeductee.SelectedItem.Index).SubItems(15) = "A", 1, 2))
            '    Dim i As Integer
            If lvwDeductee.SelectedItems(0).SubItems(15).Text = vbNullString Then
                cboRemark.SelectedIndex = 0
            Else
                'For i = 0 To cboRemark.Items.Count - 1
                DIdNo = cboRemark.FindString(lvwDeductee.SelectedItems(0).SubItems(15).Text)
                If DIdNo > -1 Then

                    cboRemark.SelectedIndex = DIdNo
                    'Exit For
                End If
                'Next i
            End If
            txtCertNo.Text = lvwDeductee.SelectedItems(0).SubItems(16).Text
            If lvwDeductee.SelectedItems(0).SubItems(13).Text = "0" Then
                cboChallanNo.Text = vbNullString
            Else

                'For i = 0 To cboChallanNo.Items.Count - 1
                '    'If cboChallanNo.SelectedValue = lvwDeductee.SelectedItems(0).SubItems(13).Text Then
                '    Dim chlno As String, oc As Integer
                '    oc = InStr(cboChallanNo.Items(i).ToString(), "- Rs.")
                '    chlno = Strings.Left(cboChallanNo.Items(i).ToString(), oc - 1)
                '    If chlno = lvwDeductee.SelectedItems(0).SubItems(14).Text Then
                '        cboChallanNo.SelectedIndex = i
                '        Exit For
                '    End If
                '    cboChallanNo.SelectedIndex = i
                'Next
                'cboChallanNo.SelectedIndex = i
                cboChallanNo.SelectedValue = lvwDeductee.SelectedItems(0).SubItems(13).Text
                cboChallanNo.Tag = lvwDeductee.SelectedItems(0).SubItems(13).Text
            End If
        ElseIf typ = "SD" Then
            If lvwSD.SelectedItems(0).SubItems(0).Text = "" Then
                cboSDDedName.SelectedIndex = -1
            Else
                cboSDDedName.SelectedIndex = cboSDDedName.FindString(lvwSD.SelectedItems(0).SubItems(0).Text)

            End If
            'For i = 0 To cboSDDedName.SelectedIndex - 1
            '    If cboSDDedName.SelectedIndex = lvwSD.SelectedItems(0).SubItems(1).Text Then
            '        cboSDDedName.SelectedIndex = i
            '        cboSDDedName.Tag = cboSDDedName.Items.Add(i)
            '        Exit For
            '    End If
            'Next i
            On Error Resume Next
            txtSDDedPAN.Text = lvwSD.SelectedItems(0).SubItems(1).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(1)
            txtSDDedCateg.Text = lvwSD.SelectedItems(0).SubItems(2).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(2)
            dt = lvwSD.SelectedItems(0).SubItems(3).Text
            txtSDEmpFrm.Text = Format(dt, "dd/MM/yy")
            dt = lvwSD.SelectedItems(0).SubItems(4).Text
            txtSDEmpTo.Text = Format(dt, "dd/MM/yy")
            'txtSDEmpFrm.Text = lvwSD.SelectedItems(0).SubItems(3).Text 'Format(lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(3), "dd/MM/yy")
            'txtSDEmpTo.Text = lvwSD.SelectedItems(0).SubItems(4).Text 'Format(lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(4), "dd/MM/yy")
            txtSDTotalSal.Text = lvwSD.SelectedItems(0).SubItems(5).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(5)
            txtSDEntAllow.Text = lvwSD.SelectedItems(0).SubItems(6).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(6)
            txtSDProfTax.Text = lvwSD.SelectedItems(0).SubItems(7).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(7)
            txtSDOtherIncome.Text = lvwSD.SelectedItems(0).SubItems(10).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(9)
            txtSD80CCE.Text = lvwSD.SelectedItems(0).SubItems(12).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(11)
            txtSD80CCF.Text = lvwSD.SelectedItems(0).SubItems(13).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(12)
            txtSD80CCG.Text = lvwSD.SelectedItems(0).SubItems(14).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(13)
            txtSDOtherIVA.Text = lvwSD.SelectedItems(0).SubItems(15).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(14)
            txtSDTax.Text = lvwSD.SelectedItems(0).SubItems(17).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(16)
            txtSDSurcharge.Text = lvwSD.SelectedItems(0).SubItems(18).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(17)
            txtSDEduCess.Text = lvwSD.SelectedItems(0).SubItems(19).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(18)
            txtSDRelief.Text = lvwSD.SelectedItems(0).SubItems(21).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(20)
            txtSDTDSCurEmp.Text = lvwSD.SelectedItems(0).SubItems(23).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(22)
            'txtSDID.Text = lvwSD.SelectedItems(0).SubItems(24).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(24)
            txtSDTotalSalPreEmp.Text = lvwSD.SelectedItems(0).SubItems(25).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(25)
            txtSDTDSPreEmp.Text = lvwSD.SelectedItems(0).SubItems(26).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(26)
            ChkHigRate.Checked = lvwSD.SelectedItems(0).SubItems(27).Text 'IIf(lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(27) = True, vbChecked, vbUnchecked)
            txtSDTDS.Text = lvwSD.SelectedItems(0).SubItems(28).Text 'lvwSD.ListItems(lvwSD.SelectedItem.Index).SubItems(28)
        End If
        AutoCalcReqd = False
    End Sub

    Private Sub CalcTotalDeducteeTDS()
        txtTotalTaxDeducted.Text = Val(txtTDSAmt.Text) + Val(txtDSurchrge.Text) + Val(txtDECess.Text)
        '   If cmdDedAdd.Caption = "Add" Then
        txtTotalTaxDeposited.Text = Val(txtTotalTaxDeducted.Text)
        '   End If
        If Val(txtTotalTaxDeducted.Text) <> Val(txtTotalTaxDeposited.Text) Then
            txtTotalTaxDeposited.BackColor = Color.Red
        Else
            txtTotalTaxDeposited.BackColor = Color.White
        End If

    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Dim sql As String
        Dim nds As New DataSet
        Dim sqlretnID As String
        Dim rsretnId As New DataSet
        Dim ret As String
        'gridhead()
        ret = IIf(OptQ1.Checked = True, "24Q1", IIf(OptQ2.Checked = True, "24Q2", IIf(OptQ3.Checked = True, "24Q3", IIf(OptQ4.Checked = True, "24Q4", "All"))))
        sqlretnID = "SELECT r.RetnID FROM comst AS c INNER JOIN retnmst AS r ON c.CoID = r.CoID WHERE (((r.FrmType)=" & "'" & ret & "'" & ") AND ((c.CoID)=" & selectedcoid & "))"
        rsretnId = FetchDataSet(sqlretnID)

        If ret <> "All" Then
            ' If cboDedName.ListIndex = -1 Then
            If rsretnId.Tables(0).Rows.Count = 0 Then
                MsgBox("There is No Record..!!")
                Exit Sub
            Else
                sql = "SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.AmtOfPayment) AS SumOfAmtOfPayment, Sum(Deductee24Q.TaxAmt) AS SumOfTaxAmt, Sum(Deductee24Q.Surcharge) AS SumOfSurcharge, " _
              & " Sum(Deductee24Q.ECess) AS SumOfECess, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, " _
              & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted" _
              & " FROM CoMst INNER JOIN (RetnMst INNER JOIN (DeductMst INNER JOIN Deductee24Q ON DeductMst.DId = Deductee24Q.DId) ON RetnMst.RetnID = Deductee24Q.RetnID) ON (CoMst.CoID = RetnMst.CoID) AND (CoMst.CoID = DeductMst.CoID) " _
             & " Where Comst.coid = " & selectedcoid & " " _
             & " and RetnMst.RetnID=" & rsretnId.Tables(0).Rows(0)(0).ToString() _
             & " GROUP BY DeductMst.DName, DeductMst.DPan"
            End If
        Else
            sql = "SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.AmtOfPayment) AS SumOfAmtOfPayment, Sum(Deductee24Q.TaxAmt) AS SumOfTaxAmt, Sum(Deductee24Q.Surcharge) AS SumOfSurcharge, " _
           & " Sum(Deductee24Q.ECess) AS SumOfECess, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, " _
           & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted" _
           & " FROM CoMst INNER JOIN (RetnMst INNER JOIN (DeductMst INNER JOIN Deductee24Q ON DeductMst.DId = Deductee24Q.DId) ON RetnMst.RetnID = Deductee24Q.RetnID) ON (CoMst.CoID = RetnMst.CoID) AND (CoMst.CoID = DeductMst.CoID) " _
           & " Where Comst.coid = " & selectedcoid & " " _
           & " GROUP BY DeductMst.DName, DeductMst.DPan"
        End If

        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        Dim da As New OleDbDataAdapter
        cmd = New OleDbCommand(sql, cn)
        da.SelectCommand = cmd
        ds = New DataSet()
        da.Fill(ds)
        grids.DataSource = ds.Tables(0)
        sql = "Select CoName From CoMst Where  Coid = " & selectedcoid
        nds = New DataSet
        nds = FetchDataSet(sql)

        If nds.Tables(0).Rows.Count > 0 Then
            CO = nds.Tables(0).Rows(0)("CoName")
        End If


        nds.Dispose()
        expexcel()
    End Sub
    Private Sub gridhead()
        With grids
            .ColumnCount = 8
            .Columns(0).HeaderText() = "Deductee Name"
            .Columns(1).HeaderText() = "PAN"
            .Columns(2).HeaderText() = "Amount of Payment"
            .Columns(3).HeaderText() = "Total Tax"
            .Columns(4).HeaderText() = "Surcharge"
            .Columns(5).HeaderText() = "Ecess"
            .Columns(6).HeaderText() = "TDS Deducted"
            .Columns(7).HeaderText() = "TDS Deposited"
            .Columns(0).Width = 2500
            .Columns(1).Width = 2500
            .Columns(2).Width = 2500
            .Columns(3).Width = 2500
            .Columns(4).Width = 2500
            grids.Visible = True
            '.ColWidth(0) = 2500
            '.ColWidth(1) = 2000
            '.ColWidth(2) = 1500
            '.ColWidth(4) = 1000
            '.ColWidth(5) = 1500
            '.ColWidth(6) = 1500
            '.FixedCols = 0
        End With
    End Sub
    Private Sub cmdDedAdd_Click(sender As Object, e As EventArgs) Handles cmdDedAdd.Click
        'Dim Itm As ListViewItem
        Dim item14 As String
        oDed = New clsDeductee24QObj
        If cboRemark.SelectedIndex = -1 Then
            MsgBox("Select the Remark")

        ElseIf cboRemark.Text = "A" Or cboRemark.Text = "B" Then
            If Trim(txtCertNo.Text) = vbNullString Then
                MsgBox("Certificate no. is mandatory, if remark is A or B")
                Exit Sub
            End If
        End If
        If cboRemark.SelectedIndex <> 2 Then
            If Val(txtTotalTaxDeducted.Text) = 0# And Val(txtTotalTaxDeposited.Text) = 0# And Val(txtAmtPay.Text) = 0 Then
                MsgBox("All Amounts  cannot be blank, any one has to be feeded.", vbExclamation, "AMOUNT MISSING")
                Exit Sub
            End If
            If Val(txtAmtPay.Text) < Val(txtTotalTaxDeducted.Text) Then
                MsgBox("Amount Paid should be greater than or equal to Total tax deducted", vbExclamation, "AMOUNT MISSING")
                txtAmtPay.Focus()
                Exit Sub
            End If
        Else
            If Val(txtAmtPay.Text) = 0# And Val(txtTotalTaxDeducted.Text) = 0# Then
                MsgBox("Both Amount Paid and TDS Deducted cannot be blank, any one has to be feeded.", vbExclamation, "AMOUNT MISSING")
                txtAmtPay.Focus()
                Exit Sub
            End If

        End If

        If cboChallanNo.SelectedIndex = -1 Then
            MsgBox("Please Select Proper Challan", vbExclamation, "CHALLAN ALLOCATION")
            cboChallanNo.Focus()
            Exit Sub
        End If
        If Val(txtTotalTaxDeducted.Text) = 0# Then
            If cboRemark.SelectedIndex <> 2 Then
                If MsgBox("No TDS deducted for this deductee; Changing Date of Deduction to NIL" & vbCrLf &
            "Please Confirm", vbQuestion + vbYesNo + vbDefaultButton1, "Confirm?") = vbYes Then
                    dtpTDSDedDt.Text = "  /  /"
                Else
                    txtTDSAmt.Focus()
                    Exit Sub
                End If
            End If
        Else    'Amount exists...make tds deduction date complusory...
            If Not IsDate(dtpTDSDedDt.Text) = True Then
                MsgBox("Please enter correct Date of Deduction", vbInformation, "Enter Date")
                dtpTDSDedDt.Focus()
                Exit Sub
            End If
        End If

        If cboRemark.SelectedIndex <> 2 Then
            If Val(txtTotalTaxDeducted.Text) = 0# And Val(txtTotalTaxDeposited.Text) = 0# And Val(txtAmtPay.Text) = 0 Then
                MsgBox("All Amounts  cannot be blank, any one has to be feeded.", vbExclamation, "AMOUNT MISSING")
                Exit Sub
            End If
            If Val(txtAmtPay.Text) < Val(txtTotalTaxDeducted.Text) Then
                MsgBox("Amount Paid should be greater than or equal to Total tax deducted", vbExclamation, "AMOUNT MISSING")
                txtAmtPay.Focus()
                Exit Sub
            End If
        Else
            If Val(txtAmtPay.Text) = 0# And Val(txtTotalTaxDeducted.Text) = 0# Then
                MsgBox("Both Amount Paid and TDS Deducted cannot be blank, any one has to be feeded.", vbExclamation, "AMOUNT MISSING")
                txtAmtPay.Focus()
                Exit Sub
            End If
        End If

        If cboRemark.SelectedIndex <> 2 Then
            If (cboDedSection.SelectedIndex = -1 Or Trim(cboDedName.Text) = vbNullString Or
        Not IsDate(dtpAmtPayDt.Text)) Then    'Or Not IsDate(dtpCertDt)
                Call MsgBox("Some field is left blank, please fill up all the fields.", vbExclamation, "INCOMPLETE DATA")
                Exit Sub
            End If
        End If
        'Check Dates
        If CDate(dtpAmtPayDt.Text) < FromDate Then
            MsgBox("Date of payment cannot be less than " & Format(FromDateQ, "dd/MM/yyyy"), vbExclamation, "Date Error")
            dtpAmtPayDt.Focus()
            Exit Sub
        ElseIf CDate(dtpAmtPayDt.Text) > ToDateQ Then
            MsgBox("Date of payment cannot be beyond  " & Format(ToDateQ, "dd/MM/yyyy"), vbExclamation, "Date Error")
            dtpAmtPayDt.Focus()
            Exit Sub
        End If
        If cboDedSection.SelectedIndex = -1 Or Trim(cboDedName.Text) = vbNullString Or
        Not IsDate(dtpAmtPayDt.Text) Then
            Call MsgBox("Some field is left blank, please fill up all the fields.", vbExclamation, "INCOMPLETE DATA")
            Exit Sub
        End If
        If cboChallanNo.Text = "" And cboRemark.SelectedIndex <> 2 Then
            Call MsgBox("Challan No Can not be blank.", vbExclamation, "INCOMPLETE DATA")
            cboChallanNo.Focus()
            Exit Sub
        ElseIf cboChallanNo.Text = "" And cboRemark.SelectedIndex = 2 Then
            MsgBox("Even though no TDS payment is made, it is required that you assign a challan to this payment", vbInformation)
            cboChallanNo.Focus()
            Exit Sub
        End If
        If IsDate(dtpTDSDedDt.Text) Then
            If CDate(dtpTDSDedDt.Text) < FromDate Then
                MsgBox("Date of deduction cannot be less than " & Format(FromDateQ, "dd/MM/yyyy"), vbExclamation, "Date Error")
                dtpTDSDedDt.Focus()
                Exit Sub
            ElseIf CDate(dtpTDSDedDt.Text) > ToDateQ Then
                MsgBox("Date of deduction cannot be beyond  " & Format(ToDateQ, "dd/MM/yyyy"), vbExclamation, "Date Error")
                dtpTDSDedDt.Focus()
                Exit Sub
            End If
        End If

        'Changes made here should also be done at Enter Certificate Date Form...

        If cmdDedAdd.Text = "Add" Then
            'Add item..
            If oDed.Insert(oDed) = False Then
                MsgBox("Unable to Insert TDS Details in DataBase" & vbCrLf & "Call JAK Infosolutions", vbCritical, "CANNOT ADD NOW")
            Else

                Dim dt As Date
                Dim newitem As New ListViewItem()
                newitem.Text = cboDedSection.Text 'first column
                newitem.SubItems.Add(cboDedName.Text) 'second column
                newitem.SubItems.Add(txtDedPAN.Text)
                newitem.SubItems.Add(txtAmtPay.Text)
                dt = dtpAmtPayDt.Text

                newitem.SubItems.Add(dt.ToString("dd/MMM/yyyy"))
                newitem.SubItems.Add("")
                newitem.SubItems.Add("")
                newitem.SubItems.Add(txtTDSAmt.Text)
                newitem.SubItems.Add(txtDSurchrge.Text)
                newitem.SubItems.Add(txtDECess.Text)
                newitem.SubItems.Add(txtTotalTaxDeducted.Text)
                If dtpTDSDedDt.Text <> "  /  /" Then
                    dt = dtpTDSDedDt.Text

                    newitem.SubItems.Add(dt.ToString("dd/MMM/yyyy"))
                Else
                    newitem.SubItems.Add("")
                End If
                newitem.SubItems.Add(txtTotalTaxDeposited.Text)



                If cboChallanNo.SelectedIndex < 0 Then
                    item14 = ""
                    'Itm.SubItems(13) = 0
                    newitem.SubItems.Add(0)
                Else

                    'Itm.SubItems(13) = cboChallanNo.ItemData(cboChallanNo.ListIndex)

                    'item14 = Strings.Left(cboChallanNo.Text, InStr(1, cboChallanNo.Text, "- Rs.", vbTextCompare) - 1)
                    item14 = oDed.getChallanNo(cboChallanNo.SelectedValue)
                    'Dim cno As String = Strings.Left(item14, InStr(1, cboChallanNo.Text, " - ", vbTextCompare) - 1)
                    'Dim cdt As Date = Strings.Mid(item14, Len(cno) + 4)
                    'Dim id = oDed.GetChallanID(cno, cdt)
                    newitem.SubItems.Add(cboChallanNo.SelectedValue)
                End If
                newitem.SubItems.Add(item14)
                newitem.SubItems.Add(cboRemark.Text)
                newitem.SubItems.Add(txtCertNo.Text & "")
                newitem.SubItems.Add(oDed.ID24Q)
                lvwDeductee.Items.Add(newitem)
                Call NormalMode()
            End If
            'lvwDeductee.Items.Item(2)
            'Dim itm As lvwd
            '     Set subItm = Itm.ListSubItems(2)
            ''            Set subItm = Itm.ListSubItems(2)
            '            subItm.ForeColor = vbMagenta
            If lvwDeductee.SelectedIndices.Count > 0 Then
                lvwDeductee.Items(0).SubItems(2).ForeColor = Color.Magenta
            End If
        Else
            'Edit Item..
            If oDed.Update(oDed) = False Then
                MsgBox("Unable to update TDS Details in database" & vbCrLf & "Call JAK Infosolutions", vbCritical, "CANNOT UPDATE NOW")
            Else
                Dim dt As Date


                lvwDeductee.SelectedItems(0).SubItems(0).Text = cboDedSection.Text

                lvwDeductee.SelectedItems(0).SubItems(1).Text = cboDedName.Text
                lvwDeductee.SelectedItems(0).SubItems(2).Text = txtDedPAN.Text
                lvwDeductee.SelectedItems(0).SubItems(3).Text = txtAmtPay.Text
                dt = dtpAmtPayDt.Text

                lvwDeductee.SelectedItems(0).SubItems(4).Text = Format(dt, "dd/MMM/yyyy")
                'lvwDeductee.SelectedItems(0).SubItems(5).Text = chkDedBookEntry.Checked
                'lvwDeductee.SelectedItems(0).SubItems(6).Text = txtTDSRate.Text 'txtCertNo.Text & ""

                lvwDeductee.SelectedItems(0).SubItems(7).Text = txtTDSAmt.Text
                lvwDeductee.SelectedItems(0).SubItems(8).Text = txtDSurchrge.Text
                lvwDeductee.SelectedItems(0).SubItems(9).Text = txtDECess.Text
                lvwDeductee.SelectedItems(0).SubItems(10).Text = txtTotalTaxDeducted.Text
                If dtpTDSDedDt.Text <> "  /  /" Then
                    dt = dtpTDSDedDt.Text
                    lvwDeductee.SelectedItems(0).SubItems(11).Text = Format(dt, "dd/MMM/yyyy")
                Else
                    lvwDeductee.SelectedItems(0).SubItems(11).Text = ""
                End If
                lvwDeductee.SelectedItems(0).SubItems(12).Text = txtTotalTaxDeposited.Text

                If cboChallanNo.SelectedIndex < 0 Then
                    lvwDeductee.SelectedItems(0).SubItems(13).Text = 0
                    item14 = ""
                Else
                    item14 = oDed.getChallanNo(cboChallanNo.SelectedValue)
                    'item14 = Strings.Left(cboChallanNo.Text, InStr(1, cboChallanNo.Text, "- Rs.", vbTextCompare) - 1)
                    'Dim cno As String = Strings.Left(item14, InStr(1, cboChallanNo.Text, " - ", vbTextCompare) - 1)
                    'Dim cdt As Date = Strings.Mid(item14, Len(cno) + 4)
                    'Dim id = oDed.GetChallanID(cno, cdt)
                    lvwDeductee.SelectedItems(0).SubItems(13).Text = cboChallanNo.SelectedValue
                End If
                'If cboChallanNo.SelectedIndex < 0 Then
                '    item14 = ""
                'Else
                '    item14 = Strings.Left(cboChallanNo.Text, InStr(1, cboChallanNo.Text, "- Rs.", vbTextCompare) - 1)
                'End If
                lvwDeductee.SelectedItems(0).SubItems(14).Text = item14   'IIf(cboChallanNo.ListIndex < 0, "", )
                lvwDeductee.SelectedItems(0).SubItems(15).Text = IIf(cboRemark.SelectedIndex = 0, vbNullString, cboRemark.Text)
                lvwDeductee.SelectedItems(0).SubItems(16).Text = txtCertNo.Text
                lvwDeductee.SelectedItems(0).SubItems(17).Text = oDed.ID24Q
                Call NormalModeDeductee()
            End If
        End If
        cboDedSection.SelectedIndex = -1
        cboDedSection.Focus()

        clearDeducteeCtrls()

    End Sub

    Private Sub Label40_Click(sender As Object, e As EventArgs) Handles Label40.Click

    End Sub

    Private Sub cmdCnlEdit_Click(sender As Object, e As EventArgs) Handles cmdCnlEdit.Click

        Call ClearChallanCtrls()
        Call NormalMode()
        cboChallanSection.SelectedIndex = -1
        cboChallanSection.Focus()
    End Sub

    Private Sub cmdBankOpen_Click(sender As Object, e As EventArgs) Handles cmdBankOpen.Click
        Dim DefCode As Long
        DefCode = cboBankBrCode.SelectedIndex
        frmBankMst.ShowDialog() 'vbModal, Me
        'Call frmTDS.FillBankDetails(Me)
        FillBankDetails()
    End Sub
    Public Sub FillBankDetails()
        Dim nds As New DataSet, i As Integer
        'Filling BSR Code in Challan Detail
        nds = FetchDataSet("select BankBrCode from BankMst WHERE CoID=" & selectedcoid & " order by BankBrCode")
        cboBankBrCode.Items.Clear()

        If nds.Tables(0).Rows.Count > 0 Then
            For i = 0 To nds.Tables(0).Rows.Count - 1
                cboBankBrCode.Items.Add(nds.Tables(0).Rows(i)("BankBrCode"))
            Next
        End If

    End Sub

    Private Sub cmdChallanWiseExport_Click(sender As Object, e As EventArgs) Handles cmdChallanWiseExport.Click
        Call Export24_2XL_ChallanWiseDeductee()
    End Sub

    Private Sub cmdDedCancel_Click(sender As Object, e As EventArgs) Handles cmdDedCancel.Click
        Call clearDeducteeCtrls()
        Call NormalModeDeductee()
        'cboDedSection.SelectedIndex = -1
        cboDedSection.Focus()
    End Sub

    Private Sub cmddesum_Click(sender As Object, e As EventArgs) Handles cmddesum.Click
        framedt.Visible = True
    End Sub

    Private Sub cboCoState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCoState.SelectedIndexChanged

    End Sub
    Private Sub GetSecWiseCount()
        Dim sumCCount As Long, i As Long, j As Integer
        Dim sumDCount As Long
        Dim strC As String, strD As String
        Dim SumCAmt As Double, SumCAmtSecWise As Double, SumDPayAmt As Double, SumDTDSAmt As Double
        Dim SumDPayAmtSWise As Double, SumdTDSAmtSWise As Double, SumdTDSDeposited As Double
        strD = ""
        strC = ""

        For Each chk As CheckBox In PanelCheckBox.Controls
            i = i + 1
            If chk.Checked = True Then
                If InStr(chk.Name, "chkSection") = 1 Then
                    SumCAmtSecWise = 0
                    For j = 0 To lvwChallan.Items.Count - 1
                        'If lvwChallan.Items(j).Text = chk.Text Then
                        sumCCount = sumCCount + 1
                        SumCAmtSecWise = SumCAmtSecWise + Val(lvwChallan.Items(j).SubItems(1).Text) + Val(lvwChallan.Items(j).SubItems(2).Text) +
                            Val(lvwChallan.Items(j).SubItems(3).Text) + Val(lvwChallan.Items(j).SubItems(4).Text) + Val(lvwChallan.Items(j).SubItems(5).Text)
                        '  End If
                    Next
                    SumCAmt = SumCAmt + SumCAmtSecWise
                    SumDPayAmtSWise = 0
                    SumdTDSAmtSWise = 0
                    SumdTDSDeposited = 0
                    For j = 0 To lvwDeductee.Items.Count - 1
                        If lvwDeductee.Items(j).Text = chk.Text Then
                            sumDCount = sumDCount + 1
                            SumDPayAmtSWise = SumDPayAmtSWise + Val(lvwDeductee.Items(j).SubItems(3).Text)
                            SumdTDSAmtSWise = SumdTDSAmtSWise + Val(lvwDeductee.Items(j).SubItems(10).Text)
                            SumdTDSDeposited = SumdTDSDeposited + Val(lvwDeductee.Items(j).SubItems(12).Text)
                        End If
                    Next j
                    SumDPayAmt = SumDPayAmt + SumDPayAmtSWise
                    SumDTDSAmt = SumDTDSAmt + SumdTDSAmtSWise
                    strC = strC & "Total Challans for Section " & chk.Text & " = " & sumCCount & " AND Total Amount = " & SumCAmtSecWise & vbCrLf
                    strD = strD & "Total Deductees for Sec " & chk.Text & " = " &
                sumDCount & " AND Total Amount Paid = " & SumDPayAmtSWise &
                " Total TDS Deducted = " & SumdTDSAmtSWise & " Total TDS Deposited = " & SumdTDSDeposited & vbCrLf
                    sumCCount = 0
                    sumDCount = 0
                End If
            End If

        Next chk


        'code for calculating total challan amt if sec is not mentioned.
        Dim lvw As ListViewItem
        Dim Str As Double
        For Each lvw In lvwChallan.Items
            Str = Str + lvw.SubItems(7).Text
        Next
        txtCChallanSum.Text = "Total Challan Amt = " & Str

        txtDSumAmt.Text = "Total Amt Paid = " & SumDPayAmt & " - Total TDS = " & SumDTDSAmt
        txtCCountSec.Text = strC
        txtDCountSec.Text = strD
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        txtDCount.Text = "Total Deductee Records = " & lvwDeductee.Items.Count
        txtCCount.Text = "Total Challan Records = " & lvwChallan.Items.Count
        GetSecWiseCount()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If lblSections.BackColor = SystemColors.ButtonFace Then
            lblSections.BackColor = SystemColors.HighlightText
        Else
            lblSections.BackColor = SystemColors.ButtonFace
        End If
        Counter = Counter + 1
        If Counter > 20 Then
            lblSections.BackColor = SystemColors.ButtonFace
            Timer1.Enabled = False
        End If

    End Sub

    Private Sub cboBankBrCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBankBrCode.SelectedIndexChanged

    End Sub

    Private Sub cboBankBrCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboBankBrCode.KeyPress

        If AllowBSREntry Then
            cboBankBrCode.AutoCompleteMode = AutoCompleteMode.None
            cboBankBrCode.AutoCompleteSource = AutoCompleteSource.None
            If Asc(e.KeyChar) <> 8 Then
                If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                    e.Handled = True
                End If
            End If

        Else
            cboBankBrCode.AutoCompleteMode = AutoCompleteMode.Append
            cboBankBrCode.AutoCompleteSource = AutoCompleteSource.ListItems

        End If
        If Len(cboBankBrCode.Text) >= 7 Then
            If Asc(e.KeyChar) >= 32 Then
                'KeyAscii = 0
                e.KeyChar = ""
            End If
        End If
    End Sub
    Private Sub EditMode()
        With Me
            .lvwChallan.Enabled = False
            .cmdAdd.Text = "Save"
            .cmdCnlEdit.Enabled = True
            cboChallanSection.Focus()
        End With
    End Sub

    Private Sub EditModeDeductee()
        With Me
            .lvwDeductee.Enabled = False
            .cmdDedAdd.Text = "Save"
            .cmdDedCancel.Enabled = True
            .cboDedSection.Focus()
        End With
    End Sub
    Private Sub EditModeSD()
        With Me
            .lvwSD.Enabled = False
            .cmdSDAdd.Text = "Save"
            .cmdSDCancel.Enabled = True
        End With
    End Sub
    Private Sub frmTDS24Q_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' GetSecWiseCount()
        Dim i As Integer, SumChallan As Double, SumDeductee As Double
        For i = 0 To lvwChallan.Items.Count - 1

            SumChallan = SumChallan + Val(lvwChallan.Items(i).SubItems(7).Text)
        Next i
        For i = 0 To lvwDeductee.Items.Count - 1
            SumDeductee = SumDeductee + Val(lvwDeductee.Items(i).SubItems(12).Text)
        Next i
        If (SumChallan + SumDeductee) <> 0 Then
            If MsgBox("               Total of Challans: Rs." & Format(SumChallan, "###########0.00") _
                   & vbCrLf & "Total TDS Deposited: Rs." & Format(SumDeductee, "###########0.00") _
                   & vbCrLf & "                           Difference: Rs." & Format(SumChallan - SumDeductee, "###########0.00") _
                   & vbCrLf & txtDCountSec.Text _
                   & vbCrLf _
                   & vbCrLf & "Do You Want To Exit?" _
                   & vbCrLf _
                   , vbYesNo + vbExclamation + vbDefaultButton2, "Message") = vbYes Then
                e.Cancel = False
            Else
                e.Cancel = True
                Exit Sub
            End If
        End If

        lvwChallan.Items.Clear()
        lvwDeductee.Items.Clear()
        ClearChallanCtrls()
        clearDeducteeCtrls()
        oChln = Nothing
        oDed = Nothing
        Me.Dispose()
    End Sub

    Private Sub cmdShwfrm_Click(sender As Object, e As EventArgs) Handles cmdShwfrm.Click
        If cboDedName.SelectedIndex < 0 Then Exit Sub
        Dim frm As New frmdeduteeTDSMST
        Dim i As Long, DFound As Boolean, dname As String

        frm.Frm_typ = "24Q"
        frm.Show()
        frm.Hide()
        dname = cboDedName.Text
        With frm
            i = .cboDName.FindString(cboDedName.Text)
            If i >= 0 Then
                .cboDName.SelectedIndex = i

                DFound = True


            End If

        End With
        If DFound = True Then frm.Show()
        'refill the combo with new data...
        Call FillDeducteeCombo()


        'For i = 0 To cboDedName.Items.Count - 1
        '    cboDedName.SelectedIndex = i
        '    If cboDedName.SelectedValue = OldId Then
        '        'select the selection again...
        '        cboDedName.SelectedIndex = i
        '        'cboDedName.Focus()
        '        Exit For
        '    End If

        'Next i
        i = cboDedName.FindString(dname)
        If i >= 0 Then
            cboDedName.SelectedIndex = i

        End If
    End Sub

    Private Sub lvwDeductee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwDeductee.SelectedIndexChanged

    End Sub

    Private Sub lvwDeductee_DoubleClick(sender As Object, e As EventArgs) Handles lvwDeductee.DoubleClick
        Call EditRow("D")
        Call EditModeDeductee()
    End Sub


    Private Sub fillcboDedChallan1(Sec As String)
        Dim sql As String
        Dim sql1 As String
        Dim nds As New DataSet
        Dim nds1 As New DataSet         'For getting the totals of deductee challan
        Dim CBalance As Long, i As Integer                   'For getting the balance of unallocated challan amount
        'Filling Challan in Deductee Detail
        Dim prevCID As Long
        sql1 = " And  RetnID=" & Me.Tag & " "
        If chkallsec.Checked = False Then
            sql1 = sql1 & "and sec='" & Sec & "' "
        End If


        sql = " SELECT challanid,iif(isnull(BankChallanNo),Null,BankChallanNo),DtOfChallan,TotalTax" _
        & " FROM Challan24Q WHERE (BankChallanNo<>Null or BankChallanNo<>0)" & sql1 _
        & " UNION ALL SELECT challanid,iif(isnull(BankChallanNo),Null,BankChallanNo),DtOfChallan,TotalTax" _
        & " FROM Challan24Q WHERE (Taxamt = 0 and (isnull(BankChallanNo) or BankChallanNo=0))" & sql1 _
        & " UNION ALL SELECT challanid,TranVouNo,DtOfChallan,TotalTax " _
        & " FROM Challan24Q WHERE (TranVouNo<>Null and TranVouNo<>0)" & sql1 _
        & " order by ChallanID"


        nds = FetchDataSet(sql)
        ' cboChallanNo.Items.Clear()
        Dim newDataset As New DataSet
        Dim dt As DataTable
        Dim dr As DataRow
        Dim icol As DataColumn
        Dim namecol As DataColumn
        dt = New DataTable
        newDataset = New DataSet
        icol = New DataColumn("ChallanID", Type.GetType("System.Int32"))
        namecol = New DataColumn("ChallanNo", Type.GetType("System.String"))
        dt.Columns.Add(icol)
        dt.Columns.Add(namecol)

        For i = 0 To nds.Tables(0).Rows.Count - 1
            nds1 = FetchDataSet("select sum(TotalTaxDeposited) as TChallan from Deductee24Q WHERE ChallanID=" & nds.Tables(0).Rows(i)("ChallanID"))
            CBalance = Format(nds.Tables(0).Rows(i)("TotalTax") - IIf(nds1.Tables(0).Rows(0)("TChallan").ToString() = "", 0, nds1.Tables(0).Rows(0)("TChallan")), "0")
            'cboChallanNo.Items.Add(nds.Tables(0).Rows(i)(1) & " - " & Format(nds.Tables(0).Rows(i)("DtOfChallan"), "dd/MM/yy") & "- Rs." & CBalance)
            ''cboChallanNo.ValueMember = nds.Tables(0).Rows(i)("ChallanID")
            'cboChallanNo.SelectedIndex = i
            dr = dt.NewRow()
            dr("ChallanID") = nds.Tables(0).Rows(i)(0)
            dr("ChallanNo") = nds.Tables(0).Rows(i)(1) & " - " & Format(nds.Tables(0).Rows(i)("DtOfChallan"), "dd/MM/yy") & "- Rs." & CBalance
            dt.Rows.Add(dr)


        Next
        newDataset.Tables.Add(dt)
        nds.Dispose()
        nds1.Dispose()

        prevCID = cboChallanNo.SelectedValue

        cboChallanNo.DataSource = Nothing
        cboChallanNo.Items.Clear()
        cboChallanNo.DataSource = newDataset.Tables(0)
        cboChallanNo.DisplayMember = "ChallanNo"
        cboChallanNo.ValueMember = "ChallanID"

        newDataset.Dispose()
        cboChallanNo.SelectedIndex = -1
        If cmdDedAdd.Text = "Save" Then

            If prevCID > 0 Then
                cboChallanNo.SelectedValue = prevCID
            End If
        End If
        'dt.Dispose()
    End Sub

    Private Sub cboDedSection_Click(sender As Object, e As EventArgs) Handles cboDedSection.Click
        'Call fillcboDedChallan1(cboDedSection.Text)
    End Sub


    Private Sub cboDedSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDedSection.SelectedIndexChanged

        ' Call fillcboDedChallan1(cboDedSection.Text)
    End Sub

    Private Sub cboDedSection_LostFocus(sender As Object, e As EventArgs) Handles cboDedSection.LostFocus

    End Sub

    Private Sub cboDedSection_Validating(sender As Object, e As CancelEventArgs) Handles cboDedSection.Validating
        If Me.TabPage1.ContainsFocus Or Me.TabPage2.ContainsFocus Or cmdDedCancel.ContainsFocus Then
            Exit Sub
        End If
        If cboDedSection.SelectedIndex = -1 Then
            Call MsgBox("Please select the section under which you have deducted" _
                & vbCrLf & "the tax at source.  This is necessary for futher calculations." _
                , vbExclamation, "SELECT SECTION")
            e.Cancel = True
        End If
    End Sub

    Private Sub txtDECess_TextChanged(sender As Object, e As EventArgs) Handles txtDECess.TextChanged
        CalcTotalDeducteeTDS()
    End Sub

    Private Sub txtDSurchrge_TextChanged(sender As Object, e As EventArgs) Handles txtDSurchrge.TextChanged
        CalcTotalDeducteeTDS()
    End Sub

    Private Sub txtTDSAmt_TextChanged(sender As Object, e As EventArgs) Handles txtTDSAmt.TextChanged
        CalcTotalDeducteeTDS()
    End Sub

    Private Sub txtTotalTaxDeducted_TextChanged(sender As Object, e As EventArgs) Handles txtTotalTaxDeducted.TextChanged
        'If Val(txtAmtPay.Text) <= 0 Then
        '    txtTDSRate.Text = Format(0, "#0.0000")
        '    Exit Sub
        'End If
        'txtTDSRate.Text = Format((Val(txtTotalTaxDeducted.Text) / Val(txtAmtPay.Text)) * 100, "00.0000")
    End Sub

    Private Sub cmdDedAdd_CausesValidationChanged(sender As Object, e As EventArgs) Handles cmdDedAdd.CausesValidationChanged

    End Sub

    Private Sub cmdCancel1_Click(sender As Object, e As EventArgs) Handles cmdCancel1.Click
        framedt.Visible = False
    End Sub

    Private Sub lvwChallan_MouseUp(sender As Object, e As MouseEventArgs) Handles lvwChallan.MouseUp
        If e.Button = MouseButtons.Right Then
            popupmenu.Show(lvwChallan, New Point(e.X, e.Y))

        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If lvwChallan.SelectedIndices.Count = 0 Then Exit Sub
        oChln = New ClsChallan24QObj

        If oChln.LinkDed24Q(lvwChallan.SelectedItems(0).SubItems(16).Text) = True Then
            MsgBox("This Record is further used, Cannot Delete", vbInformation, "Caution")
            Exit Sub
        End If
        If MsgBox("Do you want to delete this row?", vbYesNo + vbQuestion + vbDefaultButton2, "DELETE DATA") = vbYes Then
            'delete the row..

            If oChln.Delete(lvwChallan.SelectedItems(0).SubItems(16).Text) = True Then
                lvwChallan.SelectedItems(0).Remove()
            End If
        End If
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        If lvwChallan.SelectedIndices.Count = 0 Then Exit Sub
        EditRow("C")
        EditMode()
        cboChallanSection.Focus()
    End Sub

    'Private Sub chkSection3_CheckedChanged(sender As Object, e As EventArgs) Handles chkSection3.CheckedChanged
    '    If chkSection3.Checked = False Then

    '        chkSection3.Checked = chkSectionClick(chkSection3.Text)
    '    End If
    'End Sub






    Private Sub cboChallanSection_LostFocus(sender As Object, e As EventArgs) Handles cboChallanSection.LostFocus
        Call CtrlLostFocus(cboChallanSection)
    End Sub


    Private Sub txtAmtDeducted_GotFocus(sender As Object, e As EventArgs) Handles txtAmtDeducted.GotFocus

    End Sub

    Private Sub txtChallanNo_TextChanged(sender As Object, e As EventArgs) Handles txtChallanNo.TextChanged

    End Sub

    Private Sub txtChallanNo_Validating(sender As Object, e As CancelEventArgs) Handles txtChallanNo.Validating

        txtChallanNo.Text = SetFormat("00000", (txtChallanNo.Text))

    End Sub

    Private Sub dtpChallanDate_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles dtpChallanDate.MaskInputRejected

    End Sub


    Private Sub dtpChallanDate_Leave(sender As Object, e As EventArgs) Handles dtpChallanDate.Leave
        dtpChallanDate.SelectionLength = 0
        dtpChallanDate.BackColor = Color.White
    End Sub

    Private Sub dtpChallanDate_Enter(sender As Object, e As EventArgs) Handles dtpChallanDate.Enter
        'dtpChallanDate.BackColor = Color.LightYellow
        CtrlGotFocusDate(dtpChallanDate)
    End Sub

    Private Sub dtpChallanDate_Validating(sender As Object, e As CancelEventArgs) Handles dtpChallanDate.Validating
        If Not IsDate(dtpChallanDate.Text) Then
            MsgBox("Invalid Challan Date", vbCritical)
            e.Cancel = True
        End If
    End Sub


    Private Sub txtTranVouNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTranVouNo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtAIntt_TextChanged(sender As Object, e As EventArgs) Handles txtAIntt.TextChanged
        If txtAIntt.Text <> txtIntt.Text Then
            txtAIntt.ForeColor = Color.Red
        Else
            txtAIntt.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtAOthers_TextChanged(sender As Object, e As EventArgs) Handles txtAOthers.TextChanged
        If txtAOthers.Text <> txtOthers.Text Then
            txtAOthers.ForeColor = Color.Red
        Else
            txtAOthers.ForeColor = Color.Black
        End If
    End Sub


    Private Sub cboDedName_Click(sender As Object, e As EventArgs) Handles cboDedName.Click
        'Dim rate As Rates
        'If txtDedPAN.Tag = "O" Then
        '    'txtTDSRate.ToolTipText = "Default Rate of TDS for this assessee under this section is " & rate.RateNonCompany
        '    If txtTDSRate.Text = vbNullString Then AutoCalcReqd = True : txtTDSRate.Text = rate.RateNonCompany
        'ElseIf txtDedPAN.Tag = "C" Then
        '    'txtTDSRate.ToolTipText = "Default Rate of TDS for this assessee under this section is " & rate.RateCompany
        '    If txtTDSRate.Text = vbNullString Then AutoCalcReqd = True : txtTDSRate.Text = rate.RateCompany
        'End If
        ''Recalculate the amount...
        'If AutoCalcReqd = True Then
        '    txtTDSAmt.Text = Format((Val(txtAmtPay.Text) * (Val(txtTDSRate.Text) / 100)), "#########.#0")
        '    AutoCalcReqd = False
        'End If


    End Sub

    Private Sub cboDedName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboDedName.KeyPress

        e.KeyChar = UCase(e.KeyChar)

        If Asc(e.KeyChar) = 34 Or Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

    End Sub

    Private Sub cboDedName_Validating(sender As Object, e As CancelEventArgs) Handles cboDedName.Validating
        If Trim(cboDedName.Text) = vbNullString Then
            MsgBox("Name cannot be left blank", vbInformation, "NO NAME")
            e.Cancel = True
        Else
            If cboDedName.SelectedIndex = -1 Then
                cmdShwfrm_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub dtpTDSDedDt_Enter(sender As Object, e As EventArgs) Handles dtpTDSDedDt.Enter

        'dtpTDSDedDt.BackColor = Color.LightYellow
        CtrlGotFocusDate(dtpTDSDedDt)
    End Sub

    Private Sub cboDedName_GotFocus(sender As Object, e As EventArgs) Handles cboDedName.GotFocus
        'SendKeys.Send("{f4}")

    End Sub

    Private Sub cboRemark_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRemark.SelectedIndexChanged
        If cboRemark.SelectedIndex = 2 Then
            dtpTDSDedDt.Text = "  /  /"
            txtTDSAmt.Text = vbNullString
            txtDSurchrge.Text = vbNullString
            txtDECess.Text = vbNullString
            cboChallanNo.Text = ""
            dtpTDSDedDt.Enabled = False
            txtTDSAmt.Enabled = False
            txtDSurchrge.Enabled = False
            txtDECess.Enabled = False
            'txtTotalTaxDeposited.Enabled = False
            'cboChallanNo.Enabled = False
        Else
            dtpTDSDedDt.Enabled = True
            txtTDSAmt.Enabled = True
            txtDSurchrge.Enabled = True
            txtDECess.Enabled = True
            'txtTotalTaxDeposited.Enabled = True
            cboChallanNo.Enabled = True
        End If


    End Sub

    Private Sub txtAmtPay_TextChanged(sender As Object, e As EventArgs) Handles txtAmtPay.TextChanged
        'If Val(txtAmtPay.Text) <= 0 Then
        '    txtTDSRate.Text = Format(0, "#0.0000")
        '    Exit Sub
        'End If
        'txtTDSRate.Text = Format((Val(txtTotalTaxDeducted.Text) / Val(txtAmtPay.Text)) * 100, "#0.0000")

    End Sub

    Private Sub txtAmtPay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmtPay.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dtpAmtPayDt_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles dtpAmtPayDt.MaskInputRejected

    End Sub

    Private Sub dtpAmtPayDt_TextChanged(sender As Object, e As EventArgs) Handles dtpAmtPayDt.TextChanged
        dtpTDSDedDt.Text = dtpAmtPayDt.Text
    End Sub

    Private Sub dtpAmtPayDt_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpAmtPayDt.KeyDown
        'If e.KeyCode = Keys.Return Then SendKeys.Send("{tab}")
    End Sub

    Private Sub dtpAmtPayDt_Validating(sender As Object, e As CancelEventArgs) Handles dtpAmtPayDt.Validating
        If Not IsDate(dtpAmtPayDt.Text) Then
            MsgBox("Invalid Date of Payment", vbCritical)
            e.Cancel = True
        End If
    End Sub

    Private Sub txtDECess_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDECess.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtTDSAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTDSAmt.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtDSurchrge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDSurchrge.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dtpTDSDedDt_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles dtpTDSDedDt.MaskInputRejected

    End Sub

    Private Sub dtpTDSDedDt_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpTDSDedDt.KeyDown
        'If e.KeyCode = Keys.Return Then SendKeys.Send("{tab}")
    End Sub

    Private Sub dtpTDSDedDt_Validating(sender As Object, e As CancelEventArgs) Handles dtpTDSDedDt.Validating
        If Not IsDate(dtpTDSDedDt.Text) Then
            MsgBox("Invalid Date of Deduction", vbCritical)
            e.Cancel = True
        ElseIf CDate(dtpTDSDedDt.Text) < FromDateQ Or CDate(dtpTDSDedDt.Text) > ToDateQ Then
            MsgBox("Date of deduction should be within this quarter.", vbCritical)
            e.Cancel = True
        End If
    End Sub

    Private Sub txtTotalTaxDeposited_TextChanged(sender As Object, e As EventArgs) Handles txtTotalTaxDeposited.TextChanged

    End Sub

    Private Sub lvwDeductee_MouseUp(sender As Object, e As MouseEventArgs) Handles lvwDeductee.MouseUp
        If e.Button = MouseButtons.Right Then
            popupmenuD.Show(lvwDeductee, New Point(e.X, e.Y))

        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If lvwDeductee.SelectedIndices.Count = 0 Then Exit Sub
        EditRow("D")
        EditModeDeductee()
        cboDedSection.Focus()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If lvwDeductee.SelectedIndices.Count = 0 Then Exit Sub
        If MsgBox("Do you want to delete this row?", vbYesNo + vbQuestion + vbDefaultButton2, "DELETE DATA") = vbYes Then
            'delete the row..
            oDed = New clsDeductee24QObj

            If oDed.Delete(lvwDeductee.SelectedItems(0).SubItems(17).Text) = True Then
                lvwDeductee.SelectedItems(0).Remove()
            End If
        End If
    End Sub



    Private Sub cboDedSection_KeyDown(sender As Object, e As KeyEventArgs) Handles cboDedSection.KeyDown
        'If cboDedSection.Text <> "" Then
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
        'End If
    End Sub

    Private Sub cboRemark_KeyDown(sender As Object, e As KeyEventArgs) Handles cboRemark.KeyDown
        'If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtAmtPay_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAmtPay.KeyDown
        'If e.KeyCode = Keys.Return Then SendKeys.Send("{tab}")
    End Sub

    Private Sub txtAmtPay_LostFocus(sender As Object, e As EventArgs) Handles txtAmtPay.LostFocus

        Call CtrlLostFocus(txtAmtPay)
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub cboChallanSection_KeyDown(sender As Object, e As KeyEventArgs) Handles cboChallanSection.KeyDown
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboBankBrCode_KeyDown(sender As Object, e As KeyEventArgs) Handles cboBankBrCode.KeyDown
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
    End Sub



    Private Sub cboChallanNo_KeyDown(sender As Object, e As KeyEventArgs) Handles cboChallanNo.KeyDown
        If cboChallanNo.Text <> "" Then
            If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkDedBookEntry_Leave(sender As Object, e As EventArgs)
        cmdDedAdd.Focus()
    End Sub

    Private Sub txtCertNo_TextChanged(sender As Object, e As EventArgs) Handles txtCertNo.TextChanged

    End Sub

    Private Sub txtTDSRate_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cboDedName_Leave(sender As Object, e As EventArgs) Handles cboDedName.Leave

        Dim rate As Rates
        Dim nds As New DataSet, i As Long
        Dim DName As String
        'Call CtrlLostFocus(cboDedName)
        If Trim(cboDedName.Text) = vbNullString Then Exit Sub
        DName = UCase(cboDedName.Text)
        nds = FetchDataSet("SELECT * FROM DeductMst WHERE CoId = " & selectedcoid & " And DName= '" & DName & "'")
        If nds.Tables(0).Rows.Count <= 0 Then
            'not found., open deductee detail form..
            'Load frmDeducteeTDS
            frmDeducteeTDS.Frm_typ = 24
            'frmDeducteeTDS.Move(Me.Left + cboDedName.Left) + 100, (Me.Top + cboDedName.Top + cboDedName.Height + 650)
            frmDeducteeTDS.txtDName.Text = cboDedName.Text
            frmDeducteeTDS.ShowDialog()
            FillDeducteeCombo()

            For i = 0 To cboDedName.Items.Count - 1
                cboDedName.SelectedIndex = i
                If cboDedName.Text = DName Then
                    cboDedName.SelectedIndex = i
                    Exit For
                End If


            Next i
            If i = cboDedName.Items.Count Then cboDedName.SelectedIndex = -1          'Name Not Found
        Else
            txtDedPAN.Text = nds.Tables(0).Rows(0)("DPan") & ""
            txtDedPAN.Tag = nds.Tables(0).Rows(0)("DType")
            Dim Forthpan As String
            If txtDedPAN.Text <> "" Then
                Forthpan = Strings.Right(Strings.Left(txtDedPAN.Text, 4), 1)
                If Forthpan <> "P" Then
                    MsgBox("Deductee PAN cannot be other than that of the individual(4th Character of PAN Should be 'P'")
                End If
            End If
        End If

        nds.Dispose()

        ''Recalculate the amount...
        'If AutoCalcReqd = True Then
        '    txtTDSAmt.Text = Format((Val(txtAmtPay.Text) * (Val(txtTDSRate.Text) / 100)), "#########.#0")
        '    AutoCalcReqd = False
        'End If

    End Sub
    Private Sub expexcel()
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim R As Integer
        Dim c As Integer
        'xlapp = Nothing

        xlapp = New Excel.Application
        xlBook = xlapp.Workbooks.Add
        xlSheet = xlBook.Worksheets("Sheet1")
        '  xlapp.Visible = True
        xlSheet = xlBook.Sheets("Sheet1")
        xlSheet = xlBook.ActiveSheet
        xlSheet.Name = "Export sheet"

        'xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(grids.Rows.Count, grids.Columns.Count - 1)).ClearFormats()

        If cboDedName.SelectedIndex > -1 Then
            xlSheet.Cells(2, 1) = "Deductee Name :- "
            xlSheet.Cells(2, 2) = cboDedName.Text
            xlSheet.Cells(2, 2).Font.Color = Color.Blue
            xlSheet.Cells(2, 1).Font.Bold = True
            xlSheet.Cells(2, 1).Font.Color = Color.Blue
        End If
        '    if qtr
        ' xlSheet.Cells(2, 4) = "From"
        '    xlSheet.Cells(2, 5) = frdt.Value
        '    xlSheet.Cells(2, 6) = "To"
        '    xlSheet.Cells(2, 7) = todt.Value
        '    xlSheet.Cells(2, 5) = frdt.Value
        xlSheet.Cells(2, 5) = IIf(OptQ1.Checked = True, "24Q1", IIf(OptQ2.Checked = True, "24Q2", IIf(OptQ3.Checked = True, "24Q3", IIf(OptQ4.Checked = True, "24Q4", "All Quarter"))))
        xlSheet.Range(xlSheet.Cells(2, 1), xlSheet.Cells(2, 8)).Merge()
        xlSheet.Cells(1, 1) = "Company Name :- "
        xlSheet.Cells(1, 1).Font.Color = Color.Blue
        xlSheet.Cells(1, 1).Font.Bold = True
        xlSheet.Cells(1, 2) = CO
        xlSheet.Cells(1, 2).Font.Color = Color.Blue
        xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 1)).Value = "Deductee Name"
        xlSheet.Range(xlSheet.Cells(3, 2), xlSheet.Cells(3, 2)).Value = "PAN"
        xlSheet.Range(xlSheet.Cells(3, 3), xlSheet.Cells(3, 3)).Value = "Amount of Payment"
        xlSheet.Range(xlSheet.Cells(3, 4), xlSheet.Cells(3, 4)).Value = "Total Tax "
        xlSheet.Range(xlSheet.Cells(3, 5), xlSheet.Cells(3, 5)).Value = "Surcharge"
        xlSheet.Range(xlSheet.Cells(3, 6), xlSheet.Cells(3, 6)).Value = "Ecess"
        xlSheet.Range(xlSheet.Cells(3, 7), xlSheet.Cells(3, 7)).Value = "TDS Deducted"
        xlSheet.Range(xlSheet.Cells(3, 8), xlSheet.Cells(3, 8)).Value = "TDS Deposited"
        xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 8)).BorderAround()
        xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 8)).Font.Bold = True

        Dim i As Integer
        Dim j As Integer
        i = 4

        For R = 0 To grids.Rows.Count - 1
                j = 1
                For c = 0 To grids.Columns.Count - 1
                    xlSheet.Cells(i, j) = grids.Rows(R).Cells(c).Value
                    xlSheet.Range(xlSheet.Cells(i, j), xlSheet.Cells(i, j)).BorderAround()
                    'xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 1)).Font.Bold = True
                    j = j + 1
                Next
                i = i + 1
            Next

        xlSheet.Cells(i, 1) = "Total"
        xlSheet.Range(xlSheet.Cells(i, 1), xlSheet.Cells(i, 2)).Merge()
        xlSheet.Range(xlSheet.Cells(i, 1), xlSheet.Cells(i, 2)).BorderAround()
        xlSheet.Cells(i, 3) = "=sum(c4:c" & i - 1 & ")"
        xlSheet.Cells(i, 3).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(i, 3), xlSheet.Cells(i, 3)).BorderAround()
        xlSheet.Cells(i, 4) = "=sum(d4:d" & i - 1 & ")"
        xlSheet.Cells(i, 4).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(i, 4), xlSheet.Cells(i, 4)).BorderAround()

        xlSheet.Cells(i, 5) = "=sum(e4:e" & i - 1 & ")"
        xlSheet.Cells(i, 5).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(i, 5), xlSheet.Cells(i, 5)).BorderAround()

        xlSheet.Cells(i, 6) = "=sum(f4:f" & i - 1 & ")"
        xlSheet.Cells(i, 6).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(i, 6), xlSheet.Cells(i, 6)).BorderAround()

        xlSheet.Cells(i, 7) = "=sum(g4:g" & i - 1 & ")"
        xlSheet.Cells(i, 7).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(i, 7), xlSheet.Cells(i, 7)).BorderAround()

        xlSheet.Cells(i, 8) = "=sum(h4:h" & i - 1 & ")"
        xlSheet.Cells(i, 8).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(i, 8), xlSheet.Cells(i, 8)).BorderAround()
        xlSheet.Calculate()


        xlSheet.Range(xlSheet.Cells(i, 1), xlSheet.Cells(i, 8)).Font.Color = Color.Red
        'xlSheet.Range("A1", "H999").Font.Size = 8
        xlSheet.UsedRange.Rows.Columns.AutoFit()
        xlapp.Application.Visible = True
    End Sub
    Private Sub cmdDedDExcel_Click(sender As Object, e As EventArgs) Handles cmdDedDExcel.Click
        Dim CntLstItem As Integer, CntColHed As Integer
        Dim i As Integer, m As Integer, d As Integer
        Dim N As Single, j As Integer, k As Integer
        'Timer4.Enabled = False
        'cmdperprint.Visible = False
        cmdSummary.Visible = False
        cntdecdt()    'use Count deductee's
        On Error GoTo excelerr
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim rs As New DataSet
        Dim R As Integer, c As Integer
        xlapp = New Excel.Application
        xlBook = xlapp.Workbooks.Add
        xlSheet = xlBook.Worksheets("Sheet1")
        'xlapp.Visible = True
        xlSheet = xlBook.Sheets("Sheet1")
        xlSheet = xlBook.ActiveSheet
        xlSheet.Name = "Export sheet"
        'xlapp = Nothing
        'xlBook = Nothing
        'xlSheet = Nothing
        'xlapp = New Excel.Application
        'xlBook = xlapp.Workbooks.Add
        'xlSheet = xlBook.Worksheets("Sheet1")

        With lvwDeductee
            CntLstItem = .Items().Count
            CntColHed = .Columns.Count - 2
            xlSheet.Cells(3, 1) = "Section"
            xlSheet.Cells(3, 2) = .Columns(1).Text
            xlSheet.Cells(3, 3) = .Columns(2).Text
            xlSheet.Cells(3, 4) = .Columns(3).Text
            xlSheet.Cells(3, 5) = .Columns(4).Text
            xlSheet.Cells(3, 6) = .Columns(7).Text
            xlSheet.Cells(3, 7) = .Columns(8).Text
            xlSheet.Cells(3, 8) = .Columns(9).Text
            xlSheet.Cells(3, 9) = .Columns(10).Text
            xlSheet.Cells(3, 10) = .Columns(11).Text
            xlSheet.Cells(3, 11) = .Columns(12).Text
            xlSheet.Cells(3, 12) = .Columns(14).Text
            xlSheet.Cells(3, 13) = .Columns(15).Text
            'xlSheet.Cells(3, 14) = .Columns(13).Text
            'xlSheet.Cells(3, 15) = .Columns(15).Text
            'xlSheet.Cells(3, 16) = .Columns(16).Text

            For d = 1 To 13  'for bold
                xlSheet.Range(xlSheet.Cells(3, d), xlSheet.Cells(3, d)).Font.Bold = True
                'xlSheet.Range(xlSheet.Cells(3, 6), xlSheet.Cells(3, 6)).EntireColumn.Hidden = True
                'xlSheet.Range(xlSheet.Cells(3, 7), xlSheet.Cells(3, 7)).EntireColumn.Hidden = True
                xlSheet.Range(xlSheet.Cells(3, d), xlSheet.Cells(3, d)).BorderAround()
                xlSheet.Range(xlSheet.Cells(3, d), xlSheet.Cells(3, d)).VerticalAlignment = ContentAlignment.TopCenter
            Next d
            For R = 0 To lvwDeductee.Items.Count - 1
                xlSheet.Cells(R + 4, 1) = lvwDeductee.Items(R).SubItems(0).Text '.ListVItems.add(R)
                xlSheet.Cells(R + 4, 2) = lvwDeductee.Items(R).SubItems(1).Text
                xlSheet.Cells(R + 4, 3) = lvwDeductee.Items(R).SubItems(2).Text
                xlSheet.Cells(R + 4, 4) = Val(lvwDeductee.Items(R).SubItems(3).Text)
                xlSheet.Cells(R + 4, 5) = lvwDeductee.Items(R).SubItems(4).Text

                '   xlSheet.Cells(r + 3, 6) = Val(.ListItems(r).ListSubItems(5))
                xlSheet.Cells(R + 4, 6) = Val(lvwDeductee.Items(R).SubItems(7).Text) '= True, "Y", "N")
                xlSheet.Cells(R + 4, 7) = Val(lvwDeductee.Items(R).SubItems(8).Text)
                xlSheet.Cells(R + 4, 8) = Val(lvwDeductee.Items(R).SubItems(9).Text)
                xlSheet.Cells(R + 4, 9) = Val(lvwDeductee.Items(R).SubItems(10).Text)
                xlSheet.Cells(R + 4, 10) = lvwDeductee.Items(R).SubItems(11).Text
                xlSheet.Cells(R + 4, 11) = Val(lvwDeductee.Items(R).SubItems(12).Text)
                xlSheet.Cells(R + 4, 12) = lvwDeductee.Items(R).SubItems(14).Text

                xlSheet.Cells(R + 4, 13) = IIf(lvwDeductee.Items(R).SubItems(15).Text = " ", "N", lvwDeductee.Items(R).SubItems(15).Text) 'Val(lvwDeductee.Items(R).SubItems(12).Text)
                'xlSheet.Cells(R + 4, 14) = lvwDeductee.Items(R).SubItems(14).Text
                'xlSheet.Cells(R + 4, 15) = IIf(lvwDeductee.Items(R).SubItems(15).Text = " ", "N", lvwDeductee.Items(R).SubItems(15).Text)
                xlSheet.Range(xlSheet.Cells(R + 4, 1), xlSheet.Cells(R + 4, 1)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 2), xlSheet.Cells(R + 4, 2)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 3), xlSheet.Cells(R + 4, 3)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 4), xlSheet.Cells(R + 4, 4)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 5), xlSheet.Cells(R + 4, 5)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 6), xlSheet.Cells(R + 4, 6)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 7), xlSheet.Cells(R + 4, 7)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 8), xlSheet.Cells(R + 4, 8)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 9), xlSheet.Cells(R + 4, 9)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 11), xlSheet.Cells(R + 4, 11)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 12), xlSheet.Cells(R + 4, 12)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 13), xlSheet.Cells(R + 4, 13)).BorderAround()
                'xlSheet.Range(xlSheet.Cells(R + 4, 14), xlSheet.Cells(R + 4, 14)).BorderAround()
                'xlSheet.Range(xlSheet.Cells(R + 4, 15), xlSheet.Cells(R + 4, 15)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 4, 10), xlSheet.Cells(R + 4, 10)).BorderAround()
                ' xlSheet.UsedRange.Cells.Columns.AutoFit()

                'If lvwDeductee.Items(R).SubItems(5).Text = True Then
                '    xlSheet.Cells(R + 4, 6) = "Y"
                'Else
                '    xlSheet.Cells(R + 4, 6) = "N"
                'End If
                '-------------------------------------------------------
            Next R
            'xlSheet.Range(xlSheet.Cells(3, 6), xlSheet.Cells(3, 6)).EntireColumn.Hidden = True
            'xlSheet.Range(xlSheet.Cells(3, 7), xlSheet.Cells(3, 7)).EntireColumn.Hidden = True
            xlSheet.Cells(2, 1) = "Deductee's Detail List Of Form " & quter
            xlSheet.Cells(1, 1) = txtCoName.Text & " (FY  " & FY & ") "
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 1)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 16)).Merge()
            xlSheet.Range(xlSheet.Cells(2, 1), xlSheet.Cells(2, 16)).Merge()
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 16)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.Cells(R + 4, 1) = "Total"
            xlSheet.Range(xlSheet.Cells(R + 4, 1), xlSheet.Cells(R + 4, 3)).Merge()
            xlSheet.Range(xlSheet.Cells(R + 4, 1), xlSheet.Cells(R + 4, 3)).BorderAround()
            Dim t1, t2, t3, t4, t5, t6, t7 As String
            t1 = "=sum(d4:d" & R + 3 & ")"
            xlSheet.Cells(R + 4, 4) = t1
            xlSheet.Cells(R + 4, 4).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 4, 4), xlSheet.Cells(R + 4, 4)).BorderAround()
            t7 = "=sum(g4:g" & R + 3 & ")"
            xlSheet.Cells(R + 4, 7) = t7
            xlSheet.Cells(R + 4, 7).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 4, 7), xlSheet.Cells(R + 4, 7)).BorderAround()
            'Sum of Amount of TDS
            t2 = "=sum(h4:h" & R + 3 & ")"
            xlSheet.Cells(R + 4, 8) = t2
            xlSheet.Cells(R + 4, 8).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 4, 8), xlSheet.Cells(R + 4, 8)).BorderAround()
            'Sum of Surcharge
            t3 = "=sum(i4:i" & R + 3 & ")"
            xlSheet.Cells(R + 4, 9) = t3
            xlSheet.Cells(R + 4, 9).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 4, 9), xlSheet.Cells(R + 4, 9)).BorderAround()
            'Sum of Ecess
            t4 = "=sum(f4:f" & R + 3 & ")"
            xlSheet.Cells(R + 4, 6) = t4
            xlSheet.Cells(R + 4, 6).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 4, 6), xlSheet.Cells(R + 4, 6)).BorderAround()
            'Sum of Total tax Deducted
            t5 = "=sum(k4:k" & R + 3 & ")"
            xlSheet.Cells(R + 4, 11) = t5
            xlSheet.Cells(R + 4, 11).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 4, 11), xlSheet.Cells(R + 4, 11)).BorderAround()
            'Sum of Total tax Deposited
            't6 = "=sum(m4:m" & R + 3 & ")"
            'xlSheet.Cells(R + 4, 13) = t6
            'xlSheet.Cells(R + 4, 13).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 4, 13), xlSheet.Cells(R + 4, 13)).BorderAround()
            xlSheet.Calculate()
            xlSheet.Cells(R + 7, 1) = "Monthwise Deductee's Detail List Of Form " & quter
            xlSheet.Range(xlSheet.Cells(R + 7, 1), xlSheet.Cells(R + 7, 8)).Merge()
            xlSheet.Range(xlSheet.Cells(R + 7, 1), xlSheet.Cells(R + 7, 8)).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 8, 1), xlSheet.Cells(R + 8, 8)).WrapText = True
            xlSheet.Range(xlSheet.Cells(R + 8, 1), xlSheet.Cells(R + 8, 16)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.Range(xlSheet.Cells(R + 8, 1), xlSheet.Cells(R + 8, 8)).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 7, 1), xlSheet.Cells(R + 8, 8)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 1), xlSheet.Cells(R + 8, 1)).Value = "Pay Month"
            xlSheet.Range(xlSheet.Cells(R + 8, 2), xlSheet.Cells(R + 8, 2)).Value = "Record Count"
            xlSheet.Range(xlSheet.Cells(R + 8, 3), xlSheet.Cells(R + 8, 3)).Value = "Amount of Paid"
            xlSheet.Range(xlSheet.Cells(R + 8, 4), xlSheet.Cells(R + 8, 4)).Value = "TDS Amount"
            xlSheet.Range(xlSheet.Cells(R + 8, 5), xlSheet.Cells(R + 8, 5)).Value = "Surcharge"
            xlSheet.Range(xlSheet.Cells(R + 8, 6), xlSheet.Cells(R + 8, 6)).Value = "Ecess"
            xlSheet.Range(xlSheet.Cells(R + 8, 7), xlSheet.Cells(R + 8, 7)).Value = "Total Tax Deposited"
            xlSheet.Range(xlSheet.Cells(R + 8, 8), xlSheet.Cells(R + 8, 8)).Value = "Total Tax Deducted"
            xlSheet.Range(xlSheet.Cells(R + 8, 1), xlSheet.Cells(R + 8, 1)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 2), xlSheet.Cells(R + 8, 2)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 3), xlSheet.Cells(R + 8, 3)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 4), xlSheet.Cells(R + 8, 4)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 5), xlSheet.Cells(R + 8, 5)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 6), xlSheet.Cells(R + 8, 6)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 7), xlSheet.Cells(R + 8, 7)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 8, 8), xlSheet.Cells(R + 8, 8)).BorderAround()

            R = R + 9
            For i = 0 To griddet.Rows.Count - 1
                c = 1
                For m = 0 To griddet.Columns.Count - 1
                    xlSheet.Cells(R, c) = griddet.Rows(i).Cells(m).Value
                    'xlSheet.Cells(R + 8, c) = griddet.Item(i, m)
                    'xlSheet.Range(xlSheet.Cells(R + 8, 1), xlSheet.Cells(R + 8, c)).HorizontalAlignment = HorizontalAlignment.Left
                    xlSheet.Range(xlSheet.Cells(R, c), xlSheet.Cells(R, c)).BorderAround()
                    c = c + 1
                Next
                R = R + 1
            Next
            R = R - 1
            xlSheet.Cells(R, 1) = "Total"
            xlSheet.Cells(R, 2) = Val(Mid(txtDCount.Text, 25, 12))
            xlSheet.Cells(R, 3) = Val(Mid(txtDSumAmt.Text, 17, 12))
            xlSheet.Cells(R, 4) = t4 'Val(Mid(txtDSumAmt.Text, InStr(18, txtDSumAmt.Text, "=") + 1, 12))
            xlSheet.Cells(R, 5) = t7
            'Sum of Total tax Deducted

            xlSheet.Cells(R, 6) = t2
            ' xlSheet.Cells(R + 3, 11).Font.Bold = True
            'xlSheet.Range(xlSheet.Cells(R + 3, 11), xlSheet.Cells(R + 3, 11)).BorderAround()
            'Sum of Total tax Deposited
            'xlSheet.Cells(R + 3, 13) = "=sum(m4:m" & R + 2 & ")"
            'xlSheet.Cells(R + 3, 13).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 3, 13), xlSheet.Cells(R + 3, 13)).BorderAround()
            xlSheet.Calculate()
            'xlSheet.Cells(R, 6) = Val(Mid(txtDSumAmt.Text, 17, 12))
            xlSheet.Cells(R, 7) = t5
            xlSheet.Cells(R, 8) = t3
            xlSheet.Range(xlSheet.Cells(R, 1), xlSheet.Cells(R, 1)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R, 2), xlSheet.Cells(R, 2)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R, 3), xlSheet.Cells(R, 3)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R, 4), xlSheet.Cells(R, 4)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R, 5), xlSheet.Cells(R, 5)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R, 6), xlSheet.Cells(R, 6)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R, 7), xlSheet.Cells(R, 7)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R, 8), xlSheet.Cells(R, 8)).BorderAround()
            'xlSheet.UsedRange.Cells.Columns.AutoFit()
            R = R + 2
            xlSheet.Cells(R, 1) = "Summary Of Deductee's List Of Form " & quter
            xlSheet.Range(xlSheet.Cells(R, 1), xlSheet.Cells(R, 1)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.Range(xlSheet.Cells(R, 1), xlSheet.Cells(R, 1)).BorderAround()
            xlSheet.Cells(R, 1).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R, 1), xlSheet.Cells(R, 4)).Merge()
            xlSheet.Cells(R + 1, 1) = "Section"
            xlSheet.Range(xlSheet.Cells(R + 1, 1), xlSheet.Cells(R + 1, 1)).BorderAround()
            xlSheet.Cells(R + 1, 1).Font.Bold = True
            xlSheet.Cells(R + 1, 2) = "Total Deductees Records"
            xlSheet.Range(xlSheet.Cells(R + 1, 2), xlSheet.Cells(R + 1, 2)).BorderAround()
            xlSheet.Cells(R + 1, 2).Font.Bold = True
            xlSheet.Cells(R + 1, 3) = "Total Amount Paid"
            xlSheet.Range(xlSheet.Cells(R + 1, 3), xlSheet.Cells(R + 1, 3)).BorderAround()
            xlSheet.Cells(R + 1, 3).Font.Bold = True
            xlSheet.Cells(R + 1, 4) = "Total TDS Amount"
            xlSheet.Range(xlSheet.Cells(R + 1, 4), xlSheet.Cells(R + 1, 4)).BorderAround()
            xlSheet.Cells(R + 1, 4).Font.Bold = True
            'xlSheet.Range(xlSheet.Cells(R, 1), xlSheet.Cells(R, 16)).WrapText = True
            xlSheet.Range(xlSheet.Cells(R + 1, 1), xlSheet.Cells(R + 1, 16)).VerticalAlignment = ContentAlignment.TopCenter
            c = 0
            Do While Not InStr(c + 1, txtDCountSec.Text, vbCrLf) = 0
                N = InStr(c + 1, txtDCountSec.Text, "Sec")
                j = InStr(c + 1, txtDCountSec.Text, "=")
                k = InStr(j + 1, txtDCountSec.Text, "=")
                xlSheet.Cells(R + 2, 1) = Mid(txtDCountSec.Text, N + 3, j - (N + 3))
                xlSheet.Cells(R + 2, 2) = Val(Mid(txtDCountSec.Text, j + 1, 5))
                xlSheet.Cells(R + 2, 3) = Val(Mid(txtDCountSec.Text, k + 1, 12))
                xlSheet.Cells(R + 2, 4) = Val(Mid(txtDCountSec.Text, (InStr(k + 1, txtDCountSec.Text, "=") + 1), 12))
                c = InStr(c + 1, txtDCountSec.Text, vbCrLf)
                xlSheet.Range(xlSheet.Cells(R + 2, 1), xlSheet.Cells(R + 2, 1)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 2, 2), xlSheet.Cells(R + 2, 2)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 2, 3), xlSheet.Cells(R + 2, 3)).BorderAround()
                xlSheet.Range(xlSheet.Cells(R + 2, 4), xlSheet.Cells(R + 2, 4)).BorderAround()
                R = R + 1
            Loop
            xlSheet.Cells(R + 2, 1) = "Total"
            xlSheet.Cells(R + 2, 2) = Val(Mid(txtDCount.Text, 25, 12))
            xlSheet.Cells(R + 2, 3) = Val(Mid(txtDSumAmt.Text, 17, 12))
            xlSheet.Cells(R + 2, 4) = Val(Mid(txtDSumAmt.Text, InStr(18, txtDSumAmt.Text, "=") + 1, 12))
            xlSheet.Range(xlSheet.Cells(R + 2, 1), xlSheet.Cells(R + 2, 1)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 2, 2), xlSheet.Cells(R + 2, 2)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 2, 3), xlSheet.Cells(R + 2, 3)).BorderAround()
            xlSheet.Range(xlSheet.Cells(R + 2, 4), xlSheet.Cells(R + 2, 4)).BorderAround()
            xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 4)).BorderAround()
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 16)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.UsedRange.Cells.Columns.Font.Size = 8
            'xlSheet.UsedRange.Cells.Columns.AutoFit()
            xlSheet.Columns(1).ColumnWidth = 5
            xlSheet.Columns(2).ColumnWidth = 30
            xlSheet.Columns(3).ColumnWidth = 9.5
            xlSheet.Columns(4).ColumnWidth = 12
            xlSheet.Columns(5).ColumnWidth = 8
            xlSheet.Columns(6).ColumnWidth = 3
            xlSheet.Columns(7).ColumnWidth = 5
            xlSheet.Columns(8).ColumnWidth = 6
            xlSheet.Columns(9).ColumnWidth = 7.5
            xlSheet.Columns(10).ColumnWidth = 10
            xlSheet.Columns(12).ColumnWidth = 8
            xlSheet.Columns(13).ColumnWidth = 5
            xlSheet.Columns(14).ColumnWidth = 12
            xlSheet.Columns(15).ColumnWidth = 6
            xlSheet.Columns(16).ColumnWidth = 0
            xlSheet.Rows(3).RowHeight = 55
            xlSheet.Range("A1", "O999").Font.Size = 8
            xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 18)).WrapText = True
            xlSheet.PageSetup.TopMargin = 18
            xlSheet.PageSetup.BottomMargin = 18
            xlSheet.PageSetup.LeftMargin = 18
            xlSheet.PageSetup.RightMargin = 18
            xlSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 16)).HorizontalAlignment = HorizontalAlignment.Center

            xlSheet.Columns(2).WrapText = True
            'xlSheet.Columns(2).WrapText = True
        End With
        xlapp.Application.Visible = True
        xlBook = xlapp.ActiveWorkbook
        xlBook.Activate()
        Exit Sub
excelerr:
        MsgBox("Cannot open Excel", vbCritical)
    End Sub
    Private Sub cntdecdt()
        Dim sql As String
        Dim Qtr As String
        Dim i As Integer, m As Integer, c As Integer
        Dim rs As New DataSet
        Qtr = Strings.Left(quter, 2)
        Select Case Qtr
            Case 24
                sql = "SELECT format(Deductee24Q.DtOfPayment,'mmm') as Pay_Month,Count(Deductee24Q.DId) AS Record_Count, Sum(Deductee24Q.AmtOfPayment) AS AmountOfPaid, Sum(Deductee24Q.TaxAmt) AS TDSAmount, Sum(Deductee24Q.Surcharge) AS Surcharge, Sum(Deductee24Q.ECess) AS ECess, Sum(Deductee24Q.TotalTaxDeposited) AS TotalTaxDeposited, Sum(Deductee24Q.TotalTaxDeducted) AS TotalTaxDeducted" _
           & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
           & " Where RetnMst.FrmType ='" & quter & "'and comst.coid=" & selectedcoid _
           & " GROUP BY format(Deductee24Q.DtOfPayment,'mmm')"

            Case 26
                sql = "SELECT format(Deductee26Q.DtOfPayment,'mmm') as Pay_Month,Count(Deductee26Q.DId) AS Record_Count, Sum(Deductee26Q.AmtOfPayment) AS Amount_Of_Paid, Sum(Deductee26Q.TaxAmt) AS TDS_Amount, Sum(Deductee26Q.Surcharge) AS Surcharge, Sum(Deductee26Q.ECess) AS ECess, Sum(Deductee26Q.TotalTaxDeposited) AS Total_Tax_Deposited, Sum(Deductee26Q.TotalTaxDeducted) AS Total_Tax_Deducted" _
         & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
          & " Where RetnMst.FrmType = '" & quter & "' and comst.coid=" & selectedcoid _
         & " GROUP BY format(Deductee26Q.DtOfPayment,'mmm')"

            Case 27
                sql = "SELECT format(Deductee27EQ.DtOfPayment,'mmm') as PayMonth,Count(Deductee27EQ.DId) AS RecordCount, Sum(Deductee27EQ.AmtOfPayment) AS AmountOfPaid, Sum(Deductee27EQ.TaxAmt) AS TDSAmount, Sum(Deductee27EQ.Surcharge) AS Surcharge, Sum(Deductee27EQ.ECess) AS ECess, Sum(Deductee27EQ.TotalTaxDeposited) AS TotalTaxDeposited, Sum(Deductee27EQ.TotalTaxDeducted) AS TotalTaxDeducted" _
        & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
         & " Where RetnMst.FrmType ='" & quter & "' and comst.coid=" & selectedcoid _
        & " GROUP BY format(Deductee27EQ.DtOfPayment,'mmm')"
        End Select
        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        Dim da As New OleDbDataAdapter
        cmd = New OleDbCommand(sql, cn)
        da.SelectCommand = cmd
        ds = New DataSet()
        da.Fill(ds)

        ' rs.Open sql, Cnn
        'rs = FetchDataSet("sql")
        griddet.DataSource = ds.Tables(0)
        'griddet.Rows = rs.Tables(0).Records.Count + 1
        For i = 0 To griddet.Rows.Count
            ' griddet.Item(0, c) = rs.Fields(i).Name
            c = c + 1
        Next

    End Sub
    Public Sub Export24_2XL_ChallanWiseDeductee()
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim R As Long, c As Long
        xlapp = New Excel.Application
        xlBook = xlapp.Workbooks.Add
        xlSheet = xlBook.Worksheets("Sheet1")
        ' xlapp.Visible = True
        xlSheet = xlBook.Sheets("Sheet1")
        xlSheet = xlBook.ActiveSheet
        xlSheet.Name = "Export sheet"
        On Error GoTo err
        Dim Itm As ListViewItem ', subItm As ListSubItem
        Dim rs As New DataSet
        Dim rw As Long, NRW As Long, srw As Long, rwb As Long
        Dim sql As String
        With xlSheet
            .Cells(1, 1) = "Company Name" & "  :=  " & txtCoName.Text & "(FY  " & FY & ")"
            .Range(.Cells(1, 1), .Cells(1, 15)).Merge()
            .Range(.Cells(1, 1), .Cells(1, 15)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(1, 1), .Cells(1, 15)).Font.Bold = True
            .Cells(2, 1) = "Challanwise Deductee Details " & Me.quter
            .Range(.Cells(2, 1), .Cells(2, 15)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 15)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(2, 1), .Cells(2, 15)).Font.Bold = True
            .Range(.Cells(2, 1), .Cells(2, 15)).Font.Size = 10
            ' .Range(.Cells(2, 1), .Cells(2, 15)).Font.Color = vbBlue

        End With
        'Loop for each challan...
        rw = 3
        For Each Itm In lvwChallan.Items
            rw = rw + 1
            'xlapp.Visible = True
            With xlSheet
                '       rs.Open "select * from Deductee24Q where ChallanID = " & Itm.ListSubItems(16), Cnn
                rs = FetchDataSet(" SELECT Deductee24Q.*, DeductMst.DName, DeductMst.DPan, Challan24Q.IsBookEntry as bkentry,Challan24Q.BankChallanNo" _
                   & " FROM (DeductMst INNER JOIN Deductee24Q ON DeductMst.DId = Deductee24Q.DId) INNER JOIN Challan24Q ON Deductee24Q.ChallanId = Challan24Q.ChallanID" _
                   & " Where Deductee24Q.ChallanID = " & Itm.SubItems(16).Text)
                'rs.Open sql, Cnn
                Dim Col As Integer
                Col = 1
                'Fill Challan Details...
                Dim colHead As ColumnHeader
                NRW = rw
                For Each colHead In lvwChallan.Columns
                    .Cells(rw, Col) = colHead.Text
                    .Cells(rw, Col).Font.Bold = True
                    .Cells(rw, Col).Font.Color = &H80&  'red
                    If rw = NRW + 9 Then
                        Col = Col + 3
                        rw = NRW - 1
                        .Range(.Cells(rw + 1, Col), .Cells(rw + 1, Col + 1)).BorderAround()
                    Else
                        Col = Col
                        .Range(.Cells(rw, Col), .Cells(rw, Col + 1)).Merge()
                        .Range(.Cells(rw, Col), .Cells(rw, Col + 1)).BorderAround()
                    End If
                    rw = rw + 1
                Next

                Col = 3
                rw = NRW
                .Cells(rw, Col) = Itm.Text
                .Cells(rw, Col).BorderAround
                '        Col = Col + 1
                rw = rw
                Dim lvsub As ListViewItem.ListViewSubItem
                For Each lvsub In Itm.SubItems
                    If rw = rwb Then
                        '                    .Cells(rw, Col) = IIf((lvsub.Text = "FALSE"), "Y", "N")
                        .Cells(rw, Col) = lvsub.Text
                    Else
                        .Cells(rw, Col) = lvsub.Text
                    End If
                    '.Cells(rw, Col) = lvsub.Text
                    '  End If
                    .Cells(rw, Col).Font.Underline = True
                    .Range(.Cells(rw, Col), .Cells(rw, Col)).BorderAround()
                    If rw = NRW + 9 Then
                        Col = Col + 3
                        rw = NRW - 1
                        rwb = NRW + 1
                    Else
                        Col = Col
                    End If
                    rw = rw + 1
                    ' Col = Col + 1
                Next
                ' .Cells(rw - 1, Col).Delete = True
                ' .Cells(rw - 1, Col - 2).Delete = True
                '            .Rows(rw - 1).Delete = True
                '            .Rows(rw - 1).Delete = True
                rw = rw
                'Fill Deductees for this challan...
                If rs.Tables(0).Rows.Count > 0 Then
                    rw = rw + 1
                    Col = 1
                    For Each colHead In lvwDeductee.Columns
                        .Cells(rw, Col) = colHead.Text
                        .Cells(rw, Col).Font.Bold = True
                        .Cells(rw, Col).Font.Color = &H8000& 'green
                        .Range(.Cells(rw, Col), .Cells(rw, Col)).BorderAround()
                        Col = Col + 1
                    Next
                    .Range(.Cells(rw, 1), .Cells(rw, Col)).WrapText = True
                    .Range(.Cells(rw, 1), .Cells(rw, Col)).VerticalAlignment = ContentAlignment.TopCenter
                    Col = 1
                    rw = rw + 1
                    srw = rw
                    Dim s As Integer
                    'Do While Not rs.EOF
                    For s = 0 To rs.Tables(0).Rows.Count - 1
                        .Cells(rw, Col) = rs.Tables(0).Rows(s)("Sec")
                        .Cells(rw, Col + 1) = rs.Tables(0).Rows(s)("DName")
                        .Cells(rw, Col + 2) = rs.Tables(0).Rows(s)("DPan")
                        .Cells(rw, Col + 3) = rs.Tables(0).Rows(s)("AmtOfPayment")
                        .Cells(rw, Col + 4) = Format(rs.Tables(0).Rows(s)("DtOfPayment"), "dd/MMM/yyyy")
                        .Cells(rw, Col + 5) = IIf((rs.Tables(0).Rows(s)("bkentry") = False), "N", "Y")
                        .Cells(rw, Col + 7) = rs.Tables(0).Rows(s)("TaxAmt")
                        .Cells(rw, Col + 8) = rs.Tables(0).Rows(s)("Surcharge")
                        .Cells(rw, Col + 9) = rs.Tables(0).Rows(s)("ECess")
                        .Cells(rw, Col + 10) = rs.Tables(0).Rows(s)("TotalTaxDeducted")
                        .Cells(rw, Col + 11) = Format(rs.Tables(0).Rows(s)("DtOfDeduction"), "dd/MMM/yyyy")
                        .Cells(rw, Col + 12) = rs.Tables(0).Rows(s)("TotalTaxDeposited")
                        .Cells(rw, Col + 13) = rs.Tables(0).Rows(s)("ChallanID")
                        .Cells(rw, Col + 14) = rs.Tables(0).Rows(s)("BankChallanNo")
                        .Cells(rw, Col + 15) = IIf(rs.Tables(0).Rows(s)("Remark") = " ", "N", rs.Tables(0).Rows(s)("Remark"))
                        .Cells(rw, Col + 16) = rs.Tables(0).Rows(s)("CertNo")
                        .Range(.Cells(rw, 1), .Cells(rw, 17)).Borders.Color = Color.Black
                        .Range(.Cells(rw, 1), .Cells(rw, 17)).BorderAround()
                        .Range(.Cells(rw, 1), .Cells(rw, 17)).WrapText = True
                        .Range(.Cells(rw, 1), .Cells(rw, 17)).VerticalAlignment = ContentAlignment.TopCenter
                        rw = rw + 1
                    Next
                    .Cells(rw, 4) = "=sum(D" & srw & ":d" & rw - 1 & ")"
                    .Range(.Cells(rw, 4), .Cells(rw, 4)).BorderAround()
                    .Cells(rw, 11) = "=sum(k" & srw & ":k" & rw - 1 & ")"
                    .Range(.Cells(rw, 11), .Cells(rw, 11)).BorderAround()
                    .Cells(rw, 13) = "=sum(m" & srw & ":m" & rw - 1 & ")"
                    .Range(.Cells(rw, 13), .Cells(rw, 13)).BorderAround()
                    .Cells(rw, 8) = "=sum(h" & srw & ":h" & rw - 1 & ")"
                    .Range(.Cells(rw, 8), .Cells(rw, 8)).BorderAround()
                    .Cells(rw, 9) = "=sum(i" & srw & ":i" & rw - 1 & ")"
                    .Range(.Cells(rw, 9), .Cells(rw, 9)).BorderAround()
                    .Cells(rw, 10) = "=sum(j" & srw & ":j" & rw - 1 & ")"
                    .Range(.Cells(rw, 10), .Cells(rw, 10)).BorderAround()

                    rw = rw + 1
                    NRW = rw
                    .Columns(1).ColumnWidth = 3.2
                    .Columns(2).ColumnWidth = 18
                    .Columns(2).WrapText = True
                    .Columns(3).ColumnWidth = 10
                    .Columns(4).ColumnWidth = 9
                    .Columns(5).ColumnWidth = 7.4
                    .Columns(6).ColumnWidth = 7
                    .Columns(7).ColumnWidth = 3
                    .Columns(8).ColumnWidth = 8
                    .Columns(9).ColumnWidth = 8
                    .Columns(10).ColumnWidth = 7.5
                    .Columns(11).ColumnWidth = 7.5
                    .Columns(12).ColumnWidth = 7.5
                    .Columns(13).ColumnWidth = 8
                    .Columns(14).ColumnWidth = 7
                    .Columns(15).ColumnWidth = 8
                    .Range(.Cells(3, 1), .Cells(rw, 15)).Font.Size = 8
                Else
                    rw = rw + 1
                    Col = 1
                    .Cells(rw, Col) = "No deductee for this challan"
                    .Cells(rw, Col).Font.Bold = True
                    .Cells(rw, Col).Font.Color = Color.Red
                End If
            End With
            'xlSheet.UsedRange.Cells.Columns.AutoFit()
            'If rs.State = adStateOpen Then rs.Close
            rs.Dispose()
        Next
        xlSheet.Columns(18).Delete = True
        xlSheet.Columns(14).Delete = True
        xlapp.Visible = True
err:
        If Err.Number = 1004 Then Resume Next
    End Sub



    Private Sub cmdSummary_Click(sender As Object, e As EventArgs) Handles cmdSummary.Click
        On Error GoTo excelerr
        ' Timer3.Enabled = False
        'cmdperprint.Visible = False
        cmdSummary.Visible = False
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim rs As New DataSet
        Dim R As Integer, c As Integer
        Dim i As Integer, j As Integer, k As Integer
        Dim CntLstItem As Long
        xlapp = Nothing
        xlBook = Nothing
        xlSheet = Nothing
        xlapp = New Excel.Application
        xlBook = xlapp.Workbooks.Add
        xlSheet = xlBook.Worksheets("Sheet1")
        xlSheet.Cells(3, 1) = "Summary Of Deductee's List Of Form 24"
        xlSheet.Cells(1, 1) = txtCoName
        xlSheet.Cells(5, 1) = "Section"
        xlSheet.Cells(5, 2) = "Total Deductees Records"
        xlSheet.Cells(5, 3) = "Total Amount Paid"
        xlSheet.Cells(5, 4) = "Total TDS Amount"

        With lvwChallan
            CntLstItem = .Items.Count
            R = 6
            c = 0
            Do While Not InStr(c + 1, txtDCountSec.Text, vbCrLf) = 0
                i = InStr(c + 1, txtDCountSec.Text, "Sec")
                j = InStr(c + 1, txtDCountSec.Text, "=")
                k = InStr(j + 1, txtDCountSec.Text, "=")
                xlSheet.Cells(R, 1) = Mid(txtDCountSec.Text, i + 3, j - (i + 3))
                xlSheet.Cells(R, 2) = Val(Mid(txtDCountSec.Text, j + 1, 5))
                xlSheet.Cells(R, 3) = Val(Mid(txtDCountSec.Text, k + 1, 12))
                xlSheet.Cells(R, 4) = Val(Mid(txtDCountSec.Text, (InStr(k + 1, txtDCountSec.Text, "=") + 1), 12))
                c = c + InStr(R + 1, txtDCountSec.Text, vbCrLf)
                R = R + 1
            Loop
            xlSheet.Cells(R, 1) = "Total"
            xlSheet.Cells(R, 2) = Val(Mid(txtDCount.Text, 25, 12))
            xlSheet.Cells(R, 3) = Val(Mid(txtDSumAmt.Text, 17, 12))
            xlSheet.Cells(R, 4) = Val(Mid(txtDSumAmt.Text, InStr(18, txtDSumAmt.Text, "=") + 1, 12))
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 1)).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 1)).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 4)).Merge()
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 4)).Merge()
            xlSheet.Range(xlSheet.Cells(5, 1), xlSheet.Cells(5, 4)).BorderAround()
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 4)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 4)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.Range(xlSheet.Cells(R + 1, 1), xlSheet.Cells(R + 1, 4)).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(100, 4)).Columns.AutoFit()
        End With
        xlapp.Application.Visible = True
        Exit Sub
excelerr:
        MsgBox("Cannot open Excel", vbCritical)
    End Sub




    Private Sub cboRemark_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboRemark.KeyPress
        Dim KeyAscii As Integer
        KeyAscii = CtrlKeyPress(cboRemark, KeyAscii, MyKeypressEnum.KeyPressAutoFind)
    End Sub



    Private Sub cboDedName_KeyDown(sender As Object, e As KeyEventArgs) Handles cboDedName.KeyDown
        'If cboDedName.Text <> "" Then
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
        'End If
    End Sub

    Private Sub cboDedSection_TabIndexChanged(sender As Object, e As EventArgs) Handles cboDedSection.TabIndexChanged

    End Sub

    Private Sub txtTDSAmt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTDSAmt.KeyDown

    End Sub

    Private Sub frmTDS24Q_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Me.Text = strFrmCaption & " - AY: " & AY & " - For Quarter No: " & Strings.Right(quter, 1)
        Dim dpiX As Single = e.Graphics.DpiX
        Dim dpiY As Single = e.Graphics.DpiY
        Dim pfc As New PrivateFontCollection()
        If dpiX = 96 Then

            ' On Form1 shown, start applying font 
            'Dim CFontPath As String = Application.StartupPath
            'pfc.AddFontFile("C:\JAKINFO\WizinTDS 2018\WizinTDS2018\Resources\Fonts\Roboto.ttf")
            Dim allCtrl As New List(Of Control)
            For Each ctrl As Control In FindALLControlRecursive(allCtrl, Me)
                ' You need to define which control type to change it's font family; not recommendd to just change all controls' fonts, it will create a missy shape
                If TypeOf ctrl Is Label Or TypeOf ctrl Is CheckBox Then 'Or TypeOf ctrl Is Button Or TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Or TypeOf ctrl Is ProgressBar Or TypeOf ctrl Is GroupBox Then
                    ' Dim CurrentCtrlFontSize = ctrl.Font.Size ' get current object's font size before applying new font family
                    ctrl.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
                Else
                    ctrl.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
                End If
            Next
            allCtrl.Clear()
        Else

            ' On Form1 shown, start applying font 
            'Dim CFontPath As String = Application.StartupPath
            'pfc.AddFontFile("C:\JAKINFO\WizinTDS 2018\WizinTDS2018\Resources\Fonts\Roboto.ttf")
            Dim allCtrl As New List(Of Control)
            For Each ctrl As Control In FindALLControlRecursive(allCtrl, Me)
                ' You need to define which control type to change it's font family; not recommendd to just change all controls' fonts, it will create a missy shape
                If TypeOf ctrl Is Label Then 'Or TypeOf ctrl Is TextBox Or TypeOf ctrl Is Button Or TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Or TypeOf ctrl Is ProgressBar Or TypeOf ctrl Is GroupBox Then
                    ' Dim CurrentCtrlFontSize = ctrl.Font.Size ' get current object's font size before applying new font family
                    ctrl.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
                Else
                    ctrl.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Regular)
                End If
            Next
            allCtrl.Clear()
        End If
    End Sub

    Private Sub cboChallanNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboChallanNo.KeyPress
        'e = CtrlKeyPress(cboChallanNo, e, MyKeypressEnum.KeyPressAutoFind)
    End Sub

    Private Sub cmdimport_Click(sender As Object, e As EventArgs) Handles cmdimport.Click
        Dim RsRet As New DataSet
        RsRet = FetchDataSet("SELECT RetnID FROM RetnMst WHERE CoID=" & selectedcoid & " AND FrmType='" & quter & "'")
        ' RsRet.ActiveConnection = Cnn
        ' RsRet.CursorLocation = adUseClient
        ' RsRet.CursorType = adOpenDynamic
        ' RsRet.LockType = adLockOptimistic
        'RsRet.Open()
        'MsgBox (Right(quter, 1))
        Form1.FNAME = "24Q"
        Form1.fchallan = "Deductee"
        Form1.oFrmType = quter
        Form1.rtnid = RsRet.Tables(0).Rows(0)(0)
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim RsRet As New DataSet
        RsRet = FetchDataSet("SELECT RetnID FROM RetnMst WHERE CoID=" & selectedcoid & " AND FrmType='" & quter & "'")
        ' RsRet.ActiveConnection = Cnn
        ' RsRet.CursorLocation = adUseClient
        ' RsRet.CursorType = adOpenDynamic
        ' RsRet.LockType = adLockOptimistic
        'RsRet.Open()
        'MsgBox (Right(quter, 1))
        Form1.FNAME = "24Q"
        Form1.fchallan = "Challan"
        Form1.oFrmType = quter
        Form1.rtnid = RsRet.Tables(0).Rows(0)(0)
        Form1.Show()
    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub dtpChallanDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpChallanDate.GotFocus
        Call CtrlGotFocusDate(dtpChallanDate)
    End Sub

    Private Sub txtAmtDeducted_LostFocus(sender As Object, e As EventArgs) Handles txtAmtDeducted.LostFocus
        CtrlLostFocus(txtAmtDeducted)
    End Sub

    Private Sub txtSurcharge_GotFocus(sender As Object, e As EventArgs) Handles txtSurcharge.GotFocus
        Call CtrlGotFocus(txtSurcharge)
    End Sub

    Private Sub txtSurcharge_LostFocus(sender As Object, e As EventArgs) Handles txtSurcharge.LostFocus
        Call CtrlLostFocus(txtSurcharge)
    End Sub

    Private Sub txtECess_LostFocus(sender As Object, e As EventArgs) Handles txtECess.LostFocus
        Call CtrlLostFocus(txtECess)
    End Sub

    Private Sub txtECess_GotFocus(sender As Object, e As EventArgs) Handles txtECess.GotFocus
        Call CtrlGotFocus(txtECess)
    End Sub

    Private Sub txtIntt_LostFocus(sender As Object, e As EventArgs) Handles txtIntt.LostFocus
        Call CtrlLostFocus(txtIntt)
    End Sub

    Private Sub txtIntt_GotFocus(sender As Object, e As EventArgs) Handles txtIntt.GotFocus
        Call CtrlGotFocus(txtIntt)
    End Sub

    Private Sub txtfees_LostFocus(sender As Object, e As EventArgs) Handles txtFees.LostFocus
        Call CtrlLostFocus(txtFees)
    End Sub

    Private Sub txtfees_GotFocus(sender As Object, e As EventArgs) Handles txtFees.GotFocus
        Call CtrlGotFocus(txtFees)
    End Sub
    Private Sub txtOthers_LostFocus(sender As Object, e As EventArgs) Handles txtOthers.LostFocus
        Call CtrlLostFocus(txtOthers)
    End Sub

    Private Sub txtOthers_GotFocus(sender As Object, e As EventArgs) Handles txtOthers.GotFocus
        Call CtrlGotFocus(txtOthers)
    End Sub
    Private Sub txtchallanno_LostFocus(sender As Object, e As EventArgs) Handles txtChallanNo.LostFocus
        Call CtrlLostFocus(txtChallanNo)
    End Sub

    Private Sub txtChallanNo_GotFocus(sender As Object, e As EventArgs) Handles txtChallanNo.GotFocus
        Call CtrlGotFocus(txtChallanNo)
    End Sub
    Private Sub dtpChallanDate_LostFocus(sender As Object, e As EventArgs) Handles dtpChallanDate.LostFocus
        Call CtrlLostFocus(txtChallanNo)
    End Sub

    Private Sub cboBankBrCode_LostFocus(sender As Object, e As EventArgs) Handles cboBankBrCode.LostFocus
        Call CtrlLostFocus(cboBankBrCode)
    End Sub

    Private Sub cboBankBrCode_GotFocus(sender As Object, e As EventArgs) Handles cboBankBrCode.GotFocus

    End Sub

    Private Sub txtTranVouNo_TextChanged(sender As Object, e As EventArgs) Handles txtTranVouNo.TextChanged

    End Sub

    Private Sub txtTranVouNo_LostFocus(sender As Object, e As EventArgs) Handles txtTranVouNo.LostFocus
        Call CtrlLostFocus(txtTranVouNo)
    End Sub

    Private Sub txtTranVouNo_GotFocus(sender As Object, e As EventArgs) Handles txtTranVouNo.GotFocus
        Call CtrlGotFocus(txtTranVouNo)
    End Sub

    Private Sub txtChallanRemark_TextChanged(sender As Object, e As EventArgs) Handles txtChallanRemark.TextChanged

    End Sub

    Private Sub txtChallanRemark_LostFocus(sender As Object, e As EventArgs) Handles txtChallanRemark.LostFocus
        Call CtrlLostFocus(txtChallanRemark)
        'cmdAdd.Focus()
    End Sub

    Private Sub txtChallanRemark_GotFocus(sender As Object, e As EventArgs) Handles txtChallanRemark.GotFocus
        Call CtrlGotFocus(txtChallanRemark)
    End Sub

    Private Sub txtAIntt_LostFocus(sender As Object, e As EventArgs) Handles txtAIntt.LostFocus
        Call CtrlLostFocus(txtAIntt)
    End Sub

    Private Sub txtAIntt_GotFocus(sender As Object, e As EventArgs) Handles txtAIntt.GotFocus
        Call CtrlGotFocus(txtAIntt)
    End Sub

    Private Sub txtAOthers_GotFocus(sender As Object, e As EventArgs) Handles txtAOthers.GotFocus
        Call CtrlGotFocus(txtAOthers)
    End Sub

    Private Sub txtAOthers_LostFocus(sender As Object, e As EventArgs) Handles txtAOthers.LostFocus
        Call CtrlLostFocus(txtAOthers)

    End Sub

    Private Sub cboDedSection_GotFocus(sender As Object, e As EventArgs) Handles cboDedSection.GotFocus
        'Call fillcboDedChallan1(cboDedSection.Text)
        ' SendKeys.Send("{f4}")
    End Sub

    Private Sub cboDedName_LostFocus(sender As Object, e As EventArgs) Handles cboDedName.LostFocus

        Call CtrlLostFocus(cboDedName)
    End Sub

    Private Sub txtCertNo_LostFocus(sender As Object, e As EventArgs) Handles txtCertNo.LostFocus
        Call CtrlLostFocus(txtCertNo)
    End Sub

    Private Sub txtCertNo_GotFocus(sender As Object, e As EventArgs) Handles txtCertNo.GotFocus
        Call CtrlGotFocus(txtCertNo)
    End Sub

    Private Sub txtAmtPay_GotFocus(sender As Object, e As EventArgs) Handles txtAmtPay.GotFocus
        Call CtrlGotFocus(txtAmtPay)
    End Sub

    Private Sub dtpAmtPayDt_LostFocus(sender As Object, e As EventArgs) Handles dtpAmtPayDt.LostFocus
        Call CtrlLostFocus(dtpAmtPayDt)
        dtpAmtPayDt.SelectionLength = 0

    End Sub

    Private Sub dtpAmtPayDt_Validated(sender As Object, e As EventArgs) Handles dtpAmtPayDt.Validated
        'If Not IsDate(dtpAmtPayDt.Text) Then
        '    MsgBox("Invalid Date of Payment", vbCritical)
        '    sender = True
        'End If
    End Sub

    Private Sub txtTDSRate_LostFocus(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtTDSAmt_LostFocus(sender As Object, e As EventArgs) Handles txtTDSAmt.LostFocus
        Call CtrlLostFocus(txtTDSAmt)
        CalcTotalDeducteeTDS()
    End Sub

    Private Sub txtTDSAmt_GotFocus(sender As Object, e As EventArgs) Handles txtTDSAmt.GotFocus
        Call CtrlGotFocus(txtTDSAmt)
    End Sub

    Private Sub txtDSurchrge_LostFocus(sender As Object, e As EventArgs) Handles txtDSurchrge.LostFocus
        Call CtrlLostFocus(txtDSurchrge)
    End Sub

    Private Sub txtDSurchrge_GotFocus(sender As Object, e As EventArgs) Handles txtDSurchrge.GotFocus
        Call CtrlGotFocus(txtDSurchrge)
    End Sub

    Private Sub txtDECess_GotFocus(sender As Object, e As EventArgs) Handles txtDECess.GotFocus
        Call CtrlGotFocus(txtDECess)
    End Sub

    Private Sub txtDECess_LostFocus(sender As Object, e As EventArgs) Handles txtDECess.LostFocus
        Call CtrlLostFocus(txtDECess)
    End Sub

    Private Sub txtTotalTaxDeducted_LostFocus(sender As Object, e As EventArgs) Handles txtTotalTaxDeducted.LostFocus
        Call CtrlLostFocus(txtTotalTaxDeducted)
    End Sub

    Private Sub txtTotalTaxDeducted_GotFocus(sender As Object, e As EventArgs) Handles txtTotalTaxDeducted.GotFocus
        Call CtrlGotFocus(txtTotalTaxDeducted)
    End Sub

    Private Sub dtpTDSDedDt_LostFocus(sender As Object, e As EventArgs) Handles dtpTDSDedDt.LostFocus
        Call CtrlLostFocus(dtpTDSDedDt)
        dtpTDSDedDt.SelectionLength = 0
    End Sub

    Private Sub dtpTDSDedDt_Validated(sender As Object, e As EventArgs) Handles dtpTDSDedDt.Validated
        If Not IsDate(dtpTDSDedDt.Text) Then
            MsgBox("Invalid Date of Deduction", vbCritical)
            sender = True
        ElseIf CDate(dtpTDSDedDt.Text) < FromDateQ Or CDate(dtpTDSDedDt.Text) > ToDateQ Then
            MsgBox("Date of deduction should be within this quarter.", vbCritical)
            sender = True
        End If

    End Sub

    Private Sub txtTotalTaxDeposited_LostFocus(sender As Object, e As EventArgs) Handles txtTotalTaxDeposited.LostFocus
        Call CtrlLostFocus(txtTotalTaxDeposited)

        If Val(txtTotalTaxDeducted) <> Val(txtTotalTaxDeposited) Then
            txtTotalTaxDeposited.BackColor = Color.Red
        Else
            txtTotalTaxDeposited.BackColor = Color.White
        End If
        'Dim i As Integer
        'For i = 1 To 20
        '    If TDSRates(i).Section = cboDedSection.Text Then
        '        'Dim DEPAN As New DataSet
        '        Dim RSST As New DataSet
        '        RSST = FetchDataSet("Select dtype from deductmst where dname = " & Chr(34) & cboDedName.Text & Chr(34))

        '        If RSST.Tables(0).Rows(0)(0) = "O" Then
        '            If Math.Round(Val(txtTDSRate.Text), 0) < (TDSRates(i).RateNonCompany - 1) Or Math.Round(Val(txtTDSRate.Text), 0) > (TDSRates(i).RateNonCompany + 1) Then
        '                MsgBox("TDS Duducted Rate is Lower / Higher than the Prescribed rate of TDS. Please check again", vbOKOnly)
        '            End If
        '        ElseIf RSST.Tables(0).Rows(0)(0) = "C" Then
        '            If Math.Round(Val(txtTDSRate.Text), 0) < (TDSRates(i).RateNonCompany - 1) Or Math.Round(Val(txtTDSRate.Text), 0) > (TDSRates(i).RateNonCompany + 1) Then
        '                MsgBox("TDS Duducted Rate is Lower / Higher than the Prescribed rate of TDS. Please check again", vbOKOnly)
        '            End If
        '        End If
        '    End If
        'Next i

    End Sub

    Private Sub cboChallanNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboChallanNo.SelectedIndexChanged

    End Sub

    Private Sub cboChallanNo_LostFocus(sender As Object, e As EventArgs) Handles cboChallanNo.LostFocus

        'Dim ctrchallanNo
        'Dim nmonth As Integer
        'If cboChallanNo.Text <> "" Then
        '    ctrchallanNo = Split(Trim(cboChallanNo.Text), "-")
        '    If cboRemark.SelectedIndex <> 2 Then
        '        If ((Format(CDate(Trim(ctrchallanNo(1))), "dd/MM/yyyy")) < CDate(Format(dtpTDSDedDt.Text, "dd/MM/yyyy"))) Then
        '            MsgBox("Date of deduction is greater than date of challan. Please check again", vbOKOnly)
        '        End If
        '        If (Month(dtpTDSDedDt.Text) <> Month(ctrchallanNo(1))) Then
        '            If Month(dtpTDSDedDt.Text) <> 3 Then
        '                If Month(dtpTDSDedDt.Text) <> 12 Then
        '                    nmonth = Month(dtpTDSDedDt.Text) + 1
        '                    If CDate("07/" & Format(nmonth, "##") & "/" & Year(dtpTDSDedDt.Text)) < CDate(ctrchallanNo(1)) Then
        '                        MsgBox("Date of Deposite of TDS is above due dates, resulting in Interest Liability. Please check again", vbOKOnly)
        '                    End If
        '                Else
        '                    nmonth = 1
        '                    If CDate("07/" & Format(nmonth, "##") & "/" & Year(dtpTDSDedDt.Text) + 1) < CDate(ctrchallanNo(1)) Then
        '                        MsgBox("Date of Deposite of TDS is above due dates, resulting in Interest Liability. Please check again", vbOKOnly)
        '                    End If
        '                End If

        '            Else
        '                nmonth = Month(dtpTDSDedDt.Text) + 1
        '                If CDate(ctrchallanNo(1)) > CDate("30/" & nmonth & "/" & Year(dtpAmtPayDt.Text)) Then
        '                    MsgBox("Date of Deposite of TDS is above due dates, resulting in Interest Liability. Please check again", vbOKOnly)
        '                End If
        '            End If
        '        End If
        '    End If
        'End If
        'cmdDedAdd.Focus()
    End Sub

    Private Sub cboChallanNo_GotFocus(sender As Object, e As EventArgs) Handles cboChallanNo.GotFocus

    End Sub

    Private Sub txtChallanNo_Validated(sender As Object, e As EventArgs) Handles txtChallanNo.Validated

    End Sub

    Private Sub cboRemark_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboRemark.SelectedValueChanged

    End Sub

    Private Sub dtpTDSDedDt_BackgroundImageLayoutChanged(sender As Object, e As EventArgs) Handles dtpTDSDedDt.BackgroundImageLayoutChanged

    End Sub

    Private Sub txtFees_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFees.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub cboRemark_Validating(sender As Object, e As CancelEventArgs) Handles cboRemark.Validating
        Select Case cboRemark.Text
            Case "N"
                txtCertNo.Enabled = False
                txtCertNo.Text = ""
            Case "C"
                txtCertNo.Enabled = False
                txtCertNo.Text = ""
                MsgBox("Please Enter Valid PAN OR deduct tax at higher rate.", vbCritical, "Warning")
            Case "A", "B"
                txtCertNo.Enabled = True
        End Select

        If cboRemark.SelectedIndex = 2 Or cboRemark.SelectedIndex = 1 Then
            'Applicable only for these section as per new FVU 4.7 validations..
            'changes done by nitin on 20/06/2015...
            If cboDedSection.Text = "193" Or cboDedSection.Text = "194" Or cboDedSection.Text = "194A" Or
                cboDedSection.Text = "194C" Or cboDedSection.Text = "194D" Or cboDedSection.Text = "194G" Or cboDedSection.Text = "194H" Or
                cboDedSection.Text = "194Ia" Or cboDedSection.Text = "194Ib" Or cboDedSection.Text = "194J" Or cboDedSection.Text = "194L" Then
                txtCertNo.Enabled = False
                txtCertNo.Text = vbNullString
            Else
                txtCertNo.Enabled = True
            End If
        End If


        'If IsValidPAN(txtDedPAN.Text) = 0 Then
        '    If cboRemark.Text = "C" Then
        '        MsgBox("This option is not available when PAN is available." & vbCrLf & "Please check again.", vbCritical, "Warning")
        '    End If
        'Else
        '    If cboRemark.Text <> "C" Then
        '        MsgBox("Not a Valid PAN, pls. select option C and deduct tax at higher rate." & vbCrLf & "Please check again.", vbCritical, "Warning")
        '    End If
        'End If
        'If cboRemark.Text = "T" And cboDedSection.Text <> "194C" Then
        '    MsgBox("Remark 'T' is applicable only for section 194C" & vbCrLf & "Please correct data", vbCritical, "Incorrect Combination")
        '    'cboDedSection.SetFocus
        'End If

        ''If cboRemark.SelectedIndex = 2 Or cboRemark.SelectedIndex = 1 Then
        ''    If cboDedSection.Text = "193" Or cboDedSection.Text = "194" Or cboDedSection.Text = "194A" Or
        ''        cboDedSection.Text = "194C" Or cboDedSection.Text = "194D" Or cboDedSection.Text = "194G" Or cboDedSection.Text = "194H" Or
        ''        cboDedSection.Text = "194Ia" Or cboDedSection.Text = "194Ib" Or cboDedSection.Text = "194J" Or cboDedSection.Text = "194L" Then
        ''        txtCertNo.Enabled = False
        ''        txtCertNo.Text = vbNullString
        ''    Else
        ''        txtCertNo.Enabled = True
        ''    End If
        ''End If
        'If cboRemark.SelectedIndex = 1 Then         'Or cboRemark.ListIndex = 2  - removed, as per new FVU rules (applicable only for A)
        '    'Applicable only for these section as per new FVU 4.7 validations..
        '    'changes done by nitin on 20/06/2015...
        '    If cboDedSection.Text = "193" Or cboDedSection.Text = "194" Or cboDedSection.Text = "194A" Or
        '        cboDedSection.Text = "194C" Or cboDedSection.Text = "194D" Or cboDedSection.Text = "194G" Or cboDedSection.Text = "194H" Or
        '        cboDedSection.Text = "194Ia" Or cboDedSection.Text = "194Ib" Or cboDedSection.Text = "194J" Or cboDedSection.Text = "194L" Then
        '        txtCertNo.Enabled = True
        '        txtCertNo.Text = vbNullString
        '        txtCertNo.Focus()
        '    Else
        '        txtCertNo.Enabled = False
        '        txtCertNo.Text = vbNullString
        '    End If
        'Else
        '    txtCertNo.Enabled = False
        '    txtCertNo.Text = vbNullString
        'End If
    End Sub

    Private Sub ChkAllocate_Click(sender As Object, e As EventArgs) Handles ChkAllocate.Click
        If ChkAllocate.Checked = True Then
            txtAIntt.Enabled = False
            txtAOthers.Enabled = False
            txtAIntt.Text = txtIntt.Text
            txtAOthers.Text = txtOthers.Text
        Else
            txtAIntt.Enabled = True
            txtAOthers.Enabled = True
        End If
    End Sub

    Private Sub chkallsec_Click(sender As Object, e As EventArgs) Handles chkallsec.Click
        Call fillcboDedChallan1(cboDedSection.Text)
    End Sub

    'Private Sub txtTDSRate_Validating(sender As Object, e As CancelEventArgs)
    '    Dim rate As Rates, IsLow As Boolean, nrate As Double
    '    'rate = GetTDSRates(cboDedSection.Text)
    '    Select Case txtDedPAN.Tag
    '        Case "O"
    '            nrate = rate.RateNonCompany
    '            If Val(txtTDSRate.Text) < rate.RateNonCompany Then
    '                IsLow = True
    '            End If
    '        Case "C"
    '            nrate = rate.RateCompany
    '            If Val(txtTDSRate.Text) < rate.RateCompany Then
    '                IsLow = True
    '            End If
    '    End Select
    '    If nrate = 0 Then
    '        Call MsgBox("Wizin TDS does not have the rates for this section.  Kindly verify the rates and " _
    '              & vbCrLf & "applicablity of this section from the Income Tax Act." _
    '              , vbInformation + vbDefaultButton1, "Wizin-TDS")
    '        e.Cancel = False
    '    Else

    '    End If
    'End Sub

    Private Sub chkMinorHead_CheckedChanged(sender As Object, e As EventArgs) Handles chkMinorHead.CheckedChanged

    End Sub

    Private Sub chkMinorHead_GotFocus(sender As Object, e As EventArgs) Handles chkMinorHead.GotFocus

    End Sub

    Private Sub cmdAdd_GotFocus(sender As Object, e As EventArgs) Handles cmdAdd.GotFocus

    End Sub

    Private Sub dtpAmtPayDt_Enter(sender As Object, e As EventArgs) Handles dtpAmtPayDt.Enter

    End Sub

    Private Sub cboDedName_Enter(sender As Object, e As EventArgs) Handles cboDedName.Enter
        Call CtrlGotFocusC(cboDedName)
    End Sub

    Private Sub cboDedName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDedName.SelectedIndexChanged

    End Sub

    Private Sub tabMain_TabIndexChanged(sender As Object, e As EventArgs) Handles tabMain.TabIndexChanged

        'cboDedSection.SelectedIndex = 0
        Timer2.Enabled = True
        Timer2.Interval = 300
        Timer2.Start()
        Timer4.Enabled = True
        Timer4.Interval = 300
        Timer4.Start()
    End Sub

    Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click
        tabMain.SelectedIndex = tabMain.SelectedIndex + 1
        If tabMain.SelectedIndex = 1 Then
            cboChallanSection.Focus()
        End If
        If tabMain.SelectedIndex = 2 Then
            cboDedSection.Focus()
        End If
    End Sub

    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Try
            If tabMain.SelectedIndex > 0 Then
                tabMain.SelectedIndex = tabMain.SelectedIndex - 1
            End If
        Catch
        End Try
    End Sub

    Private Sub frmTDS24Q_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub chkSection0_CheckedChanged(sender As Object, e As EventArgs) Handles chkSection1.CheckedChanged

    End Sub

    Private Sub cboRemark_Click(sender As Object, e As EventArgs) Handles cboRemark.Click

    End Sub

    Private Sub dtpTDSDedDt_GotFocus(sender As Object, e As EventArgs) Handles dtpTDSDedDt.GotFocus
        CtrlGotFocusDate(dtpTDSDedDt)
    End Sub

    Private Sub txtChallanNo_EnabledChanged(sender As Object, e As EventArgs) Handles txtChallanNo.EnabledChanged

    End Sub

    Private Sub txtTDSAmt_EnabledChanged(sender As Object, e As EventArgs) Handles txtTDSAmt.EnabledChanged

    End Sub

    Private Sub dtpAmtPayDt_GotFocus(sender As Object, e As EventArgs) Handles dtpAmtPayDt.GotFocus
        CtrlGotFocusDate(dtpAmtPayDt)
    End Sub

    Private Sub TableLayoutPanel22_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel22.Paint

    End Sub

    Private Sub Label682_Click(sender As Object, e As EventArgs) Handles Label682.Click

    End Sub

    Private Sub Label83_Click_1(sender As Object, e As EventArgs) Handles Label83.Click

    End Sub

    Private Sub Label81_Click_1(sender As Object, e As EventArgs) Handles Label81.Click

    End Sub

    Private Sub Label82_Click_1(sender As Object, e As EventArgs) Handles Label82.Click

    End Sub

    Private Sub Label80_Click_1(sender As Object, e As EventArgs) Handles Label80.Click

    End Sub

    Private Sub txttotdepos_MaskInputRejected_1(sender As Object, e As MaskInputRejectedEventArgs) Handles txttotdepos.MaskInputRejected

    End Sub

    Private Sub cmdSave_Click1(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim rs As New DataSet
        Dim transaction As OleDb.OleDbTransaction
        Dim cmd As New OleDb.OleDbCommand
        '    chkvalidation
        If ChkRent.Checked = 1 And TxtLL1PAN.Text = "" Then
            MsgBox("LandLord1 PAN Can't blank, if you wish to continue Please Untick 'House Rent Exceeds 1 Lakhs'")
            Exit Sub
        End If
        If ChkIncomeFromHouseProperty.Checked = 1 And TxtLender1PAN.Text = "" Then
            MsgBox("Lender1 PAN Can't blank, if you wish to continue Please Untick 'Income From House Property'?")
        End If
        If TxtFundNm.Text = "" And CHkSAFund.Checked = 1 Then
            MsgBox("Name Of Superannuation Fund can't Blank, if you wish to continue Please Untick 'Superannuation Fund'")
            Exit Sub
        End If
        sql = "SELECT * FROM F16XTRADETAILS WHERE DID= (SELECT DID FROM DEDUCTMST WHERE DNAME='" & cbo16Ded.Text & "' AND COID=" & selectedcoid & ")"
        'If rs.State = adStateOpen Then
        rs.Dispose()
        ' End If
        rs = FetchDataSet(sql)
        If Not rs.Tables(0).Rows.Count Then '.EOF Or Not rs.BOF Then
            sql = "DELETE FROM F16XTRADETAILS WHERE F16ID=" & rs.Tables(0).Rows(0)("F16ID")
            cmd.Connection = cn
            transaction = cn.BeginTransaction()
            cmd.Transaction = transaction

            Try
                cmd.CommandText = sql
                cmd.ExecuteNonQuery()
                transaction.Commit()

            Catch ex As Exception
                MessageBox.Show("Message:", ex.Message)
                transaction.Rollback()
            End Try

            cmd.Dispose()
            transaction.Dispose()
            'Cnn.Execute sql
        End If
        sql = "INSERT INTO F16XTRADETAILS (F16ID,DID,RETNID"
        If (ChkRent.Checked = 1) Then
            sql = sql & ",RENTEXCEEDS,LANDLORD1PAN,LANDLORD1NAME,LANDLORD2PAN,LANDLORD2NAME,LANDLORD3PAN,LANDLORD3NAME,LANDLORD4PAN,LANDLORD4NAME"
        End If
        If ChkIncomeFromHouseProperty.Checked = 1 Then
            sql = sql & ",INTTPAIDONHP,LENDER1PAN,LENDER1NAME,LENDER2PAN,LENDER2NAME,LENDER3PAN,LENDER3NAME,LENDER4PAN,LENDER4NAME"
        End If
        If CHkSAFund.Checked = 1 Then
            sql = sql & ",HASSAFUNDPAID,FUNDNAME,DATEFROM,DATETO,AMTREPAID,AVGRATE,TAXDEDAMT"
        Else
            sql = sql & ",AVGRATE"
        End If
        sql = sql & ") VALUES("
        sql = sql & F16ID & ","
        sql = sql & did & ","
        sql = sql & RETN
        If (ChkRent.Checked = 1) Then
            sql = sql & IIf((ChkRent.Checked = 1), ",Yes", ",NO") & ","
            sql = sql & "'" & IIf(TxtLL1PAN.ToString(), vbNullString, TxtLL1PAN) & "',"
            sql = sql & "'" & IIf(TxtLL1Nm.ToString(), vbNullString, TxtLL1Nm) & "',"
            sql = sql & "'" & IIf(TxtLL2PAN.ToString(), vbNullString, TxtLL2PAN) & "',"
            sql = sql & "'" & IIf(TxtLL2Nm.ToString(), vbNullString, TxtLL2Nm) & "',"
            sql = sql & "'" & IIf(TxtLL3PAN.ToString(), vbNullString, TxtLL3PAN) & "',"
            sql = sql & "'" & IIf(TxtLL3Nm.ToString(), vbNullString, TxtLL3Nm) & "',"
            sql = sql & "'" & IIf(TxtLL4PAN.ToString(), vbNullString, TxtLL4PAN) & "',"
            sql = sql & "'" & IIf(TxtLL4Nm.ToString(), vbNullString, TxtLL4Nm) & "'"
        End If
        If ChkIncomeFromHouseProperty.Checked = 1 Then
            sql = sql & IIf((ChkIncomeFromHouseProperty.Checked = 1), ",Yes", ",NO") & ","
            sql = sql & "'" & IIf(TxtLender1PAN.ToString(), vbNullString, TxtLender1PAN) & "',"
            sql = sql & "'" & IIf(TxtLender1Nm.ToString(), vbNullString, TxtLender1Nm) & "',"
            sql = sql & "'" & IIf(TxtLender2PAN.ToString(), vbNullString, TxtLender2PAN) & "',"
            sql = sql & "'" & IIf(TxtLender2Nm.ToString(), vbNullString, TxtLender2Nm) & "',"
            sql = sql & "'" & IIf(TxtLender3PAN.ToString(), vbNullString, TxtLender3PAN) & "',"
            sql = sql & "'" & IIf(TxtLender3Nm.ToString(), vbNullString, TxtLender3Nm) & "',"
            sql = sql & "'" & IIf(TxtLender4PAN.ToString(), vbNullString, TxtLender4PAN) & "',"
            sql = sql & "'" & IIf(TxtLender4Nm.ToString(), vbNullString, TxtLender4Nm) & "'"
        End If
        If CHkSAFund.Checked = 1 Then
            sql = sql & IIf((CHkSAFund.Checked = 1), ",Yes", ",NO") & ","
            sql = sql & "'" & IIf(TxtFundNm.ToString(), vbNullString, TxtFundNm) & "',"
            If txtContriFrm.Text = "__/__/__" Then
                sql = sql & vbNull & ","
            Else
                sql = sql & "#" & IIf(txtContriFrm.Text = "__/__/__", vbNull, Format(CDate(txtContriFrm.Text), "MM/dd/yy")) & "#,"
            End If
            If txtContriTo.Text = "__/__/__" Then
                sql = sql & vbNull & ","
            Else
                sql = sql & "#" & IIf(txtContriTo.Text = "__/__/__", vbNull, Format(CDate(txtContriTo.Text), "MM/dd/yy")) & "#,"
            End If
            sql = sql & IIf(Val(TxtAmtRepaid) = 0, 0, TxtAmtRepaid) & ","
            sql = sql & IIf(Val(TxtRateOfDeduction) = 0, 0, TxtRateOfDeduction) & ","
            sql = sql & IIf(Val(TxtTaxDeducted) = 0, 0, TxtTaxDeducted) & ""
        Else
            sql = sql & ",0"
        End If
        sql = sql & ")"
        cmd.CommandText = sql
        cmd.Connection = cn
        transaction = cn.BeginTransaction()
        cmd.Transaction = transaction

        Try
            cmd.ExecuteNonQuery()
            transaction.Commit()
        Catch ex As Exception

            transaction.Rollback()
            MessageBox.Show(ex.Message)
        End Try
        cmd.Dispose()
        transaction.Dispose()
        ' Cnn.Execute sql
        cmdSave.Enabled = False
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Dim sql As String
        Dim cnl As Boolean
        If MsgBox("Are You Sure You Want To Delete This Record?", vbYesNo) = vbYes Then
            'Cnn.Execute sql
            Dim transaction As OleDb.OleDbTransaction
            Dim cmd As New OleDb.OleDbCommand
            sql = "DELETE FROM F16XtraDetails WHERE F16Id=" & F16ID
            cmd.Connection = cn
            transaction = cn.BeginTransaction()
            cmd.Transaction = transaction
            Try
                cmd.CommandText = sql
                cmd.ExecuteNonQuery()
                transaction.Commit()
            Catch ex As Exception
                MessageBox.Show("Message:", ex.Message)
                transaction.Rollback()
            End Try
            cmd.Dispose()
            transaction.Dispose()
        End If
    End Sub

    Private Sub cmdExit_Click_1(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdform16_Click(sender As Object, e As EventArgs) Handles cmdform16.Click
        If lvwForm16.Items.Count > 0 Then
            Call frm16A.Show()
            Exit Sub
        End If
    End Sub

    Private Sub cmdAddNewForm16_Click(sender As Object, e As EventArgs) Handles cmdAddNewForm16.Click
        FRM16Detail.Show()
        'frm16Details.Show()
        FRM16Detail.xMode = "A"
        FRM16Detail.FillDeducteeCombo(FRM16Detail.xMode)
        FRM16Detail.cmd16delete.Enabled = False
        FRM16Detail.Show()
    End Sub

    Private Sub cmd162XL_Click(sender As Object, e As EventArgs) Handles cmd162XL.Click
        Dim CntLstItem As Long
        Dim i As Integer
        On Error GoTo excelerr
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim rs As New DataSet
        Dim R As Integer

        'Set xlapp = Nothing
        'Set xlBook = Nothing
        'Set xlSheet = Nothing
        'Set xlapp = New Excel.Application
        'Set xlBook = xlapp.Workbooks.Add
        'Set xlSheet = xlBook.Worksheets("Sheet1")

        With lvwForm16
            CntLstItem = .Items.Count

            i = 0
            For i = 1 To .Columns.Count - 1
                xlSheet.Cells(3, i) = .Columns(i)
            Next i

            i = 0
            For i = 1 To .Columns.Count - 1     'for bold
                xlSheet.Cells(3, i).Font.Bold = True
                xlSheet.Range(xlSheet.Cells(3, i), xlSheet.Cells(3, i)).BorderAround()

            Next i
            i = 1
            xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 30)).WrapText = True
            'xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 30)).VerticalAlignment = xlTop
            For R = 1 To CntLstItem
                For i = 1 To .Columns.Count - 2
                    xlSheet.Cells(R + 3, 1) = .Items(R)
                    If i < 5 Then
                        xlSheet.Cells(R + 3, i + 1) = .Items(R).SubItems(i)
                    Else
                        xlSheet.Cells(R + 3, i + 1) = Val(.Items(R).SubItems(i))
                    End If
                    xlSheet.Range(xlSheet.Cells(R + 3, i), xlSheet.Cells(R + 3, i)).BorderAround()
                    If i = .Columns.Count - 2 Then
                        xlSheet.Range(xlSheet.Cells(R + 3, i + 1), xlSheet.Cells(R + 3, i + 1)).BorderAround()
                    End If
                Next
                xlSheet.Range(xlSheet.Cells(R + 3, .Columns.Count - 2), xlSheet.Cells(R + 3, .Columns.Count - 2)).BorderAround()
            Next
            Dim mKeyChar As Long
            mKeyChar = 70
            For i = 6 To .Columns.Count - 1
                'xlSheet.Cells(R + 3, i) = "=sum(" & Chr$(mKeyChar) & 4 & ":" & Chr$(mKeyChar) & R + 2 & ")"
                xlSheet.Range(xlSheet.Cells(R + 3, i), xlSheet.Cells(R + 3, i)).Font.Bold = True
                xlSheet.Range(xlSheet.Cells(R + 3, i), xlSheet.Cells(R + 3, i)).Font.Color = &H80&
                xlSheet.Range(xlSheet.Cells(R + 3, i), xlSheet.Cells(R + 3, i)).BorderAround()
                ' mKeyChar = mKeyChar + 1
            Next i
            For i = 6 To .Columns.Count - 2
                '***xlSheet.Cells(R + 3, i) = "=sum(" & Chr$(mKeyChar) & 4 & ":" & Chr$(mKeyChar) & R + 2 & ")"
                mKeyChar = mKeyChar + 1
            Next i
            xlSheet.Cells(R + 3, 1) = "Total" : xlSheet.Cells(R + 3, 1).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(R + 3, 1), xlSheet.Cells(R + 3, 5)).Merge()
            xlSheet.Range(xlSheet.Cells(R + 3, 1), xlSheet.Cells(R + 3, i - 1)).BorderAround()
            xlSheet.Range("A1", "X999").Font.Size = 8
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 6)).Merge()
            xlSheet.Cells(1, 1) = "Form16 Details For Quarter 4"
            xlSheet.Cells(1, 1).Font.Bold = True : xlSheet.Cells(1, 1).Font.Color = &H8000&

        End With
        xlSheet.Columns(1).ColumnWidth = 20
        xlSheet.Columns(2).ColumnWidth = 10
        xlSheet.Columns(3).ColumnWidth = 5
        xlSheet.Columns(4).ColumnWidth = 9
        xlSheet.Columns(5).ColumnWidth = 9
        xlSheet.Columns(6).ColumnWidth = 9
        xlSheet.Columns(7).ColumnWidth = 5
        xlSheet.Columns(8).ColumnWidth = 5
        xlSheet.Columns(9).ColumnWidth = 5
        xlSheet.Columns(10).ColumnWidth = 9
        xlSheet.Columns(11).ColumnWidth = 9
        xlSheet.Columns(12).ColumnWidth = 9
        xlSheet.Columns(13).ColumnWidth = 7
        xlSheet.Columns(15).ColumnWidth = 7
        xlSheet.Columns(16).ColumnWidth = 7
        xlSheet.Columns(17).ColumnWidth = 7
        xlSheet.Columns(18).ColumnWidth = 3
        xlSheet.Columns(19).ColumnWidth = 5
        xlSheet.Columns(20).ColumnWidth = 7
        xlSheet.Columns(21).ColumnWidth = 3
        xlSheet.Columns(22).ColumnWidth = 7
        xlSheet.Columns(23).ColumnWidth = 7
        xlSheet.Columns(24).ColumnWidth = 7
        xlSheet.Columns(1).WrapText = True
        xlSheet.PageSetup.TopMargin = 18
        xlSheet.PageSetup.BottomMargin = 18
        xlSheet.PageSetup.LeftMargin = 18
        xlSheet.PageSetup.RightMargin = 18
        'xlSheet.PageSetup.Orientation = xlLandscape
        xlapp.Application.Visible = True
        xlSheet.PageSetup.Zoom = 72
        Exit Sub
excelerr:
        MsgBox("Cannot open Excel", vbCritical)
    End Sub

    Private Sub cmdmismachForm16_Click(sender As Object, e As EventArgs) Handles cmdmismachForm16.Click
        cmdmismatch_Click()
    End Sub
    Private Sub cmdmismatch_Click()
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim rs As New DataSet
        Dim R As Long, c As Long
        xlapp = Nothing
        xlBook = Nothing
        xlSheet = Nothing
        xlapp = New Excel.Application
        xlBook = xlapp.Workbooks.Add
        xlSheet = xlBook.Worksheets("Sheet1")
        Dim m As Integer, N As Integer, i As Integer
        xlSheet.Cells(1, 10) = "Company Name" & "  :=  " & txtCoName.Text & "(FY- " & FY & ")"
        xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 10)).Merge()
        xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 10)).HorizontalAlignment = HorizontalAlignment.Center
        xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 10)).Font.Bold = True
        xlSheet.Cells(3, 1) = "Deductee's Name"
        xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 1)).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, 1)).BorderAround()
        xlSheet.Cells(3, 2) = "PAN No."
        xlSheet.Range(xlSheet.Cells(3, 2), xlSheet.Cells(3, 2)).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(3, 2), xlSheet.Cells(3, 2)).BorderAround()
        xlSheet.Cells(3, 3) = "Total Tax Deposited"
        xlSheet.Range(xlSheet.Cells(3, 3), xlSheet.Cells(3, 3)).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(3, 3), xlSheet.Cells(3, 3)).BorderAround()
        xlSheet.Cells(3, 4) = "Total TDS(Yearly)"
        xlSheet.Range(xlSheet.Cells(3, 4), xlSheet.Cells(3, 4)).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(3, 4), xlSheet.Cells(3, 4)).BorderAround()
        xlSheet.Cells(3, 5) = "Difference"
        xlSheet.Range(xlSheet.Cells(3, 5), xlSheet.Cells(3, 5)).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(3, 5), xlSheet.Cells(3, 5)).BorderAround()
        xlSheet.Cells(2, 1) = "Mismatch Deductee Report for SalaryDetail24Q"
        xlSheet.Range(xlSheet.Cells(2, 1), xlSheet.Cells(2, 1)).Font.Bold = True
        xlSheet.Range(xlSheet.Cells(2, 1), xlSheet.Cells(2, 3)).Merge()
        '  xlSheet.Range(xlSheet.Cells(2, 1), xlSheet.Cells(2, 1)).BorderAround , xlThin, xlColorIndexAutomatic, 1
        'xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 5)) = CO
        'Dim rs As New ADODB.Recordset
        Dim sql As String
        If frmCoMst.chkUseForm16.Checked = False Then
            'sql = "SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, SalaryDetail24Q.TDSAmt, Deductee24Q.DId" _
            '    & " FROM Deductee24Q,SalaryDetail24Q,DeductMst,retnmst where retnmst.coid=DeductMst.coid and " _
            '    & " Deductee24Q.retnid=retnmst.retnid and Deductee24Q.DId = SalaryDetail24Q.DID and SalaryDetail24Q.DID = DeductMst.DId" _
            '    & " and DeductMst.coid=" & selectedcoid & " GROUP BY DeductMst.DName, DeductMst.DPan, SalaryDetail24Q.TDSAmt, Deductee24Q.DId"
            sql = " SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, SalaryDetail24Q.TDSAmt, Deductee24Q.DId" _
                & " FROM DeductMst INNER JOIN (Deductee24Q INNER JOIN SalaryDetail24Q ON Deductee24Q.DId = SalaryDetail24Q.DID) ON DeductMst.DId = Deductee24Q.DId" _
                & " Where DeductMst.coid=" & selectedcoid & " GROUP BY DeductMst.DName, DeductMst.DPan, Deductee24Q.DId, SalaryDetail24Q.TDSAmt" _
                & " Union All SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, SalaryDetail24Q.TdsAmt, Deductee24Q.DId" _
                & " FROM DeductMst INNER JOIN (Deductee24Q LEFT JOIN SalaryDetail24Q ON Deductee24Q.DId = SalaryDetail24Q.DID) ON DeductMst.DId = Deductee24Q.DId" _
                & " Where DeductMst.coid = " & selectedcoid & " And SalaryDetail24Q.DID Is Null" _
                & " GROUP BY DeductMst.DName, DeductMst.DPan, Deductee24Q.DId, SalaryDetail24Q.TDSAmt" _
                & " Union All SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, SalaryDetail24Q.TDSAmt, Deductee24Q.DId" _
                & " FROM (DeductMst INNER JOIN SalaryDetail24Q ON DeductMst.DId = SalaryDetail24Q.DID) LEFT JOIN Deductee24Q ON SalaryDetail24Q.DID = Deductee24Q.DId" _
                & " Where DeductMst.coid =" & selectedcoid & " And Deductee24Q.DID Is Null" _
                & " GROUP BY DeductMst.DName, DeductMst.DPan, SalaryDetail24Q.TDSAmt, Deductee24Q.DId"
        Else
            sql = " SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited,Form16Details.TaxAmt as TDSAmt, Deductee24Q.DId " _
                & " FROM DeductMst INNER JOIN (Deductee24Q INNER JOIN Form16Details ON Deductee24Q.DId = Form16Details.DID) ON DeductMst.DId = Deductee24Q.DId " _
                & " Where DeductMst.coid=2 GROUP BY DeductMst.DName, DeductMst.DPan, Deductee24Q.DId, Form16Details.TaxAmt " _
                & " Union All SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, Form16Details.TaxAmt as TDSAmt, Deductee24Q.DId " _
                & " FROM DeductMst INNER JOIN (Deductee24Q LEFT JOIN Form16Details ON Deductee24Q.DId = Form16Details.DID) ON DeductMst.DId = Deductee24Q.DId Where DeductMst.coid = 2 " _
                & " And Form16Details.DID Is Null GROUP BY DeductMst.DName, DeductMst.DPan, Deductee24Q.DId, Form16Details.TaxAmt" _
                & " Union All SELECT DeductMst.DName, DeductMst.DPan, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, Form16Details.TaxAmt as TDSAmt, Deductee24Q.DId" _
                & " FROM (DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DID) LEFT JOIN Deductee24Q ON Form16Details.DID = Deductee24Q.DId Where DeductMst.coid =2" _
                & " And Deductee24Q.DID Is Null GROUP BY DeductMst.DName, DeductMst.DPan, Form16Details.TaxAmt, Deductee24Q.DId"
        End If
        rs = FetchDataSet(sql)
        ' rs.Open(sql, cn)
        m = 4 : N = 1
        If Not rs.Tables(0).Rows.Count Then 'Or rs.BOF Then
            For i = 0 To rs.Tables(0).Rows.Count - 1
                If IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(0)("SumOfTotalTaxDeposited").ToString()), 0, (rs.Tables(0).Rows(0)("SumOfTotalTaxDeposited").ToString())) <> IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(0)("TDSAmt").ToString()), 0, (rs.Tables(0).Rows(0)("TDSAmt").ToString())) Then
                    xlSheet.Cells(m, N) = rs.Tables(0).Rows(i)("DName")
                    'xlSheet.Range(xlSheet.Cells(m, N), xlSheet.Cells(m, N)) = DName
                    xlSheet.Range(xlSheet.Cells(m, N), xlSheet.Cells(m, N)).BorderAround()
                    xlSheet.Cells(m, N + 1) = rs.Tables(0).Rows(i)("DPan")
                    'xlSheet.Range(xlSheet.Cells(m, N + 1), xlSheet.Cells(m, N + 1)) = rs!DPan
                    xlSheet.Range(xlSheet.Cells(m, N + 1), xlSheet.Cells(m, N + 1)).BorderAround()
                    xlSheet.Cells(m, N + 2) = rs.Tables(0).Rows(i)("SumOfTotalTaxDeposited")
                    ' xlSheet.Range(xlSheet.Cells(m, N + 2), xlSheet.Cells(m, N + 2)) = rs!SumOfTotalTaxDeposited
                    xlSheet.Range(xlSheet.Cells(m, N + 2), xlSheet.Cells(m, N + 2)).BorderAround()
                    xlSheet.Cells(m, N + 3) = rs.Tables(0).Rows(i)("TDSAmt")
                    'xlSheet.Range(xlSheet.Cells(m, N + 3), xlSheet.Cells(m, N + 3)) = rs!TDSAmt
                    xlSheet.Range(xlSheet.Cells(m, N + 3), xlSheet.Cells(m, N + 3)).BorderAround()
                    'xlSheet.Cells(m, N) = rs.Tables(0).Rows(0)("DName")
                    xlSheet.Cells(m, N + 4) = xlSheet.Cells(m, N + 2).value - xlSheet.Cells(m, N + 3).value
                    xlSheet.Range(xlSheet.Cells(m, N + 4), xlSheet.Cells(m, N + 4)).BorderAround()
                    m = m + 1

                End If
                ' rs.MoveNext()
            Next
        End If

        xlapp.Application.Visible = True

    End Sub

    Private Sub cmdSDAdd_Click_1(sender As Object, e As EventArgs) Handles cmdSDAdd.Click
        Dim Itm As New ListViewItem
        Dim item14 As String
        oSD = New ClsSalaryDetail24QObj
        'check dates...
        If CDate(txtSDEmpFrm.Text) < FromDate Then
            MsgBox("Employement From Date cannot be less than " & Format(FromDate, "dd/MM/yyyy"), vbExclamation, "Date Error")
            txtSDEmpFrm.Focus()
            Exit Sub
        ElseIf CDate(txtSDEmpTo.Text) > ToDate Then
            MsgBox("Employment To Date cannot be beyond  " & Format(ToDate, "dd/MM/yyyy"), vbExclamation, "Date Error")
            txtSDEmpTo.Focus()
            Exit Sub
        ElseIf CDate(txtSDEmpTo.Text) < CDate(txtSDEmpFrm.Text) Then
            MsgBox("Employment To Date cannot be less than  " & Format(txtSDEmpFrm, "dd/MM/yyyy"), vbExclamation, "Date Error")
            txtSDEmpTo.Focus()
            Exit Sub
        End If
        'check other data
        If cboSDDedName.SelectedIndex = -1 Then
            MsgBox("Please select Deductee first", vbInformation, "NO DEDUCTEE")
            cboSDDedName.Focus()
            Exit Sub
        End If
        If Val(txtSDTotalSal.Text) <= 0 Then
            MsgBox("Total Salary cannot be left blank or cannot be zero", vbInformation, "ZERO AMT")
            txtSDTotalSal.Focus()
            Exit Sub
        End If
        If Val(txtSDTaxableSalary.Text) <= 0 Then
            MsgBox("Wrong figures results in Taxable salary going Negative, Check Again", vbInformation, "WRONG AMTS")
            txtSDTotalSal.Focus()
            Exit Sub
        End If
        If Val(txtSD80CCE.Text) > 150000 Then
            MsgBox("Deductible amount u/s 80CCE cannot exceed Rs. 150000, for this AY")
            txtSD80CCE.Focus()
            Exit Sub
        End If
        If Val(txtSD80CCF.Text) > 20000 Then
            MsgBox("Deductible amount u/s 80CCF cannot exceed Rs. 20000, for this AY")
            txtSD80CCE.Focus()
            Exit Sub
        End If
        ' Added on 11-05-2013
        If Val(txtSD80CCG.Text) > 25000 Then
            MsgBox("Deductible amount u/s 80CCG cannot exceed Rs. 25000, for this AY")
            txtSD80CCG.Focus()
            Exit Sub
        End If
        ' uptill here added on 11-05-2013

        If (Val(txtSD80CCE.Text) + Val(txtSDOtherIVA.Text)) > Val(txtSDGTI.Text) Then
            MsgBox("Total Deductions u/chapter IV-A cannot be greater than Gross Total Income", vbInformation, "WRONG AMTS")
            txtSD80CCE.Focus()
            Exit Sub
        End If
        If cmdSDAdd.Text = "Add" Then
            'Add item..
            If oSD.Insert(oSD) = False Then
                MsgBox("Unable to Insert Salary Details in DataBase" & vbCrLf & "Call JAK Infosolutions", vbCritical, "CANNOT ADD NOW")
            Else
                Dim dt, dt1 As Date
                Itm.Text = cboSDDedName.Text
                Itm.SubItems.Add(txtSDDedPAN.Text)
                Itm.SubItems.Add(txtSDDedCateg.Text)
                dt = txtSDEmpFrm.Text
                Itm.SubItems.Add(dt.ToString("dd/MMM/yyyy"))
                dt1 = txtSDEmpTo.Text
                Itm.SubItems.Add(dt1.ToString("dd/MMM/yyyy"))
                Itm.SubItems.Add(txtSDTotalSal.Text)
                Itm.SubItems.Add(txtSDEntAllow.Text)
                Itm.SubItems.Add(txtSDProfTax.Text)
                Itm.SubItems.Add("")
                Itm.SubItems.Add(txtSDTaxableSalary.Text)
                Itm.SubItems.Add(txtSDOtherIncome.Text)
                Itm.SubItems.Add(txtSDGTI.Text)
                Itm.SubItems.Add(txtSD80CCE.Text)
                Itm.SubItems.Add(txtSD80CCF.Text)
                Itm.SubItems.Add(txtSD80CCG.Text)
                Itm.SubItems.Add(txtSDOtherIVA.Text)
                Itm.SubItems.Add(txtSDTotalTaxableIncome.Text)
                Itm.SubItems.Add(txtSDTax.Text)
                Itm.SubItems.Add(txtSDSurcharge.Text)
                Itm.SubItems.Add(txtSDEduCess.Text)
                Itm.SubItems.Add(txtSDTotalTax.Text)
                Itm.SubItems.Add(txtSDRelief.Text)
                Itm.SubItems.Add(txtSDNetTax.Text)
                Itm.SubItems.Add(txtSDTDSCurEmp.Text)
                Itm.SubItems.Add(txtSDShortfall.Text)
                Itm.SubItems.Add(txtSDTotalSalPreEmp.Text)
                Itm.SubItems.Add(txtSDTDSPreEmp.Text)
                Itm.SubItems.Add(IIf(ChkHigRate.Checked = True, True, False))
                Itm.SubItems.Add(txtSDTDS.Text)
                Itm.SubItems.Add(oSD.SDID)
                lvwSD.Items.Add(Itm)

            End If
            Call ClearSDCtrls()
            Call NormalModeSD()
        Else
            'Edit Item..
            If oSD.Update(oSD) = False Then
                MsgBox("Unable to update Annual Salary Details in database" & vbCrLf & "Call JAK Infosolutions", vbCritical, "CANNOT UPDATE NOW")
            Else
                Dim dt, dt1 As Date
                lvwSD.SelectedItems(0).SubItems(0).Text = cboSDDedName.Text
                lvwSD.SelectedItems(0).SubItems(1).Text = txtSDDedPAN.Text
                lvwSD.SelectedItems(0).SubItems(2).Text = txtSDDedCateg.Text
                dt = txtSDEmpFrm.Text
                lvwSD.SelectedItems(0).SubItems(3).Text = Format(dt, "dd/MMM/yyyy")
                dt1 = txtSDEmpTo.Text
                lvwSD.SelectedItems(0).SubItems(4).Text = Format(dt, "dd/MMM/yyyy")
                lvwSD.SelectedItems(0).SubItems(5).Text = txtSDTotalSal.Text
                lvwSD.SelectedItems(0).SubItems(6).Text = txtSDEntAllow.Text
                lvwSD.SelectedItems(0).SubItems(7).Text = txtSDProfTax.Text
                lvwSD.SelectedItems(0).SubItems(9).Text = txtSDTaxableSalary.Text
                lvwSD.SelectedItems(0).SubItems(10).Text = txtSDOtherIncome.Text
                lvwSD.SelectedItems(0).SubItems(11).Text = txtSDGTI.Text
                lvwSD.SelectedItems(0).SubItems(12).Text = txtSD80CCE.Text
                lvwSD.SelectedItems(0).SubItems(13).Text = txtSD80CCF.Text
                lvwSD.SelectedItems(0).SubItems(14).Text = txtSD80CCG.Text
                lvwSD.SelectedItems(0).SubItems(15).Text = txtSDOtherIVA.Text
                lvwSD.SelectedItems(0).SubItems(16).Text = txtSDTotalTaxableIncome.Text
                lvwSD.SelectedItems(0).SubItems(17).Text = txtSDTax.Text
                lvwSD.SelectedItems(0).SubItems(18).Text = txtSDSurcharge.Text
                lvwSD.SelectedItems(0).SubItems(19).Text = txtSDEduCess.Text
                lvwSD.SelectedItems(0).SubItems(20).Text = txtSDTotalTax.Text
                lvwSD.SelectedItems(0).SubItems(21).Text = txtSDRelief.Text
                lvwSD.SelectedItems(0).SubItems(22).Text = txtSDNetTax.Text
                lvwSD.SelectedItems(0).SubItems(23).Text = txtSDTDSCurEmp.Text
                lvwSD.SelectedItems(0).SubItems(25).Text = txtSDTotalSalPreEmp.Text
                lvwSD.SelectedItems(0).SubItems(26).Text = txtSDTDSPreEmp.Text
                lvwSD.SelectedItems(0).SubItems(27).Text = IIf(ChkHigRate.Checked = True, True, False)
                lvwSD.SelectedItems(0).SubItems(29).Text = oSD.SDID
                lvwSD.SelectedItems(0).SubItems(24).Text = txtSDShortfall.Text
                lvwSD.SelectedItems(0).SubItems(28).Text = txtSDTDS.Text 'Val(txtSDTDSPreEmp) + Val(txtSDTDSCurEmp)
            End If
            Call ClearSDCtrls()
            Call NormalModeSD()
        End If
        cboSDDedName.Focus()
        clearDeducteeCtrls()
    End Sub

    Private Sub cmdSDCancel_Click_1(sender As Object, e As EventArgs) Handles cmdSDCancel.Click
        Call ClearSDCtrls()
        Call NormalModeSD()
        cboSDDedName.Focus()
    End Sub

    Private Sub ClearSDCtrls()
        'txtSDID.Text = ""
        cboSDDedName.Text = ""
        txtSD80CCE.Text = ""
        txtSDDedCateg.Text = ""
        txtSDDedPAN.Text = ""
        txtSDEduCess.Text = ""
        'txtSDEmpFrm.Text = "##/##/##"
        'txtSDEmpTo.Text = ""
        txtSDEntAllow.Text = ""
        txtSDGTI.Text = ""
        txtSDNetTax.Text = ""
        txtSDOtherIncome.Text = ""
        txtSDOtherIVA.Text = ""
        txtSDProfTax.Text = ""
        txtSDRelief.Text = ""
        txtSDShortfall.Text = ""
        txtSDSurcharge.Text = ""
        txtSDTax.Text = ""
        txtSDTaxableSalary.Text = ""
        txtSDTDS.Text = ""
        txtSDTotalSal.Text = ""
        txtSDTotalTax.Text = ""
        txtSDTotalTaxableIncome.Text = ""
        txtSurcharge.Text = ""
        txtSD80CCF.Text = ""
        txtSD80CCG.Text = ""
        txtSDTotalSalPreEmp.Text = ""
        txtSDTDSPreEmp.Text = ""
        ChkHigRate.Checked = False
        txtSDTDSCurEmp.Text = ""
    End Sub
    Private Sub NormalModeSD()
        With Me
            .lvwSD.Enabled = True
            .cmdSDAdd.Text = "Add"
            .cmdSDCancel.Enabled = False
        End With
    End Sub

    Private Sub cmdSD2XL_Click_1(sender As Object, e As EventArgs) Handles cmdSD2XL.Click
        detail()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If Me.tabMain.SelectedIndex = 2 Then
            'If cmdperprint.BackColor = SystemColors.ButtonFace Then
            '    cmdperprint.BackColor = SystemColors.HighlightText
            '    cmdsumm.BackColor = SystemColors.HighlightText
            'Else
            '    cmdperprint.BackColor = SystemColors.ButtonFace
            '    cmdsumm.BackColor = SystemColors.ButtonFace
            'End If

            'Counter1 = Counter1 + 1
            'If Counter1 > 10 Then
            '    cmdperprint.BackColor = SystemColors.ButtonFace
            '    cmdsumm.BackColor = SystemColors.ButtonFace
            '    cmdperprint.Visible = False
            '    cmdsumm.Visible = False
            '    Timer4.Enabled = False
            '    Counter1 = 0
            'End If
        End If
    End Sub

    Private Sub cbo16Ded_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo16Ded.SelectedIndexChanged

        Dim rst, rs As New DataSet
        rs.Dispose()
        rs = FetchDataSet("SELECT * FROM F16XtraDetails WHERE DID=(SELECT DID FROM DEDUCTMST WHERE DNAME='" & cbo16Ded.Text & "' AND COID=" & selectedcoid & ")")
        If rs.Tables(0).Rows.Count Then
            TxtLL1Nm.Text = vbNullString
            TxtLL2Nm.Text = vbNullString
            TxtLL3Nm.Text = vbNullString
            TxtLL4Nm.Text = vbNullString
            TxtLL1PAN.Text = vbNullString
            TxtLL2PAN.Text = vbNullString
            TxtLL3PAN.Text = vbNullString
            TxtLL4PAN.Text = vbNullString
            TxtLender1PAN.Text = vbNullString
            TxtLender2PAN.Text = vbNullString
            TxtLender3PAN.Text = vbNullString
            TxtLender4PAN.Text = vbNullString
            TxtLender1PAN.Text = vbNullString
            TxtLender2PAN.Text = vbNullString
            TxtLender3PAN.Text = vbNullString
            TxtLender4PAN.Text = vbNullString
            TxtFundNm.Text = vbNullString
            txtContriFrm.Mask = vbNullString
            txtContriTo.Mask = vbNullString
            TxtRateOfDeduction.Text = vbNullString
            TxtAmtRepaid.Text = vbNullString
            TxtTaxDeducted.Text = vbNullString
            'If rst.State = adStateOpen Then
            rst.Dispose()
            'End If
            rst = FetchDataSet("SELECT F16ID,RetnId,DID FROM Form16Details WHERE DID=(SELECT DID FROM DEDUCTMST WHERE DNAME='" & cbo16Ded.Text & "' AND COID=" & selectedcoid & ")")
            If Not rst.Tables(0).Rows.Count Then 'Not rst.BOF Then
                F16ID = rst.Tables(0).Rows(0)("F16ID")
                RETN = rst.Tables(0).Rows(0)("RetnID")
                did = rst.Tables(0).Rows(0)("did")
            Else
                'If rst.State = adStateOpen Then
                rst.Dispose()
                ' End If
                rst = FetchDataSet("SELECT F16ID,RetnId,Did FROM Form16Details WHERE DID=(SELECT DID FROM DEDUCTMST WHERE DNAME='" & cbo16Ded.Text & "' AND COID=" & selectedcoid & ")")
                F16ID = rst.Tables(0).Rows(0)("F16ID")
                RETN = rst.Tables(0).Rows(0)("RetnID")
                did = rst.Tables(0).Rows(0)("did")
            End If
        Else
            F16ID = rs.Tables(0).Rows(0)("F16ID")
            RETN = rs.Tables(0).Rows(0)("RetnID")
            did = rs.Tables(0).Rows(0)("did")
            If rs.Tables(0).Rows(0)("rentexceeds").ToString() = True Then

                For i = 0 To 7
                    Label68.Visible = True
                Next

                ChkRent.Checked = 1
                TxtLL1Nm.Text = IIf((rs.Tables(0).Rows(0)("Landlord1Name").ToString()), vbNullString, rs.Tables(0).Rows(0)("Landlord1Name").ToString())
                TxtLL2Nm.Text = IIf((rs.Tables(0).Rows(0)("Landlord2Name").ToString()), vbNullString, rs.Tables(0).Rows(0)("Landlord2Name").ToString())
                TxtLL3Nm.Text = IIf((rs.Tables(0).Rows(0)("Landlord3Name").ToString()), vbNullString, rs.Tables(0).Rows(0)("Landlord3Name").ToString())
                TxtLL4Nm.Text = IIf((rs.Tables(0).Rows(0)("Landlord4Name").ToString()), vbNullString, rs.Tables(0).Rows(0)("Landlord4Name").ToString())
                TxtLL1PAN.Text = IIf((rs.Tables(0).Rows(0)("Landlord1PAN").ToString()), vbNullString, rs.Tables(0).Rows(0)("Landlord1PAN").ToString())
                TxtLL2PAN.Text = IIf((rs.Tables(0).Rows(0)("LANDLORD2PAN").ToString()), vbNullString, rs.Tables(0).Rows(0)("LANDLORD2PAN").ToString())
                TxtLL3PAN.Text = IIf((rs.Tables(0).Rows(0)("Landlord3PAN").ToString()), vbNullString, rs.Tables(0).Rows(0)("LANDLORD3PAN").ToString())
                TxtLL4PAN.Text = IIf((rs.Tables(0).Rows(0)("Landlord4PAN").ToString()), vbNullString, rs.Tables(0).Rows(0)("LANDLORD4PAN").ToString())

                TxtLL1Nm.Visible = True
                TxtLL2Nm.Visible = True
                TxtLL3Nm.Visible = True
                TxtLL4Nm.Visible = True
                TxtLL1PAN.Visible = True
                TxtLL2PAN.Visible = True
                TxtLL3PAN.Visible = True
                TxtLL4PAN.Visible = True

            End If
            If rs.Tables(0).Rows(0)("InttPaidOnHP").ToString() = True Then
                For i = 8 To 15
                    Label68.Visible = True
                Next
                ChkIncomeFromHouseProperty.Checked = 1
                TxtLender1PAN.Text = IIf((rs.Tables(0).Rows(0)("Lender1PAN").ToString()), vbNullString, rs.Tables(0).Rows(0)("Lender1PAN").ToString())
                TxtLender2PAN.Text = IIf(rs.Tables(0).Rows(0)("Lender2PAN").ToString(), vbNullString, rs.Tables(0).Rows(0)("Lender2PAN").ToString())
                TxtLender3PAN.Text = IIf(rs.Tables(0).Rows(0)("Lender3PAN").ToString(), vbNullString, rs.Tables(0).Rows(0)("Lender3PAN").ToString())
                TxtLender4PAN.Text = IIf(rs.Tables(0).Rows(0)("Lender4PAN").ToString(), vbNullString, rs.Tables(0).Rows(0)("Lender4PAN").ToString())
                TxtLender1PAN.Text = IIf(rs.Tables(0).Rows(0)("Lender1Name").ToString(), vbNullString, rs.Tables(0).Rows(0)("Lender1Name").ToString())
                TxtLender2PAN.Text = IIf(rs.Tables(0).Rows(0)("Lender2Name").ToString(), vbNullString, rs.Tables(0).Rows(0)("Lender2Name").ToString())
                TxtLender3PAN.Text = IIf(rs.Tables(0).Rows(0)("Lender3Name").ToString(), vbNullString, rs.Tables(0).Rows(0)("Lender3Name").ToString())
                TxtLender4PAN.Text = IIf(rs.Tables(0).Rows(0)("Lender4Name").ToString(), vbNullString, rs.Tables(0).Rows(0)("Lender4Name").ToString())

                TxtLender1PAN.Visible = True
                TxtLender2PAN.Visible = True
                TxtLender3PAN.Visible = True
                TxtLender4PAN.Visible = True
                TxtLender1PAN.Visible = True
                TxtLender2PAN.Visible = True
                TxtLender3PAN.Visible = True
                TxtLender4PAN.Visible = True

            End If

            If rs.Tables(0).Rows(0)("HasSAFundPaid").ToString = True Then
                For i = 16 To 22
                    Label68.Visible = True
                Next
                CHkSAFund.Checked = 1
                TxtFundNm.Text = IIf(rs.Tables(0).Rows(0)("FundName").ToString(), vbNullString, rs.Tables(0).Rows(0)("FundName").ToString())
                txtContriFrm.Text = IIf(rs.Tables(0).Rows(0)("DateFrom").ToString(), vbNullString, rs.Tables(0).Rows(0)("DateFrom").ToString())
                txtContriTo.Text = IIf(rs.Tables(0).Rows(0)("DateTO").ToString(), vbNullString, rs.Tables(0).Rows(0)("DateTo").ToString())
                TxtRateOfDeduction.Text = IIf(rs.Tables(0).Rows(0)("AvgRate").ToString(), vbNullString, rs.Tables(0).Rows(0)("AvgRate").ToString())
                TxtAmtRepaid.Text = IIf(rs.Tables(0).Rows(0)("AmtRepaid").ToString(), vbNullString, rs.Tables(0).Rows(0)("AmtRepaid").ToString())
                TxtTaxDeducted.Text = IIf(rs.Tables(0).Rows(0)("TaxDedAmt").ToString(), vbNullString, rs.Tables(0).Rows(0)("TaxDedAmt").ToString())


                TxtFundNm.Visible = True
                txtContriFrm.Visible = True
                txtContriTo.Visible = True
                TxtRateOfDeduction.Visible = True
                TxtAmtRepaid.Visible = True
                TxtTaxDeducted.Visible = True

            End If

        End If
    End Sub

    Private Sub oSD_PrepareDataForSave(Cancel As Boolean) Handles oSD.PrepareDataForSave
        With oSD
            If lvwSD.Items.Count > 0 Then
                If lvwDeductee.SelectedIndices.Count > 0 Then
                    .SDID = lvwSD.SelectedItems(0).SubItems(29).Text
                End If
            Else
                    .SDID = 0
            End If
            .RetnID = Me.Tag
            .did = cboSDDedName.SelectedValue
            Dim dt As Date
            dt = txtSDEmpFrm.Text
            .EmpFrDt = dt.ToString("dd/MMM/yyyy")
            Dim dt1 As Date
            dt1 = txtSDEmpTo.Text
            .EmpToDt = dt1.ToString("dd/MMM/yyyy")
            .TotalSalary = Val(txtSDTotalSal.Text)
            .Sec16ii = Val(txtSDEntAllow.Text)
            .Sec16iii = Val(txtSDProfTax.Text)
            .OtherIncome = Val(txtSDOtherIncome.Text)
            .Sec80CCEAmt = Val(txtSD80CCE.Text)
            .Sec80CCFAmt = Val(txtSD80CCF.Text)
            .Sec80CCGAmt = Val(txtSD80CCG.Text)
            .OtherVIA = Val(txtSDOtherIVA.Text)
            .TaxAmt = Val(txtSDTax.Text)
            .Surcharge = Val(txtSDSurcharge.Text)
            .ECess = Val(txtSDEduCess.Text)
            .Relief89 = Val(txtSDRelief.Text)
            .TDSAmt = Val(txtSDTDSCurEmp.Text)
            .TotalSalaryPreEmp = Val(txtSDTotalSalPreEmp.Text)
            .TDSAmtPreEmp = Val(txtSDTDSPreEmp.Text)
            '.HighRatePAN = IIf(ChkHigRate = True, vbTrue, vbFalse)
            .HighRatePAN = IIf(ChkHigRate.Checked = True, True, False)
            '.tdsamtcurEmp = Val(txtSDTDS) + Val(txtSDTDSPreEmp)
        End With
    End Sub

    Private Sub cboSDDedName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSDDedName.SelectedIndexChanged

    End Sub

    Private Sub fillcboSDDedName(Sec As String)
        Dim sql As String
        Dim sql1 As String
        Dim nds As New DataSet
        Dim nds1 As New DataSet         'For getting the totals of deductee challan
        Dim CBalance As Long, i As Integer                   'For getting the balance of unallocated challan amount
        'Filling Challan in Deductee Detail
        sql1 = " And  RetnID=" & Me.Tag & " "

        sql = " SELECT challanid,iif(isnull(BankChallanNo),Null,BankChallanNo),DtOfChallan,TotalTax" _
      & " FROM Challan24Q WHERE (BankChallanNo<>Null or BankChallanNo<>0)" & sql1 _
      & " UNION ALL SELECT challanid,iif(isnull(BankChallanNo),Null,BankChallanNo),DtOfChallan,TotalTax" _
      & " FROM Challan24Q WHERE (Taxamt = 0 and (isnull(BankChallanNo) or BankChallanNo=0))" & sql1 _
      & " UNION ALL SELECT challanid,TranVouNo,DtOfChallan,TotalTax " _
      & " FROM Challan24Q WHERE (TranVouNo<>Null and TranVouNo<>0)" & sql1 _
      & " order by ChallanID"
        nds = FetchDataSet(sql)
        cboChallanNo.Items.Clear()

        For i = 0 To nds.Tables(0).Rows.Count - 1
            nds1 = FetchDataSet("select sum(TotalTaxDeposited) as TChallan from Deductee24Q WHERE ChallanID=" & nds.Tables(0).Rows(i)("ChallanID"))
            CBalance = Format(nds.Tables(0).Rows(i)("TotalTax") - IIf(nds1.Tables(0).Rows(0)("TChallan").ToString() = "", 0, nds1.Tables(0).Rows(0)("TChallan")), "0")
            cboChallanNo.Items.Add(nds.Tables(0).Rows(i)(1) & " - " & Format(nds.Tables(0).Rows(i)("DtOfChallan"), "dd/MM/yy") & "- Rs." & CBalance)
            'cboChallanNo.ValueMember = nds.Tables(0).Rows(i)("ChallanID")
            cboChallanNo.SelectedIndex = i

        Next
        nds.Dispose()
        nds1.Dispose()
    End Sub

    'export Annual Salary deductee list to export
    Private Sub detail()
        Dim CntLstItem As Long, CntColHed As Integer
        Dim tot As Double, totsal As Double, entallow As Double, taxemp As Double, taxsal As Double, othicm As Double
        Dim Gtotsal As Double, CCEamt As Double, CCFamt As Double, chpamt As Double, taxicm As Double, icmtax As Double, sur As Double, eduec As Double
        Dim tottax As Double, reli As Double, ntax As Double
        Timer3.Enabled = False
        On Error GoTo excelerr
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim rs As New DataSet
        Dim R, c As Integer
        xlapp = Nothing
        xlBook = Nothing
        xlSheet = Nothing
        xlapp = New Excel.Application
        xlBook = xlapp.Workbooks.Add
        xlSheet = xlBook.Worksheets("Sheet1")
        With lvwSD
            CntLstItem = .Items.Count
            CntColHed = .Columns.Count - 2
            xlSheet.Cells(4, 1) = .Columns(0).Text
            xlSheet.Cells(4, 2) = .Columns(1).Text
            xlSheet.Cells(4, 3) = .Columns(2).Text
            xlSheet.Cells(4, 4) = .Columns(3).Text
            xlSheet.Cells(4, 5) = .Columns(4).Text
            xlSheet.Cells(4, 6) = .Columns(5).Text
            xlSheet.Cells(4, 7) = .Columns(25).Text
            xlSheet.Cells(4, 8) = .Columns(6).Text
            xlSheet.Cells(4, 9) = .Columns(7).Text
            xlSheet.Cells(4, 10) = .Columns(9).Text
            xlSheet.Cells(4, 11) = .Columns(10).Text
            xlSheet.Cells(4, 12) = .Columns(11).Text
            xlSheet.Cells(4, 13) = .Columns(12).Text
            xlSheet.Cells(4, 14) = .Columns(13).Text
            xlSheet.Cells(4, 15) = .Columns(14).Text
            xlSheet.Cells(4, 16) = .Columns(15).Text
            xlSheet.Cells(4, 17) = .Columns(16).Text
            xlSheet.Cells(4, 18) = .Columns(17).Text
            xlSheet.Cells(4, 19) = .Columns(18).Text
            xlSheet.Cells(4, 20) = .Columns(19).Text
            xlSheet.Cells(4, 21) = .Columns(20).Text
            xlSheet.Cells(4, 22) = .Columns(21).Text
            xlSheet.Cells(4, 23) = .Columns(22).Text
            xlSheet.Cells(4, 24) = .Columns(23).Text
            xlSheet.Cells(4, 25) = .Columns(26).Text
            xlSheet.Cells(4, 26) = .Columns(24).Text
            xlSheet.Cells(4, 27) = .Columns(27).Text
            For R = 0 To CntLstItem - 1
                xlSheet.Cells(R + 5, 1) = lvwSD.Items(R).SubItems(0).Text
                xlSheet.Cells(R + 5, 2) = lvwSD.Items(R).SubItems(1).Text
                xlSheet.Cells(R + 5, 3) = lvwSD.Items(R).SubItems(2).Text
                xlSheet.Cells(R + 5, 4) = (lvwSD.Items(R).SubItems(3).Text)
                xlSheet.Cells(R + 5, 5) = lvwSD.Items(R).SubItems(4).Text
                xlSheet.Cells(R + 5, 6) = Val(lvwSD.Items(R).SubItems(5).Text)
                xlSheet.Cells(R + 5, 7) = Val(lvwSD.Items(R).SubItems(25).Text)
                xlSheet.Cells(R + 5, 8) = Val(lvwSD.Items(R).SubItems(6).Text)
                xlSheet.Cells(R + 5, 9) = Val(lvwSD.Items(R).SubItems(7).Text)
                xlSheet.Cells(R + 5, 10) = Val(lvwSD.Items(R).SubItems(9).Text)
                xlSheet.Cells(R + 5, 11) = Val(lvwSD.Items(R).SubItems(10).Text)
                xlSheet.Cells(R + 5, 12) = lvwSD.Items(R).SubItems(11).Text
                xlSheet.Cells(R + 5, 13) = Val(lvwSD.Items(R).SubItems(12).Text)
                xlSheet.Cells(R + 5, 14) = Val(lvwSD.Items(R).SubItems(13).Text)
                xlSheet.Cells(R + 5, 15) = Val(lvwSD.Items(R).SubItems(14).Text)
                xlSheet.Cells(R + 5, 16) = Val(lvwSD.Items(R).SubItems(15).Text)
                xlSheet.Cells(R + 5, 17) = Val(lvwSD.Items(R).SubItems(16).Text)
                xlSheet.Cells(R + 5, 18) = Val(lvwSD.Items(R).SubItems(17).Text)
                xlSheet.Cells(R + 5, 19) = Val(lvwSD.Items(R).SubItems(18).Text)
                xlSheet.Cells(R + 5, 20) = lvwSD.Items(R).SubItems(19).Text
                xlSheet.Cells(R + 5, 21) = lvwSD.Items(R).SubItems(20).Text
                xlSheet.Cells(R + 5, 22) = lvwSD.Items(R).SubItems(21).Text
                xlSheet.Cells(R + 5, 23) = Val(lvwSD.Items(R).SubItems(22).Text)
                xlSheet.Cells(R + 5, 24) = Val(lvwSD.Items(R).SubItems(23).Text)
                xlSheet.Cells(R + 5, 25) = Val(lvwSD.Items(R).SubItems(26).Text)
                xlSheet.Cells(R + 5, 26) = Val(lvwSD.Items(R).SubItems(21).Text) - (Val(lvwSD.Items(R).SubItems(26).Text) + Val(lvwSD.Items(R).SubItems(22).Text))
                xlSheet.Cells(R + 5, 27) = IIf(lvwSD.Items(R).SubItems(27).Text = "True", "Yes", "No")
                ' xlSheet.Cells(r + 5, 28) = .ListItems(r).ListSubItems(28)
                totsal = totsal + xlSheet.Cells(R + 5, 6).value
                entallow = entallow + xlSheet.Cells(R + 5, 7).value
                taxemp = taxemp + xlSheet.Cells(R + 5, 8).value
                taxsal = taxsal + xlSheet.Cells(R + 5, 9).value
                othicm = othicm + xlSheet.Cells(R + 5, 10).value
                Gtotsal = Gtotsal + xlSheet.Cells(R + 5, 11).value
                CCEamt = CCEamt + xlSheet.Cells(R + 5, 12).value
                CCFamt = CCFamt + xlSheet.Cells(R + 5, 13).value
                '                chpamt = chpamt + xlSheet.Cells(R + 7, 14)
                chpamt = chpamt + IIf(xlSheet.Cells(R + 5, 14).value = vbNullString, 0, xlSheet.Cells(R + 5, 14).value)
                taxicm = taxicm + xlSheet.Cells(R + 5, 15).value
                icmtax = icmtax + xlSheet.Cells(R + 5, 16).value
                sur = sur + xlSheet.Cells(R + 5, 17).value
                eduec = eduec + xlSheet.Cells(R + 5, 18).value
                tot = tot + xlSheet.Cells(R + 5, 19).value
                tottax = tottax + xlSheet.Cells(R + 5, 20).value
                reli = reli + xlSheet.Cells(R + 5, 21).value
                ntax = ntax + xlSheet.Cells(R + 5, 22).value
                For c = 1 To 23
                    xlSheet.Cells(R + 5, c).HorizontalAlignment = HorizontalAlignment.Right
                Next
            Next R
            xlSheet.Cells(R + 5, 6) = totsal
            xlSheet.Cells(R + 5, 7) = entallow
            xlSheet.Cells(R + 5, 8) = taxemp
            xlSheet.Cells(R + 5, 9) = taxsal
            xlSheet.Cells(R + 5, 10) = othicm
            xlSheet.Cells(R + 5, 11) = Gtotsal
            xlSheet.Cells(R + 5, 12) = CCEamt
            xlSheet.Cells(R + 5, 13) = CCFamt
            xlSheet.Cells(R + 5, 14) = chpamt
            xlSheet.Cells(R + 5, 15) = taxicm
            xlSheet.Cells(R + 5, 16) = icmtax
            xlSheet.Cells(R + 5, 17) = sur
            xlSheet.Cells(R + 5, 18) = eduec
            xlSheet.Cells(R + 5, 19) = tot
            xlSheet.Cells(R + 5, 20) = tottax
            xlSheet.Cells(R + 5, 21) = reli
            xlSheet.Cells(R + 5, 22) = ntax
            xlSheet.Cells(R + 5, 23) = "=sum(w6:w" & R + 4 & ")"
            xlSheet.Cells(R + 5, 6).Font.Bold = True : xlSheet.Cells(R + 5, 6).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 7).Font.Bold = True : xlSheet.Cells(R + 5, 7).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 8).Font.Bold = True : xlSheet.Cells(R + 5, 8).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 9).Font.Bold = True : xlSheet.Cells(R + 5, 9).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 10).Font.Bold = True : xlSheet.Cells(R + 5, 10).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 11).Font.Bold = True : xlSheet.Cells(R + 5, 11).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 12).Font.Bold = True : xlSheet.Cells(R + 5, 12).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 13).Font.Bold = True : xlSheet.Cells(R + 5, 13).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 14).Font.Bold = True : xlSheet.Cells(R + 5, 14).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 15).Font.Bold = True : xlSheet.Cells(R + 5, 15).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 16).Font.Bold = True : xlSheet.Cells(R + 5, 16).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 17).Font.Bold = True : xlSheet.Cells(R + 5, 17).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 18).Font.Bold = True : xlSheet.Cells(R + 5, 18).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 19).Font.Bold = True : xlSheet.Cells(R + 5, 19).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 20).Font.Bold = True : xlSheet.Cells(R + 5, 20).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 21).Font.Bold = True : xlSheet.Cells(R + 5, 21).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 22).Font.Bold = True : xlSheet.Cells(R + 5, 22).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Cells(R + 5, 23).Font.Bold = True : xlSheet.Cells(R + 5, 23).HorizontalAlignment = HorizontalAlignment.Right
            xlSheet.Range(xlSheet.Cells(R + 4, 1), xlSheet.Cells(R + 4, .Columns.Count - 2)).BorderAround()

            xlSheet.Cells(3, 1) = "Deductee's Detail List Of Form 24Q4"
            xlSheet.Cells(1, 1) = txtCoName.Text
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, .Columns.Count - 2)).Merge()
            xlSheet.Range(xlSheet.Cells(5, 1), xlSheet.Cells(5, .Columns.Count - 2)).BorderAround()
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, .Columns.Count - 1)).HorizontalAlignment = HorizontalAlignment.Center
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, .Columns.Count - 1)).Font.Bold = True
            '        xlSheet.Range(xlSheet.Cells(7, 1), xlSheet.Cells(7, .ColumnHeaders.Count - 1)).Font.Bold = True
            xlSheet.Range(xlSheet.Cells(3, 1), xlSheet.Cells(3, .Columns.Count - 1)).Font.Bold = True
        End With
        xlSheet.Range("A1", "AE999").Font.Size = 8
        xlSheet.Columns(1).ColumnWidth = 20
        xlSheet.Columns(2).ColumnWidth = 10
        xlSheet.Columns(3).ColumnWidth = 2
        xlSheet.Columns(4).ColumnWidth = 9
        xlSheet.Columns(5).ColumnWidth = 9
        xlSheet.Columns(6).ColumnWidth = 9
        xlSheet.Columns(7).ColumnWidth = 5
        xlSheet.Columns(8).ColumnWidth = 5
        xlSheet.Columns(9).ColumnWidth = 5
        xlSheet.Columns(10).ColumnWidth = 7
        xlSheet.Columns(11).ColumnWidth = 7
        xlSheet.Columns(12).ColumnWidth = 7
        xlSheet.Columns(13).ColumnWidth = 5
        xlSheet.Columns(14).ColumnWidth = 2
        xlSheet.Columns(15).ColumnWidth = 5
        xlSheet.Columns(16).ColumnWidth = 7
        xlSheet.Columns(17).ColumnWidth = 7
        xlSheet.Columns(18).ColumnWidth = 7
        xlSheet.Columns(19).ColumnWidth = 2
        xlSheet.Columns(20).ColumnWidth = 5
        xlSheet.Columns(21).ColumnWidth = 5
        xlSheet.Columns(22).ColumnWidth = 3
        xlSheet.Columns(23).ColumnWidth = 7
        xlSheet.Columns(24).ColumnWidth = 7
        xlSheet.Columns(25).ColumnWidth = 7
        xlSheet.Columns(26).ColumnWidth = 5
        xlSheet.Columns(27).ColumnWidth = 7
        xlSheet.Rows(5).WrapText = True
        xlSheet.PageSetup.TopMargin = 18
        xlSheet.PageSetup.BottomMargin = 18
        xlSheet.PageSetup.LeftMargin = 18
        xlSheet.PageSetup.RightMargin = 18
        xlSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
        xlapp.Application.Visible = True
        'xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(lvwSD.ListItems.Count + 7, lvwSD.ColumnHeaders.Count - 1 + 1)).Columns.AutoFit
        Exit Sub
excelerr:
        MsgBox("Cannot open Excel", vbCritical)
    End Sub

    Private Sub cboSDDedName_GotFocus(sender As Object, e As EventArgs) Handles cboSDDedName.GotFocus
        'SendKeys "{f4}"
        Call CtrlGotFocusC(cboSDDedName)
    End Sub

    Private Sub cboSDDedName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboSDDedName.KeyPress
        e.KeyChar = UCase(e.KeyChar)

        If Asc(e.KeyChar) = 34 Or Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        Call calcSD()
    End Sub
    Private Sub calcSD()
        txtSDTaxableSalary.Text = Val(txtSDTotalSal.Text) + Val(txtSDTotalSalPreEmp.Text) - Val(txtSDEntAllow.Text) - Val(txtSDProfTax.Text)
        txtSDTotalSalBoth.Text = Val(txtSDTotalSal.Text) + Val(txtSDTotalSalPreEmp.Text)
        txtSDGTI.Text = Val(txtSDTaxableSalary.Text) + Val(txtSDOtherIncome.Text)
        txtSDTotalTaxableIncome.Text = Val(txtSDGTI.Text) - Val(txtSD80CCE.Text) - Val(txtSD80CCF.Text) - Val(txtSD80CCG.Text) - Val(txtSDOtherIVA.Text)
        txtSDTotalTax.Text = Val(txtSDTax.Text) + Val(txtSDSurcharge.Text) + Val(txtSDEduCess.Text)
        txtSDNetTax.Text = Val(txtSDTotalTax.Text) - Val(txtSDRelief.Text)
        txtSDTDS.Text = Val(txtSDTDSCurEmp.Text) + Val(txtSDTDSPreEmp.Text)
        txtSDShortfall.Text = Val(txtSDNetTax.Text) - Val(txtSDTDS.Text)
    End Sub

    Private Sub txtSDEmpFrm_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles txtSDEmpFrm.MaskInputRejected

    End Sub

    Private Sub cboSDDedName_LostFocus(sender As Object, e As EventArgs) Handles cboSDDedName.LostFocus
        Call CtrlLostFocus(cboSDDedName)
        If cboSDDedName.Text = vbNullString Then Exit Sub
        'If Val(txtSDID.Text) = 0 And 
        If cboSDDedName.Text <> vbNullString Then
            Dim Itm As ListViewItem
            Itm = lvwSD.FindItemWithText(cboSDDedName.Text)
            If Itm Is Nothing Then
                'do nothing
            Else
                MsgBox("Entry of chosen Deductee is already present")
                cboSDDedName.SelectedIndex = -1
                cboSDDedName.Focus()
                Exit Sub
            End If
        End If
        Dim rs As New DataSet
        Dim i As Integer, DName As String
        DName = UCase(cboSDDedName.Text)
        rs = FetchDataSet("SELECT * FROM DeductMst WHERE CoId = " & selectedcoid & " And DName='" & DName & "'")
        If rs.Tables(0).Rows.Count <= 0 Then
            'not found., open deductee detail form..
            frmDeducteeTDS.Show()
            frmDeducteeTDS.Frm_typ = 24
            'frmDeducteeTDS.Move(Me.Left + cboSDDedName.Left) + 100, (Me.Top + cboSDDedName.Top + cboSDDedName.Height + 650)
            frmDeducteeTDS.txtDName.Text = cboSDDedName.Text
            frmDeducteeTDS.optCo.Visible = False
            frmDeducteeTDS.optCo.TabStop = False
            frmDeducteeTDS.Show()
            FillDeducteeCombo()

            For i = 0 To cboSDDedName.SelectedIndex - 1
                If cboSDDedName.SelectedIndex = DName Then
                    cboSDDedName.SelectedIndex = i
                    Exit Sub
                End If
            Next i
            If i = cboSDDedName.SelectedIndex Then cboSDDedName.SelectedIndex = -1      'Not Found
        Else
            txtSDDedPAN.Text = rs.Tables(0).Rows(0)("DPan") & ""
            txtSDDedPAN.Tag = rs.Tables(0).Rows(0)("DType")
            Dim txt2Show As String
            If Not (rs.Tables(0).Rows(0)("Category").ToString() = "") Then
                Select Case UCase(rs.Tables(0).Rows(0)("Category"))
                    Case "W"
                        txt2Show = "W-Woman"
                    Case "S"
                        txt2Show = "S-Senior Citizen"
                    Case Else
                        txt2Show = "G-Other"
                End Select
            End If
            txtSDDedCateg.Text = txt2Show
        End If

        rs.Dispose()

    End Sub

    Private Sub lvwSD_MouseUp(sender As Object, e As MouseEventArgs) Handles lvwSD.MouseUp
        If e.Button = MouseButtons.Right Then
            PopupmenuS.Show(lvwSD, New Point(e.X, e.Y))

        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        If lvwSD.SelectedIndices.Count = 0 Then Exit Sub
        EditRow("SD")
        EditModeSD()
        cboSDDedName.Focus()
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If lvwSD.SelectedIndices.Count = 0 Then Exit Sub
        oSD = New ClsSalaryDetail24QObj

        If oSD.Delete(lvwSD.SelectedItems(0).SubItems(24).Text) = True Then
            MsgBox("This Record is further used, Cannot Delete", vbInformation, "Caution")
            Exit Sub
        End If
        If MsgBox("Do you want to delete this row?", vbYesNo + vbQuestion + vbDefaultButton2, "DELETE DATA") = vbYes Then
            'delete the row..
            If oSD.Delete(lvwSD.SelectedItems(0).SubItems(24).Text) = True Then
                lvwSD.SelectedItems(0).Remove()
            End If
        End If
    End Sub

    Private Sub cmdmismatch_Click(sender As Object, e As EventArgs) Handles cmdmismatch.Click
        cmdmismatch_Click()
    End Sub

    Private Sub lvwSD_DoubleClick(sender As Object, e As EventArgs) Handles lvwSD.DoubleClick
        Call EditRow("SD")
        Call EditModeSD()
    End Sub

    Private Sub cboDedName_MarginChanged(sender As Object, e As EventArgs) Handles cboDedName.MarginChanged

    End Sub
End Class