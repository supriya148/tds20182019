Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.IO

Public Class frm16A
    Dim xlapp As Excel.Application
    Dim wrkbk As Excel.Workbook
    Dim wrkst As Excel.Worksheet
    'Dim frmd As New frmMulSelDed
    Dim sqld As String
    Dim WithEvents o16A As New Form16Details
    Dim MainPath, SavePath As String
    Dim frmd As New frmMulSelDed
    'Dim fso As New FileSystemobject


    Private Sub cmdgen_Click(sender As Object, e As EventArgs) Handles cmdgen.Click
        On Error Resume Next
        Dim mFlag As Boolean
        Dim ctr As Integer
        mFlag = CheckBeforeGenerate()
        If mFlag = True Then
            MsgBox("Some fields remain blank." & vbCrLf &
            "Please fill all fields.", 0 + 16, "Data Require...")
            txtSignByName.Focus()
            Exit Sub
        End If
        Dim MultiDeducteeList As Long
        If cmbDeductee.Enabled = True Then
            MultiDeducteeList = cmbDeductee.Text
            sqld = "SELECT DeductMst.*, Form16Details.* FROM DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId where DeductMst.CoId=" & selectedcoid
        Else
            MultiDeducteeList = sqld
        End If
        Dim ds As DataSet
        ds = FetchDataSet(sqld)
        For i = 0 To ds.Tables(0).Rows.Count - 1
            lblMsg.Text = "Generating form 16 for:" & cmbDeductee.Text
            'DeducteeName = MultiDeducteeList(i)
            ' F16ID = o16A.F16ID
            ' UpdateForm16Data(RetnId)
            FillForm16()
            MainPath = Application.StartupPath & "\Form16 Files\"
            SavePath = MainPath & o16A.mCoName & "\"
            Dim logInfo As System.IO.DirectoryInfo
            If Not Directory.Exists(MainPath) Then
                Directory.CreateDirectory(MainPath)
            End If
            If Not Directory.Exists(SavePath) Then
                Directory.CreateDirectory(SavePath)
            End If

            If File.Exists(SavePath & cmbDeductee.Text & ".xlsx") Then
                File.Delete(SavePath & cmbDeductee.Text & ".xlsx")
                'DeleteFile.exits(SavePath & cmbDeductee.Text & ".xls")
            End If
            wrkbk.SaveAs(SavePath & cmbDeductee.Text & ".xlsx")
            'clean up....
            wrkbk.Close()
        Next
        lblMsg.Text = "Process Finished..."
        xlapp.Quit()
        xlapp = Nothing
        wrkbk = Nothing
        wrkst = Nothing
        'DeducteeName = ""
        If chkOpenXL.Checked = 1 Then
            'open workbook...only if single deductee selection...
            If ds.Tables(0).Rows.Count - 1 <= 0 Then
                Dim XLHandle As Long
                XLHandle = OpenXLFile(SavePath & cmbDeductee.Text & ".xlsx")
                'AppActivate XLHandle, True
            End If
        End If

        If cmbDeductee.Enabled = False Then
            'open save folder here, when multiple form 16 are generated..
        End If
    End Sub

    Private Function CheckBeforeGenerate() As Boolean
        If Trim(txtSignByName.Text) = vbNullString Then
            CheckBeforeGenerate = True
        ElseIf Trim(txtSignByFatherName.Text) = vbNullString Then
            CheckBeforeGenerate = True
        ElseIf Trim(txtSignByCapacity.Text) = vbNullString Then
            CheckBeforeGenerate = True
        ElseIf Trim(txtPlace.Text) = vbNullString Then
            CheckBeforeGenerate = True
        ElseIf Trim(certidt.Text) = "__/__/__" Then
            CheckBeforeGenerate = True
        ElseIf cmbDeductee.Enabled = False Then
            If sqld = vbNullString Then
                CheckBeforeGenerate = True
            End If
        ElseIf cmbDeductee.SelectedIndex <= -1 Then
            CheckBeforeGenerate = True
        Else
            CheckBeforeGenerate = False
        End If
        Return CheckBeforeGenerate
    End Function

    ' Private Sub FillForm16()
    '     Dim rs, rsTmp, rsQrt As New DataSet
    '     Dim sql2, sql As String
    '     Dim NoOfRowsInserted As Long
    '     Dim i, TmpDID As Long
    '     Dim SqlQurt As String
    '     Dim mDname As String, TmpCellAdd As Long, AddOfGrossTotInc As Long
    '     Dim Addof_TaxDeductedAtSourceUs192_2 As Long
    '     GenFromXl()
    '     xlapp = New Excel.Application
    '     xlBook = xlapp.Workbooks.Add
    '     xlSheet = xlBook.Worksheets("Sheet1")
    '     xlapp.Visible = True
    '     xlSheet = xlBook.Sheets("Sheet1")
    '     xlSheet = xlBook.ActiveSheet
    '     xlSheet.Name = "Export sheet"
    '     rs = FetchDataSet("SELECT Form16Details.*, Form16MoreDetails.*, Form16Details.RetnID, Form16Details.DId" &
    '" FROM Form16Details INNER JOIN Form16MoreDetails ON Form16Details.F16ID = Form16MoreDetails.F16ID" &
    '" WHERE (((Form16Details.RetnID)=" & o16A.RetnID & ") AND ((Form16Details.F16Id)=" & o16A.F16ID & "))")


    '     TmpDID = o16A.did 'rs.Fields("Form16Details.DID")

    '     sql2 = "SELECT * from DeductMst WHERE DId=" & TmpDID & " And dname in ('" & cmbDeductee.Text & "')"

    '     'rstTmp.Open sql2, cnn, adOpenKeyset, adLockReadOnly
    '     With xlSheet
    '         'Basic Page Formatting...
    '         .Range("A1", "F999").Font.Size = 8
    '         .Range(.Cells(1, 1), .Cells(1, 6)).ColumnWidth = 13.75
    '         'Start writing form 16 with data...
    '         .Cells(1, 1) = "FORM NO. 16"
    '         With .Range(.Cells(1, 1), .Cells(1, 6))
    '             .Merge()
    '             .Font.Size = 20
    '             .Font.Bold = True
    '             .Font.Underline = True
    '         End With
    '         'Line 2
    '         .Cells(2, 1) = "[(See rule 31(1)(a)]"
    '         With .Range(.Cells(2, 1), .Cells(2, 6))
    '             .Merge()
    '         End With
    '         'Line 3
    '         .Cells(3, 1) = "PART A"
    '         With .Range(.Cells(3, 1), .Cells(3, 6))
    '             .Merge()
    '             .Font.Size = 10
    '             .Font.Bold = True
    '         End With
    '         'Line 4
    '         .Cells(4, 1) = "Certificate under section 203 of the Income Tax Act, 1961 for Tax Deducted at Source on Salary"
    '         With .Range(.Cells(4, 1), .Cells(4, 6))
    '             .Merge()
    '             .Font.Size = 10
    '             .Font.Bold = True
    '         End With
    '         'Line 5
    '         .Cells(5, 1) = "Name and Addres of the Employer"
    '         With .Range(.Cells(5, 1), .Cells(5, 3))
    '             .Merge()
    '         End With
    '         .Cells(5, 4) = " Name and Designation of the Employee"
    '         With .Range(.Cells(5, 4), .Cells(5, 6))
    '             .Merge()
    '         End With
    '         'Line 6
    '         .Cells(6, 1) = o16A.mCoName
    '         With .Range(.Cells(6, 1), .Cells(6, 3))
    '             .Merge()
    '         End With
    '         .Cells(6, 4) = cmbDeductee.Text 'rstTmp!DName
    '         With .Range(.Cells(6, 4), .Cells(6, 6))
    '             .Merge()
    '         End With
    '         mDname = cmbDeductee.Text 'rstTmp!DName
    '         'Line 7
    '         .Cells(7, 1) = o16A.mCoAdd1 & " " & o16A.mCoAdd2 & " " & o16A.mCoAdd3
    '         With .Range(.Cells(7, 1), .Cells(8, 3))
    '             .Merge()
    '         End With
    '         .Cells(7, 4) = o16A.DDesgn 'rst!DDesgn
    '         With .Range(.Cells(7, 4), .Cells(8, 6))
    '             .Merge()
    '         End With
    '         'Line 8
    '         .Cells(8, 1) = o16A.mCoAdd4 & " " & o16A.mCoAdd5 & "-" & o16A.mCoPin
    '         With .Range(.Cells(8, 1), .Cells(8, 3))
    '             .Merge()
    '         End With
    '         'Line 9
    '         .Cells(9, 1) = " PAN "
    '         With .Range(.Cells(9, 1), .Cells(9, 2))
    '             .Merge()
    '         End With
    '         .Cells(9, 3) = " TAN "
    '         With .Range(.Cells(9, 3), .Cells(9, 4))
    '             .Merge()
    '         End With
    '         .Cells(9, 5) = "PAN/GIR NO"
    '         With .Range(.Cells(9, 5), .Cells(9, 6))
    '             .Merge()
    '         End With
    '         'Line 10
    '         .Cells(10, 1) = o16A.mCoPAN
    '         With .Range(.Cells(10, 1), .Cells(10, 2))
    '             .Merge()
    '         End With
    '         .Cells(10, 3) = o16A.mCoTAN
    '         With .Range(.Cells(10, 3), .Cells(10, 4))
    '             .Merge()
    '         End With
    '         .Cells(10, 5) = o16A.mCoPAN 'rstTmp!DPan
    '         With .Range(.Cells(10, 5), .Cells(10, 6))
    '             .Merge()
    '         End With
    '         'Line 11
    '         .Cells(11, 1) = "CIT (TDS)"
    '         With .Range(.Cells(11, 1), .Cells(11, 3))
    '             .Merge()
    '             .Font.Bold = True
    '         End With
    '         .Cells(11, 4) = "Period"
    '         With .Range(.Cells(11, 4), .Cells(11, 5))
    '             .Font.Bold = True
    '             .Merge()
    '         End With
    '         .Cells(11, 6) = "Assessment Year"
    '         With .Range(.Cells(11, 6), .Cells(13, 6))
    '             .Font.Bold = True
    '             .Merge()
    '             .WrapText = True
    '         End With
    '         'Line 12 & 13
    '         Dim strsql As String
    '         Dim headadaptor As New OleDbDataAdapter
    '         Dim cmd As New OleDbCommand
    '         Dim ds As New DataSet
    '         'If rstTmp.State = adStateOpen Then rstTmp.Close
    '         strsql = "SELECT CoMst.CITTDSAddtess, CoMst.CITTDSCity, CoMst.CITTDSPin From CoMst WHERE CoMst.CoID=" & selectedcoid
    '         'rstTmp.Open strsql, cnn, adOpenKeyset, adLockReadOnly
    '         cmd = New OleDbCommand(strsql, cn)
    '         headadaptor = New OleDbDataAdapter
    '         ds = New DataSet
    '         headadaptor.SelectCommand = cmd
    '         headadaptor.Fill(ds)
    '         'rst.fill(strsql)
    '         .Cells(12, 1) = "Address - " & ds.Tables(0).Rows(0)("CITTDSAddtess").ToString() ' ( '& rstTmp(0)
    '         With .Range(.Cells(12, 1), .Cells(13, 3))
    '             .Merge()
    '         End With
    '         .Cells(12, 4) = "FROM"
    '         With .Range(.Cells(12, 4), .Cells(13, 4))
    '             .Merge()
    '         End With
    '         With .Range(.Cells(12, 5), .Cells(13, 5))
    '             .Merge()
    '         End With
    '         .Cells(12, 5) = "TO"
    '         'Line 14
    '         .Cells(14, 1) = "City - " & ds.Tables(0).Rows(0)("CITTDSCity").ToString 'rstTmp(1)
    '         .Cells(14, 2) = "Pin Code - "
    '         .Cells(14, 3) = ds.Tables(0).Rows(0)("CITTDSPin").ToString 'rstTmp(2)
    '         .Cells(14, 4) = o16A.EmpFromDt 'Format(rst!EmpFromDt, "dd-mmm-yy")
    '         .Cells(14, 5) = o16A.EmpFromDt 'Format(rst!EmpToDt, "dd-mmm-yy")
    '         '.Cells(14, 6) = mAYear

    '         'Create a border around these cells.
    '         .Range(.Cells(5, 1), .Cells(14, 6)).BorderAround()
    '         '.Range(.Cells(5, 1), .Cells(14, 6)).Borders(xlInsideHorizontal).Weight = xlThin
    '         '.Range(.Cells(5, 1), .Cells(14, 6)).Borders(xlInsideVertical).Weight = xlThin
    '         'Line 15
    '         .Cells(15, 1) = "Summary of tax deducted at source"
    '         With .Range(.Cells(15, 1), .Cells(15, 6))
    '             .Merge()
    '             .Font.Bold = True
    '         End With
    '         'Line 16 & 17
    '         With .Range(.Cells(16, 1), .Cells(16, 6))
    '             .WrapText = True
    '         End With
    '         .Cells(16, 1) = "Quarter"
    '         .Cells(16, 2) = "Receipt Numbers of original statements of TDS under sub-section (3) of section 200"
    '         .Cells(16, 4) = "Amount of tax deducted in respect of the employee"
    '         .Cells(16, 6) = "Amount of tax deposited remitted in respect of the employee"
    '         With .Range(.Cells(16, 1), .Cells(17, 1))
    '             .Merge()
    '         End With
    '         With .Range(.Cells(16, 2), .Cells(17, 3))
    '             .Merge()
    '             .WrapText = True
    '             .RowHeight = 25
    '         End With
    '         With .Range(.Cells(16, 4), .Cells(17, 5))
    '             .Merge()
    '         End With
    '         With .Range(.Cells(16, 6), .Cells(17, 6))
    '             .Merge()
    '         End With
    '         'Center all the data above this line...
    '         .Range(.Cells(1, 1), .Cells(17, 6)).HorizontalAlignment = HorizontalAlignment.Center
    '         'Line 18 to 21
    '         .Cells(18, 1) = "1st Quarter"
    '         .Cells(19, 1) = "2nd Quarter"
    '         .Cells(20, 1) = "3rd Quarter"
    '         .Cells(21, 1) = "4th Quarter"
    '         .Range(.Cells(18, 2), .Cells(18, 3)).Merge()
    '         .Range(.Cells(19, 2), .Cells(19, 3)).Merge()
    '         .Range(.Cells(20, 2), .Cells(20, 3)).Merge()
    '         .Range(.Cells(21, 2), .Cells(21, 3)).Merge()
    '         .Range(.Cells(18, 4), .Cells(18, 5)).Merge()
    '         .Range(.Cells(19, 4), .Cells(19, 5)).Merge()
    '         .Range(.Cells(20, 4), .Cells(20, 5)).Merge()
    '         .Range(.Cells(21, 4), .Cells(21, 5)).Merge()
    '         'Qtry return PRN data...
    '         SetTDSRates()
    '         rsQrt = FetchDataSet(" SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype,r.NewReceiptNo FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#30/Jun/" & Year(FromDate) & "# between #" & Format(o16A.EmpFromDt) & "# and #" & Format(o16A.EmpToDt) & "#" _
    '              & " or #" & Format(o16A.EmpToDt) & "# > #30/Jun/" & Year(FromDate) & " #) and r.frmtype='24Q1'" _
    '              & " Union All  SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype,r.NewReceiptNo FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#30/09/" & Year(FromDate) & "# between #" & Format(o16A.EmpFromDt) & "# and #" & Format(o16A.EmpToDt) & "#" _
    '              & " or #" & Format(o16A.EmpToDt) & "# between #01/07/" & Year(FromDate) & " # and  #30/Sep/" & Year(FromDate) & "#) and r.frmtype='24Q2'" _
    '              & " Union All  SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype,r.NewReceiptNo FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#31/12/" & Year(FromDate) & "# between #" & Format(o16A.EmpFromDt) & "# and #" & Format(o16A.EmpToDt) & "#" _
    '              & " or #" & Format(o16A.EmpToDt) & "# between #01/10/" & Year(FromDate) & " # and  #31/Dec/" & Year(FromDate) & "#)and r.frmtype='24Q3'" _
    '              & " Union All SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype,r.NewReceiptNo FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#31/03/" & Year(ToDate) & "# between #" & Format(o16A.EmpFromDt) & "# and #" & Format(o16A.EmpToDt) & "#" _
    '              & " or #" & Format(o16A.EmpToDt) & "# between #01/01/" & Year(ToDate) & " # and  #31/Mar/" & Year(ToDate) & "#) and r.frmtype='24Q4'")
    '         ' rsQrt.Open SqlQurt, cnn, adOpenStatic, adLockOptimistic
    '         'While Not rsQrt.EOF
    '         Dim s1 As Integer
    '         For s1 = 0 To rsQrt.Tables(0).Rows.Count - 1
    '             If Not String.IsNullOrEmpty(rsQrt.Tables(0).Rows(s1)("PRN")) Then
    '                 If Trim(rsQrt.Tables(0).Rows(0)("FrmType")) = 1 Then
    '                     .Cells(18, 2) = rsQrt.Tables(0).Rows(s1)("NewReceiptNo")
    '                 ElseIf Trim(rsQrt.Tables(0).Rows("FrmType")(1)) = 2 Then
    '                     .Cells(19, 2) = rsQrt.Tables(0).Rows(s1)("NewReceiptNo")
    '                 ElseIf Trim(rsQrt.Tables(0).Rows("FrmType")(1)) = 3 Then
    '                     .Cells(20, 2) = rsQrt.Tables(0).Rows(s1)("NewReceiptNo")
    '                 ElseIf Trim(rsQrt.Tables(0).Rows("FrmType")(1)) = 4 Then
    '                     .Cells(21, 2) = rsQrt.Tables(0).Rows(s1)("NewReceiptNo")
    '                 End If
    '             End If
    '             'rsQrt.MoveNext
    '             'Wend
    '         Next
    '         rsQrt.Dispose()

    '         ' Qtrly TDS deducted & deposited
    '         rsQrt = FetchDataSet("Select DeductMst.DName, Sum(Deductee24Q.TotalTaxDeposited) As SumOfTotalTaxDeposited, Sum(Deductee24Q.TotalTaxDeducted) As SumOfTotalTaxDeducted, Form16Details.F16ID, 'Q1' AS FrmType FROM ((DeductMst INNER JOIN Deductee24Q ON DeductMst.DId = Deductee24Q.DId) INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) LEFT JOIN RetnMst ON Deductee24Q.RetnID = RetnMst.RetnID" _
    '             & " Where (((Deductee24Q.dtofDeduction)" & " between #01/Apr/" & Year(FromDate) & " # and  #30/Jun/" & Year(FromDate) & "#))GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) =" & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & " ))" _
    '              & " Union All" _
    '              & " SELECT DeductMst.DName, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted, Form16Details.F16ID, 'Q2' AS FrmType FROM ((DeductMst INNER JOIN Deductee24Q ON DeductMst.DId = Deductee24Q.DId) INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) LEFT JOIN RetnMst ON Deductee24Q.RetnID = RetnMst.RetnID" _
    '              & " WHERE (((Deductee24Q.DtOfDeduction)" & " between #01/Jul/" & Year(FromDate) & " # and  #30/Sept/" & Year(FromDate) & "#))GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) =" & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & " ))" _
    '              & " Union All" _
    '              & " SELECT DeductMst.DName, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted, Form16Details.F16ID, 'Q3' AS FrmType FROM ((DeductMst INNER JOIN Deductee24Q ON DeductMst.DId = Deductee24Q.DId) INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) LEFT JOIN RetnMst ON Deductee24Q.RetnID = RetnMst.RetnID" _
    '              & " WHERE (((Deductee24Q.DtOfDeduction)" & " between #01/Oct/" & Year(FromDate) & " # and  #31/Dec/" & Year(FromDate) & "#))GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) =" & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & " ))" _
    '              & " Union All" _
    '              & " SELECT DeductMst.DName, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited, Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted, Form16Details.F16ID, 'Q4' AS FrmType FROM ((DeductMst INNER JOIN Deductee24Q ON DeductMst.DId = Deductee24Q.DId) INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) LEFT JOIN RetnMst ON Deductee24Q.RetnID = RetnMst.RetnID" _
    '              & " WHERE (((Deductee24Q.DtOfDeduction)" & " between #01/Jan/" & Year(ToDate) & " # and  #31/Mar/" & Year(ToDate) & "#)) GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) =" & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & " ))" _
    '              & " Union All" _
    '              & " SELECT DeductMst.DName, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess]) AS TotalTaxDeposited, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess])AS TotalTaxDeducted, Form16Details.F16ID, 'Q1' AS FrmType FROM RetnMst INNER JOIN ((DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) INNER JOIN F16Challan ON Form16Details.F16ID = F16Challan.F16ID) ON RetnMst.RetnID = Form16Details.RetnID" _
    '              & " WHERE (((F16Challan.DtOfChallan)" & " between #01/Apr/" & Year(FromDate) & " # and  #30/Jun/" & Year(FromDate) & "#)) GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) =" & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & "))" _
    '              & " Union All" _
    '              & " SELECT DeductMst.DName, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess]) AS TotalTaxDeposited, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess]) AS TotalTaxDeducted, Form16Details.F16ID, 'Q2' AS FrmType FROM RetnMst INNER JOIN ((DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) INNER JOIN F16Challan ON Form16Details.F16ID = F16Challan.F16ID) ON RetnMst.RetnID = Form16Details.RetnID" _
    '              & " WHERE (((F16Challan.DtOfChallan)" & " between #01/jul/" & Year(FromDate) & " # and  #30/sept/" & Year(FromDate) & "#)) GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) = " & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & "))" _
    '              & " Union All" _
    '              & " SELECT DeductMst.DName, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess]) AS TotalTaxDeposited, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess]) AS TotalTaxDeducted, Form16Details.F16ID, 'Q3' AS FrmType FROM RetnMst INNER JOIN ((DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) INNER JOIN F16Challan ON Form16Details.F16ID = F16Challan.F16ID) ON RetnMst.RetnID = Form16Details.RetnID" _
    '              & " WHERE (((F16Challan.DtOfChallan)" & " between #01/oct/" & Year(FromDate) & " # and  #31/dec/" & Year(FromDate) & "#)) GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) = " & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & "))" _
    '              & " Union All" _
    '              & " SELECT DeductMst.DName, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess]) AS TotalTaxDeposited, Sum([F16Challan].[taxamt]+[f16challan].[surcharge]+[f16challan].[ecess]) AS TotalTaxDeducted, Form16Details.F16ID, 'Q4' AS FrmType FROM RetnMst INNER JOIN ((DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId) INNER JOIN F16Challan ON Form16Details.F16ID = F16Challan.F16ID) ON RetnMst.RetnID = Form16Details.RetnID" _
    '              & " WHERE (((F16Challan.DtOfChallan)" & " between #01/jan/" & Year(ToDate) & " # and  #31/Mar/" & Year(ToDate) & "#)) GROUP BY DeductMst.DName, Form16Details.F16ID, DeductMst.CoID Having (((Form16Details.F16ID) = " & o16A.F16ID & ") And ((DeductMst.CoID) = " & selectedcoid & "))")
    '         '            rsQrt.Open SqlQurt, cnn, adOpenStatic, adLockOptimistic
    '         'While Not rsQrt.EOF
    '         Dim s2 As Integer
    '         For s2 = 0 To rsQrt.Tables(0).Rows.Count - 1
    '             If Not String.IsNullOrEmpty(rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")) Then
    '                 If rsQrt.Tables(0).Rows(s1)("FrmType")(1) = 1 Then
    '                     .Cells(18, 4) = .Cells(18, 4) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                     .Range(.Cells(18, 4), .Cells(18, 5)).Merge()
    '                     .Cells(18, 6) = .Cells(18, 6) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                 ElseIf rsQrt.Tables(0).Rows(s1)("FrmType")(1) = 2 Then
    '                     .Cells(19, 4) = .Cells(19, 4) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                     .Range(.Cells(19, 4), .Cells(19, 5)).Merge()
    '                     .Cells(19, 6) = .Cells(19, 6) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                 ElseIf rsQrt.Tables(0).Rows(s1)("FrmType")(1) = 3 Then
    '                     .Cells(20, 4) = .Cells(20, 4) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                     .Range(.Cells(20, 4), .Cells(20, 5)).Merge()
    '                     .Cells(20, 6) = .Cells(20, 6) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                 ElseIf rsQrt.Tables(0).Rows(s1)("FrmType")(1) = 4 Then
    '                     .Cells(21, 4) = .Cells(21, 4) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                     .Range(.Cells(21, 4), .Cells(21, 5)).Merge()
    '                     .Cells(21, 6) = .Cells(21, 6) + rsQrt.Tables(0).Rows(s2)("SumOfTotalTaxDeducted")
    '                 End If
    '             End If
    '         Next
    '         'rsQrt.MoveNext
    '         'Wend
    '         ' rsQrt.Close
    '         ' Set rsQrt = Nothing
    '         rsQrt.Dispose()
    '         'Line 22
    '         .Cells(22, 1) = "TOTAL:"
    '         .Cells(22, 4) = "=SUM(D18:D21)"
    '         .Cells(22, 6) = "=SUM(F18:F21)"
    '         .Range(.Cells(22, 1), .Cells(22, 3)).Merge()
    '         .Range(.Cells(22, 4), .Cells(22, 5)).Merge()

    '         'Create a border around these cells.
    '         .Range(.Cells(16, 1), .Cells(22, 6)).BorderAround()
    '         ' .Range(.Cells(16, 1), .Cells(22, 6)).Borders(Excel.XlBordersIndex.xlInsideHorizontal).Weight = 
    '         ' .Range(.Cells(16, 1), .Cells(22, 6)).Borders(xlInsideVertical).Weight = xlThin

    '         'Line 23
    '         .Cells(23, 1) = "PART B"
    '         With .Range(.Cells(23, 1), .Cells(23, 6))
    '             .Merge()
    '             .Font.Size = 10
    '             .Font.Bold = True
    '             .HorizontalAlignment = HorizontalAlignment.Center
    '         End With
    '         'Line 24
    '         .Cells(24, 1) = "DETAILS OF SALARY PAID AND ANY OTHER INCOME AND TAX DEDUCTED"
    '         With .Range(.Cells(24, 1), .Cells(24, 6))
    '             .Merge()
    '             .Font.Size = 10
    '             .Font.Bold = True
    '             .HorizontalAlignment = HorizontalAlignment.Center
    '         End With
    '         'Line 25
    '         .Cells(25, 1) = "1. Gross Salary"
    '         With .Range(.Cells(25, 1), .Cells(25, 3))
    '             .Merge()
    '             .Font.Bold = True
    '         End With
    '         'Line 26
    '         .Cells(26, 1) = "a. Salary as per provisions contained in section 17(1)"
    '         With .Range(.Cells(26, 1), .Cells(26, 3))
    '             .Merge()
    '         End With
    '         .Cells(26, 4) = o16A.Gross1
    '         'Line 27
    '         .Cells(27, 1) = "b. Value of perquisites u/s 17(2)."
    '         With .Range(.Cells(27, 1), .Cells(27, 3))
    '             .Merge()
    '         End With
    '         .Cells(27, 4) = o16A.Gross2
    '         'Line 28
    '         .Cells(28, 1) = "(as per Form No. 12BB, whereever applicable."
    '         With .Range(.Cells(28, 1), .Cells(28, 3))
    '             .Merge()
    '         End With
    '         'Line 29
    '         .Cells(29, 1) = "c. Profits in lieu of salary u/s 17(3)"
    '         With .Range(.Cells(29, 1), .Cells(29, 3))
    '             .Merge()
    '         End With
    '         .Cells(29, 4) = Format(o16A.Gross3, "#0.00")
    '         'Line 30
    '         .Cells(30, 1) = "(as per Form No. 12BB, whereever applicable."
    '         With .Range(.Cells(30, 1), .Cells(30, 3))
    '             .Merge()
    '         End With
    '         'Line 31
    '         .Cells(31, 1) = "d. Total: including Previous Employer Salary"
    '         With .Range(.Cells(31, 1), .Cells(31, 3))
    '             .Merge()
    '             .Font.Bold = True
    '         End With
    '         .Cells(31, 4) = o16A.TotalSalaryPreEmp
    '         Dim rspr As New DataSet
    '         Dim sqlpr As String
    '         rspr = FetchDataSet("SELECT Form16Details.TotalSalaryPreEmp, Form16Details.TDSAmtPreEmp From Form16Details WHERE Form16Details.F16ID=" & o16A.F16ID)

    '         .Cells(31, 5) = "=sum(d26:d29)+" & Val(o16A.TotalSalaryPreEmp)
    '         'End With
    '         ''Fill Allowances and other Incomes
    '         '' Call FillAllowanceOtherIncome
    '         Dim mRow As Integer
    '         mRow = 32

    '         With xlSheet
    '             .Cells(mRow, 1) = "2. Less: Allowances to the extent exempt u/s 10"
    '             With .Range(.Cells(mRow, 1), .Cells(mRow, 3))
    '                 .Merge()
    '                 .Font.Bold = True
    '             End With
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "Allowances"
    '             With .Range(.Cells(mRow, 1), .Cells(mRow, 2))
    '                 .Merge()
    '                 .Font.Bold = True
    '             End With
    '             .Cells(mRow, 3) = "Rs."
    '             With .Range(.Cells(mRow, 3), .Cells(mRow, 3))
    '                 .Font.Bold = True
    '             End With
    '             mRow = mRow + 1
    '             'allowances details...
    '             Dim StartRow As Integer, BalanceRow As Integer, BalanceRow1 As Integer

    '             StartRow = mRow
    '             'rst.Filter = "TypeOfDetail='A'"
    '             'rst.MoveFirst
    '             'Do While Not rst.EOF
    '             '    .Cells(mRow, 1) = rst!Particulars
    '             '    .Cells(mRow, 3) = rst!GrossAmt
    '             '    mRow = mRow + 1
    '             '    rst.MoveNext
    '             'Loop
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "TOTAL:"
    '             .Cells(mRow, 3) = "=SUM(C" & StartRow & ":c" & mRow - 1 & ")"
    '             .Cells(mRow, 5) = "=C" & mRow
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "3. Balance (1-2)"
    '             .Cells(mRow, 5) = "=E31-E" & mRow - 1
    '             BalanceRow = mRow
    '             'rst.Filter = ""
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "4. Deductions " : mRow = mRow + 1
    '             .Cells(mRow, 1) = "(a) Entertainment Allowance"
    '             .Cells(mRow, 3) = o16A.Sec16ii : mRow = mRow + 1
    '             .Cells(mRow, 1) = "(b) Tax on Employment"
    '             .Cells(mRow, 3) = o16A.Sec16iii : mRow = mRow + 1
    '             .Cells(mRow, 1) = "5. Aggregate of 4(a) and 4(b)"
    '             .Cells(mRow, 5) = "=SUM(C" & mRow - 2 & ":C" & mRow - 1 & ")" : mRow = mRow + 1
    '             .Cells(mRow, 1) = "6. Income Chargable under the Head Salaries (3-5)"
    '             .Cells(mRow, 6) = "=E" & BalanceRow & "-E" & mRow - 1
    '             BalanceRow = mRow
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "Income"
    '             With .Range(.Cells(mRow, 1), .Cells(mRow, 2))
    '                 .Merge()
    '                 .Font.Bold = True
    '             End With
    '             .Cells(mRow, 3) = "Rs."
    '             With .Range(.Cells(mRow, 3), .Cells(mRow, 3))
    '                 .Font.Bold = True
    '             End With
    '             mRow = mRow + 1
    '             'Other Income...
    '             StartRow = mRow
    '             'rst.Filter = "TypeOfDetail='O'"
    '             'rst.MoveFirst
    '             'Do While Not rst.EOF
    '             '    .Cells(mRow, 1) = rst!Particulars
    '             '    .Cells(mRow, 3) = rst!GrossAmt
    '             '    mRow = mRow + 1
    '             '    rst.MoveNext
    '             'Loop
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "TOTAL:"
    '             .Cells(mRow, 3) = "=SUM(C" & StartRow & ":c" & mRow - 1 & ")"
    '             .Cells(mRow, 6) = "=C" & mRow
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "8. Gross Total Income (6+7)"
    '             .Cells(mRow, 6) = "=F" & BalanceRow & "+F" & mRow - 1
    '             BalanceRow = mRow
    '             'rst.Filter = "" : mRow = mRow + 1
    '             .Cells(mRow, 1) = "9. Deductions under Chapter VI A" : mRow = mRow + 1
    '             .Cells(mRow, 1) = "A. Sections 80C, 80CCC & 80CCD" : mRow = mRow + 1

    '             .Cells(mRow, 4) = "Gross Amount"
    '             With .Range(.Cells(mRow, 4), .Cells(mRow, 5))
    '                 .Font.Bold = True
    '             End With
    '             .Cells(mRow, 5) = "Deductible Amount"
    '             mRow = mRow + 1
    '             '80C...
    '             StartRow = mRow
    '             'rst.Filter = "TypeOfDetail='E'"
    '             'rst.MoveFirst
    '             'Do While Not rst.EOF
    '             '    .Cells(mRow, 1) = rst!Particulars
    '             '    .Cells(mRow, 4) = rst!GrossAmt
    '             '    .Cells(mRow, 5) = rst!DeductibleAmt
    '             '    mRow = mRow + 1
    '             '    rst.MoveNext
    '             'Loop
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "TOTAL:"
    '             .Cells(mRow, 4) = "=SUM(D" & StartRow & ":D" & mRow - 1 & ")"
    '             .Cells(mRow, 5) = "=SUM(E" & StartRow & ":E" & mRow - 1 & ")"
    '             .Cells(mRow, 6) = "=E" & mRow
    '             BalanceRow1 = mRow
    '             mRow = mRow + 2
    '             .Cells(mRow, 1) = "Note :1. Aggregate amount deductible under section 80C, 80CCC & 80CCD(1) shall not exceed one lakh rupees " : mRow = mRow + 1
    '             'commented on dt 11/05/14 for ver 5.0
    '             '             .Cells(mRow, 1) = "shall not exceed one lakh rupees other sections (e.g., 80E, 80G etc.)": mRow = mRow + 1
    '             '             .Cells(mRow, 1) = "2. aggregate amount deductible under the three sections , i.e., 80C, 80CCC and 80CCD, ": mRow = mRow + 1

    '             'Chapter VI A Other...............
    '             .Cells(mRow, 1) = "Other sections (e.g., 80E, 80G, 80TTA, etc.) under chapter VI A" : mRow = mRow + 1
    '             .Cells(mRow, 3) = "Gross Amount"
    '             With .Range(.Cells(mRow, 3), .Cells(mRow, 5))
    '                 .Font.Bold = True
    '             End With
    '             .Cells(mRow, 4) = "Qualifying Amt"
    '             .Cells(mRow, 5) = "Deductible Amount"
    '             mRow = mRow + 1
    '             StartRow = mRow
    '             ' rst.Filter = "TypeOfDetail='V' OR TypeOfDetail='F' or TypeOfDetail='G'"
    '             'rst.MoveFirst
    '             'Do While Not rst.EOF
    '             '    .Cells(mRow, 1) = rst!Particulars
    '             '    .Cells(mRow, 3) = rst!GrossAmt
    '             '    .Cells(mRow, 4) = rst!QualifyAmt
    '             '    .Cells(mRow, 5) = rst!DeductibleAmt
    '             '    mRow = mRow + 1
    '             '    rst.MoveNext
    '             'Loop
    '             'rst.Filter = ""
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "TOTAL:"
    '             .Cells(mRow, 3) = "=SUM(C" & StartRow & ":C" & mRow - 1 & ")"
    '             .Cells(mRow, 4) = "=SUM(D" & StartRow & ":D" & mRow - 1 & ")"
    '             .Cells(mRow, 5) = "=SUM(E" & StartRow & ":E" & mRow - 1 & ")"
    '             .Cells(mRow, 6) = "=E" & mRow
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "10. Aggregate of deductible amounts under Chapter VI-A (a+b+c)"
    '             .Cells(mRow, 6) = "=F" & BalanceRow1 & "+F" & mRow - 1 : mRow = mRow + 1
    '             .Cells(mRow, 1) = "11. Total income (8—10)"
    '             .Cells(mRow, 6) = "=F" & BalanceRow & "-F" & mRow - 1 : mRow = mRow + 1
    '             .Cells(mRow, 1) = "12. Tax on total income Rs."
    '             .Cells(mRow, 6) = o16A.TaxAmt : mRow = mRow + 1
    '             .Cells(mRow, 1) = "13. Surcharge (on tax computed at S. No. 12) Rs."
    '             .Cells(mRow, 6) = o16A.Surcharge : mRow = mRow + 1
    '             .Cells(mRow, 1) = "14. Education Cess @3% "
    '             .Cells(mRow, 6) = o16A.ECess : mRow = mRow + 1
    '             .Cells(mRow, 1) = "15. Tax payable (12+13+14) Rs."
    '             .Cells(mRow, 6) = "=SUM(F" & mRow - 3 & ":F" & mRow - 1 & ")" : mRow = mRow + 1
    '             .Cells(mRow, 1) = "16. Relief u/s 89 (attach details)"
    '             .Cells(mRow, 6) = o16A.Relief89 : mRow = mRow + 1
    '             .Cells(mRow, 1) = "17. Tax Payable"
    '             .Cells(mRow, 6) = "=F" & mRow - 2 & " - F" & mRow - 1 : mRow = mRow + 1
    '             .Cells(mRow, 1) = "18. Less : (a) Tax deducted at source u/s 192(1)incluing Tds by Previous Employer "
    '             .Cells(mRow, 5) = "=F22-E" & mRow + 2 & "+" & Val(o16A.TDSAmtPreEmp) : mRow = mRow + 1
    '             .Cells(mRow, 1) = "(b) Tax paid by the employer on behalf of the employee " ': mRow = mRow + 1
    '             .Cells(mRow, 5) = o16A.TDSOnPerks : mRow = mRow + 1
    '             .Cells(mRow, 1) = "u/s 192(1A) on perquisites u/s 17(2)"
    '             Dim sqlperk As String
    '             Dim rsperk As New DataSet
    '             .Cells(mRow, 6) = "=sum(E" & mRow - 2 & ":E" & mRow : mRow = mRow + 1
    '             .Cells(mRow, 1) = "Tax Payble /Refundable (17-18)"
    '             .Cells(mRow, 6) = "=f" & mRow - 4 & "-f" & mRow - 1
    '             'Format cells
    '             .Range(.Cells(25, 1), .Cells(mRow, 6)).BorderAround()
    '             '.Range(.Cells(25, 3), .Cells(mRow, 6)).Borders(xlInsideVertical).Weight = xlThin
    '             mRow = mRow + 1
    '             BalanceRow = mRow
    '             .Cells(mRow, 1) = "VERIFICATION"
    '             With .Range(.Cells(mRow, 1), .Cells(mRow, 6))
    '                 .Merge()
    '                 .Font.Bold = True
    '                 .HorizontalAlignment = HorizontalAlignment.Center
    '             End With
    '             mRow = mRow + 1
    '             Dim DeclRow As Integer
    '             DeclRow = mRow
    '             ' Format this row, data will be filled after challan details...
    '             With .Range(.Cells(mRow, 1), .Cells(mRow, 6))
    '                 .Merge()
    '                 .WrapText = True
    '                 .RowHeight = 56
    '                 .HorizontalAlignment = HorizontalAlignment.Center
    '             End With
    '             mRow = mRow + 2
    '             .Cells(mRow, 1) = "Place : " & txtPlace.Text : mRow = mRow + 1
    '             .Cells(mRow, 1) = "Date : " & Format(certidt.Text) : mRow = mRow + 1
    '             .Cells(mRow, 4) = "Signature of the person responsible for deduction of tax" : mRow = mRow + 1
    '             .Cells(mRow, 4) = "Full Name   :" & txtSignByName.Text : mRow = mRow + 1
    '             .Cells(mRow, 4) = "Designation :" & txtSignByCapacity.Text
    '             .Range(.Cells(BalanceRow, 1), .Cells(mRow, 6)).BorderAround()
    '             mRow = mRow + 2
    '             If rspr.Tables(0).Rows.Count > 0 And rspr.Tables(0).Rows.Count <> 0 Or rspr.Tables(0).Rows.Count <> vbNull Then
    '                 .Cells(mRow, 1) = "The above figures includes : " : mRow = mRow + 1
    '                 .Cells(mRow, 1) = "Salary from previous employer(S) Rs. " & rspr.Tables(0).Rows(0)("TotalSalaryPreEmp") : mRow = mRow + 1
    '                 .Cells(mRow, 1) = "TDS by previous employer(S) Rs. " & rspr.Tables(0).Rows(0)("TDSAmtPreEmp") : mRow = mRow + 1
    '             End If

    '             'now the challan details...
    '             .HPageBreaks.Add(.Cells(mRow, 1))
    '             mRow = mRow + 1
    '             .Cells(mRow, 1) = "=" & Chr(34) & "Name: " & Chr(34) & " & d6"
    '             mRow = mRow + 1
    '             BalanceRow = mRow

    '             If o16A.mCoStatus = "A" Or o16A.mCoStatus = "S" Or o16A.mCoStatus = "D" Or o16A.mCoStatus = "E" Or o16A.mCoStatus = "G" Or
    '                 o16A.mCoStatus = "H" Or o16A.mCoStatus = "L" Or o16A.mCoStatus = "N" Then
    '                 'This is a Govt Co. - Show Annexure A only..
    '                 .Cells(mRow, 1) = "ANNEXURE - A"
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 1) = "DETAILS OF TAX DEDUCTED AND DEPOSITED IN THE CENTRAL GOVERNMENT ACCOUNT"
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 1) = "THROUGH BOOK ENTRY"
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 1) = "(The Employer to provide payment wise details of tax deducted and deposited with respect to the employees)"
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).RowHeight = 25
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 1) = "Sr.No."
    '                 .Range(.Cells(mRow, 1), .Cells(mRow + 1, 1)).Merge() : .Range(.Cells(mRow, 2), .Cells(mRow + 1, 2)).Merge()
    '                 .Cells(mRow, 2) = "Tax Deposited in respect of Employees (Rs)."
    '                 '                    .Range(Cells(mRow, 2), Cells(mRow + 1, 2)).Merge
    '                 .Cells(mRow, 3) = "Book identification number (BIN)"
    '                 .Range(.Cells(mRow, 3), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 3) = "Receipt No. of form No. 24G"
    '                 .Cells(mRow, 4) = "DDO Sequence No. in the Book Adjustment Mini Statement"
    '                 .Cells(mRow, 5) = "Date on which tax deposited (dd/mm/yyyy)"
    '             Else
    '                 'This is a non-Govt Co. - Show Annexure B only..
    '                 .Cells(mRow, 1) = "ANNEXURE - B"
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 1) = "DETAILS OF TAX DEDUCTED AND DEPOSITED IN THE CENTRAL GOVERNMENT ACCOUNT"
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 1) = "(The Employer to provide payment wise details of tax deducted and deposited with respect to the employees)"
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).RowHeight = 25
    '                 .Range(.Cells(mRow, 1), .Cells(mRow, 5)).Merge()
    '                 mRow = mRow + 1
    '                 .Cells(mRow, 1) = "Sr.No."
    '                 .Range(.Cells(mRow, 1), .Cells(mRow + 1, 1)).Merge()
    '                 .Cells(mRow, 2) = "Tax Deposited in respect of Employees (Rs)."
    '                 .Range(.Cells(mRow, 2), .Cells(mRow + 1, 2)).Merge()      '
    '                 .Cells(mRow, 3) = "Challan identification number (CIN)"
    '                 .Range(.Cells(mRow, 3), .Cells(mRow, 5)).Merge()     '
    '                 mRow = mRow + 1
    '                 '.Range(Cells(mRow - 1, 2), Cells(mRow, 2)).Merge
    '                 .Cells(mRow, 3) = "BSR code of the Bank Branch"
    '                 .Cells(mRow, 4) = "Date on which tax deposited (dd/mm/yyyy)"
    '                 .Cells(mRow, 5) = "Challan Serial Number"
    '             End If
    '             With .Range(.Cells(BalanceRow, 1), .Cells(mRow, 5))
    '                 .HorizontalAlignment = HorizontalAlignment.Center
    '                 .WrapText = True
    '                 .Font.Bold = True
    '             End With
    '             mRow = mRow + 1
    '             Dim CRow As Long, TotalTDSFromChallan As Double
    '             Dim rstTmp As DataSet
    '             'If rstTmp.State = adStateOpen Then rstTmp.Close

    '             rstTmp = FetchDataSet("SELECT D24.RetnID, D24.DId, D24.TaxAmt, D24.Surcharge, D24.ECess, C24.ChqDDNo, C24.BankBrCode," &
    '              " C24.DtOfChallan, C24.BankChallanNo FROM Challan24Q AS C24 INNER JOIN Deductee24Q AS D24 ON " &
    '              " C24.ChallanID = D24.ChallanId WHERE (((D24.DId)=" & TmpDID & ")) " &
    '              " Union All select 0 as expr1, 0 as expr2, F.TaxAmt, F.Surcharge, F.ECess, F.ChqDDNo, " &
    '              " F.BankBrCode, F.DtOfChallan, F.BankChallanNo FROM F16Challan as F where F.F16ID=" & F16ID & " Order by DtOfChallan")

    '             'rstTmp.Open strsql, cnn, adOpenKeyset, adLockReadOnly

    '             Dim cRowStartsFrom As Long, SrNoCtr As Long
    '             Dim mCol As Integer
    '             Addof_TaxDeductedAtSourceUs192_2 = mRow
    '             cRowStartsFrom = mRow
    '             TotalTDSFromChallan = 0 : SrNoCtr = 1
    '             Dim s As Integer
    '             For s = 0 To rstTmp.Tables(0).Rows.Count - 1
    '                 .Cells(mRow, 1) = SrNoCtr
    '                 .Cells(mRow, 2) = rstTmp.Tables(0).Rows(s)("TaxAmt") + rstTmp.Tables(0).Rows(s)("Surcharge") + rstTmp.Tables(0).Rows(s)("ECess")
    '                 .Cells(mRow, 3) = rstTmp.Tables(0).Rows(s)("BankBrCode") & ""
    '                 .Cells(mRow, 4) = IIf(o16A.mCoStatus = "A" Or o16A.mCoStatus = "S" Or o16A.mCoStatus = "D" Or o16A.mCoStatus = "E" Or o16A.mCoStatus = "G" Or
    '                 o16A.mCoStatus = "H" Or o16A.mCoStatus = "L" Or o16A.mCoStatus = "N", "", Format(rstTmp.Tables(0).Rows(s)("DtOfChallan")))
    '                 .Cells(mRow, 5) = IIf(o16A.mCoStatus = "A" Or o16A.mCoStatus = "S" Or o16A.mCoStatus = "D" Or o16A.mCoStatus = "E" Or o16A.mCoStatus = "G" Or
    '                 o16A.mCoStatus = "H" Or o16A.mCoStatus = "L" Or o16A.mCoStatus = "N", Format(rstTmp.Tables(0).Rows(s)("DtOfChallan")), rstTmp.Tables(0).Rows(s)("BankChallanNo"))
    '                 mRow = mRow + 1
    '                 SrNoCtr = SrNoCtr + 1
    '                 TotalTDSFromChallan = TotalTDSFromChallan + rstTmp.Tables(0).Rows(s)("TaxAmt") + rstTmp.Tables(0).Rows(s)("Surcharge") + rstTmp.Tables(0).Rows(s)("ECess")
    '                 'rstTmp.Move
    '             Next
    '             'Loop
    '             .Cells(mRow, 1) = "TOTAL:"
    '             .Cells(mRow, 2) = IIf(TotalTDSFromChallan = 0, 0, "=SUM(B" & cRowStartsFrom & ":B" & mRow - 1 & ")")
    '             .Range(.Cells(BalanceRow, 1), .Cells(mRow, 5)).BorderAround()
    '             ' .Range(.Cells(BalanceRow, 1), .Cells(mRow, 5)).Borders(xlInsideHorizontal).Weight = xlThin
    '             ' .Range(.Cells(BalanceRow, 1), .Cells(mRow, 5)).Borders(xlInsideVertical).Weight = xlThin
    '             'Declaration above challan details but filled here...
    '             .Cells(DeclRow, 1) = "I " & txtSignByName.Text & " son/daughter of " & txtSignByFatherName.Text & " working in the capacity of " &
    '          txtSignByCapacity.Text & " (Desig.) do hereby certify that a sum of " & TotalTDSFromChallan & " (in words) " & SpellRupee(TotalTDSFromChallan) & " has been deducted at " &
    '          "source and paid to the credit of the Central Government. I further certify that the information given above is true and correct based on the books of account, documents and other available records."
    '         End With
    '     End With
    ' End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Close()
    End Sub

    Private Sub cmdc_Click(sender As Object, e As EventArgs) Handles cmdc.Click
        Dim sql As String
        sql = (" SELECT DeductMst.*, Form16Details.* FROM DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId  WHERE Form16Details.RetnID =" & RetnId)
        cmbDeductee.Items.Clear()
        cmbDeductee.SelectedIndex = -1
        frmd.sql1 = sql
        frmd.strflg = True
        frmd.Show()
        sqld = frmd.sql1
        If sqld = "" Then
            cmbDeductee.Enabled = True
            chkOpenXL.Enabled = True
        Else
            cmbDeductee.Enabled = False
            chkOpenXL.Enabled = False
        End If
        frmd.Hide()
    End Sub

    Private Sub cmbDeductee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDeductee.SelectedIndexChanged
        If cmbDeductee.SelectedIndex >= 0 And cmbDeductee.Text <> "System.Data.DataRowView" Then
            o16A = o16A.Fetch(cmbDeductee.Text)
            txtSignByName.Text = o16A.PR24Name & ""
            txtSignByFatherName.Text = o16A.SignByFatherName & ""
            txtSignByCapacity.Text = o16A.PR24Desg & ""
            txtPlace.Text = o16A.PlaceOfForm
            certidt.Text = (Now().ToString("dd/MM/yy")) 'o16A.DateOfForm
            '.Text = oBank.State
            'cmdSave.Text = "&Save"
        Else
            'ClearBankCtrls()
        End If
    End Sub
    'Dim rs As New DataSet
    '    ' ReDim arrF16ID(0)
    '    cmbDeductee.Items.Clear()
    '    rs = FetchDataSet("SELECT * FROM FORM16DETAILS WHERE DID=" & cmbDeductee.Items(cmbDeductee.SelectedIndex).ToString)
    '    If cmbDeductee.Text <> "System.Data.DataRowView" And cmbDeductee.Text <> "" Then
    '        'ShowData(cmbDeductee.Text)
    '        'txtDName.Text = cmbDeductee.Text
    '    End If
    '    '        While Not rs.EOF
    '    '            txtSignByFatherName.Text = IIf(IsNull(rs!SignByFatherName), vbNullString, rs!SignByFatherName)
    '    '            txtPlace.Text = IIf(IsNull(rs!place), vbNullString, rs!place)
    '    '            certidt.Text = Format(IIf(IsNull(rs!DateOfCertificate), Date, rs!DateOfCertificate), "dd/mm/yy")
    '    '            RetnId = IIf(IsNull(rs!RetnId), vbNullString, rs!RetnId)
    '    '            F16ID = IIf(IsNull(rs!F16ID), vbNullString, rs!F16ID)
    '    '            arrF16ID(0) = F16ID
    '    '            Exit Sub
    '    'Wend

    '    'If rs.State = 1 Then rs.Close
    '    'Set rs = Nothing
    'End Sub



    Private Sub frm16A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Main()
        o16A = New Form16Details
        filldeductee()
        'Dim DM As New frm16A
        'Dim ds, ds1 As New DataSet
        'ds = FetchDataSet("SELECT * FROM CoMst WHERE CoID=" & selectedcoid)
        'If ds.Tables(0).Rows.Count > 0 Then
        '    With DM
        '        txtSignByName.Text = ds.Tables(0).Rows(0)("PR24Name") 'rst!PR24Name
        '        txtSignByCapacity.Text = ds.Tables(0).Rows(0)("PR24Desg") 'rst!PR24Desg

        '    End With
        'Else
        'End If
        'ds.Dispose()
        'ds1 = FetchDataSet("SELECT SignByFatherName,Place, DateOfCertificate, F16ID " _
        '       & "FROM FORM16DETAILS WHERE RETNID=" & RetnId)

        ''Do While Not ds.Tables(0).Rows(0).co
        ''    txtSignByFatherName.Text = 'IIf(IsNull(rs!SignByFatherName), vbNullString, rs!SignByFatherName)
        ''    txtPlace.Text = IIf(IsNull(rs!place), vbNullString, rs!place)
        ''    certidt.Text = Format(IIf(IsNull(rs!DateOfCertificate), Date, rs!DateOfCertificate), "dd/mm/yy")
        ''    'F16ID = rs!F16ID
        ''    Exit Do
        ''Loop
        ''If rs.State = 1 Then rs.Close
        ''Set rs = Nothing

        'txtSignByName.TabIndex = 0
        'txtSignByName.TabStop = True
        'txtSignByFatherName.TabStop = True
        'txtSignByCapacity.TabStop = True
        'txtPlace.TabStop = True
        'certidt.TabStop = True
    End Sub

    Private Sub filldeductee()

        Dim nds As New DataSet
        Dim Sql As String = "SELECT DeductMst.*, Form16Details.* FROM DeductMst INNER JOIN Form16Details ON DeductMst.DId = Form16Details.DId where DeductMst.CoId=" & selectedcoid
        cmbDeductee.DataSource = Nothing
        nds = FetchDataSet(Sql)
        If nds.Tables(0).Rows.Count > 0 Then
            cmbDeductee.DataSource = nds.Tables(0)
            'cmbDeductee.ValueMember = "DId"
            cmbDeductee.DisplayMember = "DName"
        End If
        cmbDeductee.SelectedIndex = 0
        nds.Dispose()
    End Sub

    Private Sub UpdateForm16Data(mRetID As Long)
        Dim rs As New DataSet
        Dim mSql As String


        mSql = "UPDATE FORM16DETAILS SET SignByName='"

        mSql = "UPDATE FORM16DETAILS SET SignByFatherName='" & txtSignByFatherName.Text & "', " _
               & "Place='" & txtPlace.Text & "', " _
               & "DateOfCertificate=#" & Format(certidt.Text) & "# " _
               & " WHERE RETNID=" & o16A.RetnID
        'rs.Open mSql, cnn, adOpenStatic, adLockOptimistic
        'If rs.State = 1 Then rs.Close
        rs = FetchDataSet("mSql")
        rs.Dispose()
    End Sub

    '    Private Sub GenFromXl()
    '        Dim m As Integer, i As Integer
    '        On Error GoTo ErrHandler
    '        xlapp = Nothing : xlBook = Nothing : xlSheet = Nothing
    '        xlapp = New Excel.Application : xlBook = xlapp.Workbooks.Add : xlSheet = xlBook.Worksheets("sheet1")
    '        Exit Sub
    'ErrHandler:
    '        MsgBox(Err.Description, 0 + 16, "Error...")
    '    End Sub

    Public Function OpenXLFile(XLFileName As String) As Long
        OpenXLFile = Shell(0&, "Open", XLFileName, vbNullString)
    End Function

    Private Sub chkOpenXL_CheckedChanged(sender As Object, e As EventArgs) Handles chkOpenXL.CheckedChanged

    End Sub

    Private Sub GenFromXl()
        Dim m As Integer, i As Integer
        'Dim xlApp As Excel.Application, 


        On Error GoTo ErrHandler
        xlapp = Nothing : wrkbk = Nothing : wrkst = Nothing
        xlapp = New Excel.Application : wrkbk = xlapp.Workbooks.Add : wrkst = wrkbk.Worksheets("sheet1")
        xlapp.Application.Visible = True
        'icolm = 2: ocolm = 8: hcolm = 1: fcolm = icolm + 5
        With wrkst
            .PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4
            .PageSetup.Zoom = 98
            .Range(.Cells(1, 1), .Cells(1, 1)).ColumnWidth = 8.43
            .Range(.Cells(1, 2), .Cells(1, 2)).ColumnWidth = 8.43
            .Range(.Cells(1, 3), .Cells(1, 3)).ColumnWidth = 7.71
            .Range(.Cells(1, 4), .Cells(1, 4)).ColumnWidth = 8.43
            .Range(.Cells(1, 5), .Cells(1, 5)).ColumnWidth = 7.43
            .Range(.Cells(1, 6), .Cells(1, 6)).ColumnWidth = 5.86
            .Range(.Cells(1, 7), .Cells(1, 7)).ColumnWidth = 7.29
            .Range(.Cells(1, 8), .Cells(1, 8)).ColumnWidth = 3.43
            .Range(.Cells(1, 9), .Cells(1, 9)).ColumnWidth = 7.14
            .Range(.Cells(1, 10), .Cells(1, 10)).ColumnWidth = 4.71
            .Range(.Cells(1, 11), .Cells(1, 11)).ColumnWidth = 7.29
            .Range(.Cells(1, 12), .Cells(1, 12)).ColumnWidth = 2.86

            m = 1 : i = 0        '1 Row
            m = m + 1          '2 Row
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Cells(m, 1) = "Form No. 16 "
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Bold = True : .Range(.Cells(m, 1), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 20 ' .Range(.Cells(m, 1), .Cells(m, 12)).Font.Underline = True
            m = m + 1           '3 Row
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Cells(m, 1) = "[See rule 31(1)(a)]"
            .Range(.Cells(m, 1), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Italic = False : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            m = m + 1          '4 Row
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Cells(m, 1) = "Certificate under section 203 of the Income-tax Act,1961 for tax deducted at source"
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 10 : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Bold = True
            m = m + 1          '5 Row
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Cells(m, 1) = "from income chargeable under the head 'Salaries' "
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 10 : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Bold = True
            m = m + 1          '6 Row
            .Range(.Cells(m, 1), .Cells(m, 6)).Merge() : .Range(.Cells(m, 7), .Cells(m, 12)).Merge()
            .Cells(m, 1) = "Name and Address of the Employer" : .Range(.Cells(m, 1), .Cells(m, 6)).BorderAround() : .Range(.Cells(m, 1), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center
            .Cells(m, 7) = "Name and Designation of the Employee"
            .Range(.Cells(m, 6), .Cells(m, 12)).BorderAround() : .Range(.Cells(m, 7), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            m = m + 1          '7 Row
            .Range(.Cells(m, 1), .Cells(m + 2, 6)).Merge() ' True: .Range(.Cells(m, 1), .Cells(m + 2, 6)).VerticalAlignment = xlTop
            .Range(.Cells(m, 1), .Cells(m + 2, 6)).WrapText = True : .Range(.Cells(m, 1), .Cells(m + 2, 6)).BorderAround() : .Range(.Cells(m, 1), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 1), .Cells(m + 2, 6)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 7), .Cells(m + 2, 12)).Merge() 'True: .Range(.Cells(m, 7), .Cells(m + 2, 12)).VerticalAlignment = xlTop
            .Range(.Cells(m, 7), .Cells(m + 2, 12)).WrapText = True : .Range(.Cells(m, 7), .Cells(m + 2, 12)).BorderAround() : .Range(.Cells(m, 1), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 7), .Cells(m + 2, 12)).HorizontalAlignment = HorizontalAlignment.Center
            m = m + 3          '11 Row leave 3 Rows Blank
            .Range(.Cells(m, 1), .Cells(m, 3)).Merge() : .Range(.Cells(m, 1), .Cells(m, 3)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 1), .Cells(m, 3)).Font.Size = 8
            .Range(.Cells(m, 1), .Cells(m, 3)).BorderAround()
            .Range(.Cells(m, 4), .Cells(m, 6)).Merge() : .Range(.Cells(m, 4), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 4), .Cells(m, 6)).Font.Size = 8
            .Range(.Cells(m, 4), .Cells(m, 6)).BorderAround()
            .Range(.Cells(m, 7), .Cells(m, 12)).Merge() : .Range(.Cells(m, 7), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 7), .Cells(m, 12)).Font.Size = 8
            .Range(.Cells(m, 7), .Cells(m, 12)).BorderAround()
            .Cells(m, 1) = "PAN" : .Cells(m, 4) = "TAN" : .Cells(m, 7) = "PAN/GIR NO."
            m = m + 1          '12 Row     Print Blank Row
            .Range(.Cells(m, 1), .Cells(m, 3)).Merge() : .Range(.Cells(m, 1), .Cells(m, 3)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 1), .Cells(m, 3)).BorderAround()
            .Range(.Cells(m, 4), .Cells(m, 6)).Merge() : .Range(.Cells(m, 4), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 4), .Cells(m, 6)).BorderAround()
            .Range(.Cells(m, 7), .Cells(m, 12)).Merge() : .Range(.Cells(m, 7), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 7), .Cells(m, 12)).BorderAround()
            m = m + 1          '13 Row
            .Range(.Cells(m, 1), .Cells(m, 6)).Merge()
            '.Range(.Cells(m, 1), .Cells(m + 1, 6)).VerticalAlignment = xlTop
            .Range(.Cells(m, 1), .Cells(m + 1, 8)).BorderAround()
            .Cells(m, 1) = "TDS Circle where Annual Return/Statement under "
            .Range(.Cells(m, 1), .Cells(m + 1, 12)).Font.Size = 8
            .Cells(m + 1, 1) = "section 206 is to be filed "
            .Cells(m, 8) = "Period" : .Cells(m, 11) = "Assessment Year"
            .Range(.Cells(m, 7), .Cells(m, 10)).Merge() : .Range(.Cells(m, 7), .Cells(m, 10)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 7), .Cells(m, 10)).BorderAround()
            .Range(.Cells(m, 11), .Cells(m + 1, 12)).Merge() : .Range(.Cells(m, 11), .Cells(m + 1, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 9), .Cells(m + 1, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 9), .Cells(m + 1, 12)).VerticalAlignment = HorizontalAlignment.Center
            m = m + 1          '11 Row
            .Cells(m, 7) = "FROM" : .Cells(m, 9) = "TO"
            .Range(.Cells(m, 7), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge()
            .Range(.Cells(m, 7), .Cells(m, 9)).BorderAround()
            .Range(.Cells(m, 9), .Cells(m, 10)).BorderAround()
            .Range(.Cells(m, 11), .Cells(m, 12)).Merge() : .Range(.Cells(m, 7), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 11), .Cells(m, 12)).WrapText = True
            .Range(.Cells(m - 1, 11), .Cells(m, 12)).BorderAround()
            m = m + 1
            .Cells(m, 1) = "Quarter" : .Cells(m, 4) = "Acknowledgement No."        '12 Row
            .Range(.Cells(m, 1), .Cells(m, 3)).Merge() : .Range(.Cells(m, 4), .Cells(m, 6)).Merge()
            .Range(.Cells(m, 1), .Cells(m, 3)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m, 4), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 1), .Cells(m, 3)).BorderAround() : .Range(.Cells(m, 4), .Cells(m, 6)).BorderAround()
            .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m, 7), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge()
            .Range(.Cells(m, 7), .Cells(m, 8)).BorderAround()
            .Range(.Cells(m, 9), .Cells(m, 10)).BorderAround()
            .Range(.Cells(m, 7), .Cells(m, 8)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m, 9), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 11), .Cells(m, 12)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m, 6), .Cells(m, 12)).BorderAround()
            i = m
            For i = m To m + 4
                .Range(.Cells(m, 1), .Cells(m, 3)).Merge() : .Range(.Cells(m, 4), .Cells(m, 6)).Merge()
                .Range(.Cells(m, 4), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m, 1), .Cells(m, 3)).HorizontalAlignment = HorizontalAlignment.Center
                .Range(.Cells(m, 1), .Cells(m, 3)).BorderAround() : .Range(.Cells(m, 4), .Cells(m, 6)).BorderAround()
                .Range(.Cells(m, 11), .Cells(m, 12)).Merge()

                m = i + 1
            Next i
            .Range(.Cells(m - 4, 7), .Cells(m - 1, 12)).BorderAround() : .Range(.Cells(m, 4), .Cells(m, 6)).BorderAround()
            .Cells(m, 1) = "DETAILS OF SALARY PAID AND ANY OTHER INCOME AND TAX DEDUCTED"
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge()
            .Range(.Cells(m, 1), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center
            '.Range(.Cells(m, 1), .Cells(m, 12)).VerticalAlignment = xlTop
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Bold = True

            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = True
            .Cells(m, 1) = "1.  Gross Salary" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = False
            .Cells(m, 1) = "a.  Salary as per provisions" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = False
            .Cells(m, 1) = "    contained un sec 17(1)" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m - 1, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m - 1, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m - 1, 11), .Cells(m, 12)).Merge()
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = False
            .Cells(m, 1) = "b.  Value as per perquisites u/s 17(2)" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = False
            .Cells(m, 1) = "    (as per Form No. 12BA,whenever applicable)" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m - 1, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m - 1, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m - 1, 11), .Cells(m, 12)).Merge()
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = False
            .Cells(m, 1) = "c.  Profit in lieu of salary u/s 17(3)" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = False
            .Cells(m, 1) = "    (as per Form No 12BA,whenever applicable)" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m - 1, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m - 1, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m - 1, 11), .Cells(m, 12)).Merge()
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = True
            .Cells(m, 1) = "d.  Total" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
            .Range(.Cells(m - 7, 1), .Cells(m, 5)).BorderAround()
            .Range(.Cells(m - 7, 6), .Cells(m, 8)).BorderAround()
            .Range(.Cells(m - 7, 9), .Cells(m, 10)).BorderAround()
            .Range(.Cells(m - 7, 11), .Cells(m, 12)).BorderAround()
            '2 section starts
            m = m + 1 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = True
            .Cells(m, 1) = "2.  Less: Allowance to the extent exempt u/s 10" : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
            '.Range(.Cells(m, 6), .Cells(m, 12)).BorderAround ():
            Dim mRows As Integer

            i = m
            For i = m To m + 16
                .Range(.Cells(m, 1), .Cells(m, 3)).Merge() : .Range(.Cells(m, 4), .Cells(m, 5)).Merge() : .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()

                If mRows = 6 Or mRows = 7 Or mRows = 10 Or mRows = 11 Or mRows = 12 Or mRows = 13 Or mRows = 16 Then
                    .Range(.Cells(m, 1), .Cells(m, 5)).BorderAround()
                    .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8 : .Range(.Cells(m, 1), .Cells(m, 5)).Merge()
                Else
                    .Range(.Cells(m, 1), .Cells(m, 3)).BorderAround() : .Range(.Cells(m, 4), .Cells(m, 5)).BorderAround()
                    .Range(.Cells(m, 1), .Cells(m, 3)).Font.Size = 8 : .Range(.Cells(m, 4), .Cells(m, 5)).Font.Size = 8
                End If
                m = i + 1
                mRows = mRows + 1
            Next i
            mRows = 0
            .Cells(m - 16, 1) = "Details" : .Cells(m - 16, 4) = "Rs." : .Cells(m - 12, 1) = "Total"
            .Cells(m - 11, 1) = "3.  Balance(1-2)" : .Cells(m - 10, 1) = "4.  Deductions" : .Cells(m - 7, 1) = "3.  Aggregate of 4(a) and (b)"
            .Cells(m - 6, 1) = "6.  Income Chargable under the Head " : .Cells(m - 5, 1) = "   Salaries(3-5)"
            .Cells(m - 4, 1) = "7.  Add: Any Other Income reported by the employee"
            .Cells(m - 3, 1) = "Details" : .Cells(m - 3, 4) = "Rs." : .Cells(m - 1, 1) = "8.  Gross Total Income(6+7)"
            .Range(.Cells(m - 16, 1), .Cells(m - 16, 3)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 16, 1), .Cells(m - 16, 3)).Font.Bold = True
            .Range(.Cells(m - 16, 4), .Cells(m - 16, 5)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 16, 4), .Cells(m - 16, 5)).Font.Bold = True

            .Range(.Cells(m - 3, 1), .Cells(m - 3, 3)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 3, 1), .Cells(m - 3, 3)).Font.Bold = True
            .Range(.Cells(m - 3, 4), .Cells(m - 3, 5)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 3, 4), .Cells(m - 3, 5)).Font.Bold = True

            .Range(.Cells(m - 12, 1), .Cells(m - 12, 1)).HorizontalAlignment = HorizontalAlignment.Right
            .Range(.Cells(m - 17, 6), .Cells(m - 11, 8)).BorderAround()
            .Range(.Cells(m - 17, 9), .Cells(m - 11, 10)).BorderAround()
            .Range(.Cells(m - 17, 11), .Cells(m - 11, 12)).BorderAround()
            .Range(.Cells(m - 10, 6), .Cells(m - 7, 8)).BorderAround()
            .Range(.Cells(m - 10, 9), .Cells(m - 7, 10)).BorderAround()
            .Range(.Cells(m - 10, 11), .Cells(m - 7, 12)).BorderAround()
            .Range(.Cells(m - 7, 6), .Cells(m - 7, 8)).BorderAround()
            .Range(.Cells(m - 7, 9), .Cells(m - 7, 10)).BorderAround()
            .Range(.Cells(m - 7, 11), .Cells(m - 7, 12)).BorderAround()
            .Range(.Cells(m - 6, 6), .Cells(m - 5, 8)).Merge() : .Range(.Cells(m - 6, 9), .Cells(m - 5, 10)).Merge() : .Range(.Cells(m - 6, 11), .Cells(m - 5, 12)).Merge()
            .Range(.Cells(m - 6, 6), .Cells(m - 5, 8)).BorderAround()
            .Range(.Cells(m - 6, 9), .Cells(m - 5, 10)).BorderAround()
            .Range(.Cells(m - 6, 11), .Cells(m - 5, 12)).BorderAround()
            .Range(.Cells(m - 4, 6), .Cells(m - 3, 8)).Merge() : .Range(.Cells(m - 4, 9), .Cells(m - 3, 10)).Merge() : .Range(.Cells(m - 4, 11), .Cells(m - 3, 12)).Merge()
            .Range(.Cells(m - 4, 6), .Cells(m - 2, 8)).BorderAround()
            .Range(.Cells(m - 4, 9), .Cells(m - 2, 10)).BorderAround()
            .Range(.Cells(m - 4, 11), .Cells(m - 2, 12)).BorderAround()
            .Range(.Cells(m - 1, 6), .Cells(m - 1, 8)).BorderAround()
            .Range(.Cells(m - 1, 9), .Cells(m - 1, 10)).BorderAround()
            .Range(.Cells(m - 1, 11), .Cells(m - 1, 12)).BorderAround()
            'CODE BELLOW 9 SECTION
            i = m
            For i = m To m + 13
                .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
                .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
                m = i + 1
            Next i
            .Cells(m - 14, 1) = "9.  Deductions under Chapter VI A" : .Cells(m - 13, 1) = "A.  Sections 80C"
            .Cells(m - 12, 6) = "Gross Amount" : .Cells(m - 12, 9) = "Deductible Amount" : .Cells(m - 1, 1) = "Total"
            .Range(.Cells(3, 1), .Cells(m, 12)).RowHeight = 12
            .Cells(m, 1) = "  Note: 1. Aggregate amount deductible u/s 80C shall not exceed One Lack Rupees"
            .Range(.Cells(m - 1, 1), .Cells(m - 1, 5)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 12, 6), .Cells(m - 12, 6)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 12, 9), .Cells(m - 12, 10)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m - 12, 6), .Cells(m - 12, 8)).Font.Underline = True : .Range(.Cells(m - 12, 9), .Cells(m - 12, 10)).Font.Underline = True
            .Range(.Cells(m - 12, 6), .Cells(m - 12, 8)).Font.Size = 8 : .Range(.Cells(m - 12, 9), .Cells(m - 12, 10)).Font.Size = 8
            .Range(.Cells(m - 15, 1), .Cells(m - 1, 5)).BorderAround()
            .Range(.Cells(m - 15, 6), .Cells(m - 1, 8)).BorderAround()
            .Range(.Cells(m - 15, 9), .Cells(m - 1, 10)).BorderAround()
            .Range(.Cells(m - 15, 11), .Cells(m - 1, 12)).BorderAround()
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 6
            m = m + 1
            i = m
            mRows = 0
            For i = m To m + 12
                If mRows > 8 Then
                    .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
                    .Range(.Cells(m, 1), .Cells(m, 5)).BorderAround() : .Range(.Cells(m, 4), .Cells(m, 5)).BorderAround()
                    .Range(.Cells(m, 6), .Cells(m, 8)).BorderAround()
                    .Range(.Cells(m, 9), .Cells(m, 10)).BorderAround()
                    .Range(.Cells(m, 11), .Cells(m, 12)).BorderAround()
                Else
                    .Range(.Cells(m, 1), .Cells(m, 3)).Merge() : .Range(.Cells(m, 4), .Cells(m, 5)).Merge() : .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
                End If
                .Range(.Cells(m, 1), .Cells(m, 1)).Font.Size = 8 : .Range(.Cells(m, 1), .Cells(m, 1)).WrapText = True
                m = i + 1
                mRows = mRows + 1
            Next i
            mRows = 0
            .Cells(m - 13, 1) = "B.  Other Sections (For e.g. 80E," : .Cells(m - 12, 1) = "    80G etc.) under chapter VI A"
            .Cells(m - 11, 4) = "Gross Amount" : .Cells(m - 11, 6) = "Qualifying Amount" : .Cells(m - 11, 9) = "Deductible Amount"
            .Cells(m - 5, 1) = "Total" : .Range(.Cells(m - 5, 1), .Cells(m - 5, 1)).HorizontalAlignment = HorizontalAlignment.Right
            .Cells(m - 4, 1) = "10.  Aggregate of Deductible Amounts under Chapter VI A"
            .Cells(m - 3, 1) = "11.  Total Income (8-10)" : .Cells(m - 2, 1) = "12.  Tax on Total Income"
            .Cells(m - 1, 1) = "13.  Surcharge (On tax computed at S.No. 12)" : .Cells(m, 1) = "14.  Education Cess @ 2% (On tax at S. No. 12 and"
            .Cells(m + 1, 1) = "   surcharge at S.No. 13)"
            .Range(.Cells(m - 11, 4), .Cells(m - 11, 4)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 11, 6), .Cells(m - 11, 6)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 11, 9), .Cells(m - 11, 9)).HorizontalAlignment = HorizontalAlignment.Center
            .Range(.Cells(m - 11, 4), .Cells(m - 11, 4)).Font.Underline = True : .Range(.Cells(m - 11, 6), .Cells(m - 11, 9)).Font.Underline = True
            .Range(.Cells(m - 11, 4), .Cells(m - 11, 4)).Font.Size = 8 : .Range(.Cells(m - 11, 6), .Cells(m - 11, 9)).Font.Size = 8
            .Range(.Cells(m - 13, 1), .Cells(m - 5, 5)).BorderAround()
            .Range(.Cells(m - 13, 6), .Cells(m - 5, 8)).BorderAround()
            .Range(.Cells(m - 13, 9), .Cells(m - 5, 10)).BorderAround()
            .Range(.Cells(m - 13, 11), .Cells(m - 5, 12)).BorderAround()

            .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 6), .Cells(m + 1, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m + 1, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m + 1, 12)).Merge()
            m = m + 1
            For i = m To m + 9
                .Range(.Cells(m, 1), .Cells(m, 5)).Merge() : .Range(.Cells(m, 6), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
                .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
                m = i + 1
            Next i
            .Cells(m - 9, 1) = "15.   Tax Payable (12 + 13 + 14 + 15)"
            .Cells(m - 8, 1) = "16.   Relief u/s 89(attach details)"
            .Cells(m - 7, 1) = "17.   Tax Payable (16 - 17)"
            .Cells(m - 6, 1) = "18.   Less: (a)Tax deducted at source u/s 192(1)"
            .Cells(m - 5, 1) = "            (b)Tax paid  by employer on befalf "
            .Cells(m - 4, 1) = "               of the employee u/s 192(1A) on "
            .Cells(m - 3, 1) = "               perquisites u/s 17(2)"
            .Cells(m - 1, 1) = "Tax Payable/ Refundable (17 - 18)"
            'Border for above Head
            .Range(.Cells(m - 11, 1), .Cells(m - 10, 5)).BorderAround()
            .Range(.Cells(m - 11, 6), .Cells(m - 10, 8)).BorderAround()
            .Range(.Cells(m - 11, 9), .Cells(m - 10, 10)).BorderAround()
            .Range(.Cells(m - 11, 11), .Cells(m - 10, 12)).BorderAround()
            'Border for Bellow Tax Payable
            .Range(.Cells(m - 9, 1), .Cells(m - 9, 5)).BorderAround()
            .Range(.Cells(m - 9, 6), .Cells(m - 9, 8)).BorderAround()
            .Range(.Cells(m - 9, 9), .Cells(m - 9, 10)).BorderAround()
            .Range(.Cells(m - 9, 11), .Cells(m - 9, 12)).BorderAround()
            .Range(.Cells(m - 8, 1), .Cells(m - 8, 5)).BorderAround()
            .Range(.Cells(m - 8, 6), .Cells(m - 8, 8)).BorderAround()
            .Range(.Cells(m - 8, 9), .Cells(m - 8, 10)).BorderAround()
            .Range(.Cells(m - 8, 11), .Cells(m - 8, 12)).BorderAround()
            .Range(.Cells(m - 7, 1), .Cells(m - 7, 5)).BorderAround()
            .Range(.Cells(m - 7, 6), .Cells(m - 7, 8)).BorderAround()
            .Range(.Cells(m - 7, 9), .Cells(m - 7, 10)).BorderAround()
            .Range(.Cells(m - 7, 11), .Cells(m - 7, 12)).BorderAround()
            .Range(.Cells(m - 6, 1), .Cells(m - 2, 5)).BorderAround()
            .Range(.Cells(m - 6, 6), .Cells(m - 2, 8)).BorderAround()
            .Range(.Cells(m - 6, 9), .Cells(m - 2, 10)).BorderAround()
            .Range(.Cells(m - 6, 11), .Cells(m - 2, 12)).BorderAround()
            .Range(.Cells(m - 1, 1), .Cells(m - 1, 5)).BorderAround()
            .Range(.Cells(m - 1, 6), .Cells(m - 1, 8)).BorderAround()
            .Range(.Cells(m - 1, 9), .Cells(m - 1, 10)).BorderAround()
            .Range(.Cells(m - 1, 11), .Cells(m - 1, 12)).BorderAround()
            'Details of tax Deduction
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() '.Range(.Cells(m, 1), .Cells(m, 12)).font.Size=8
            .Cells(m, 1) = "DETAILS OF TAX DEDUCTED AND DEPOSITED INTO CENTRAL GOVERNMENT ACCOUNT"
            .Range(.Cells(m, 1), .Cells(m, 1)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Bold = True
            .Range(.Cells(m - 1, 1), .Cells(m, 12)).BorderAround()
            m = m + 1
            'Drawing Table
            i = m
            For i = m To m + 13
                .Range(.Cells(m, 7), .Cells(m, 8)).Merge() : .Range(.Cells(m, 9), .Cells(m, 10)).Merge() : .Range(.Cells(m, 11), .Cells(m, 12)).Merge()
                .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
                m = i + 1
            Next i
            .Range(.Cells(m - 14, 1), .Cells(m - 14, 12)).RowHeight = 40.5 : .Range(.Cells(m - 14, 1), .Cells(m - 14, 12)).WrapText = True
            .Range(.Cells(m - 14, 1), .Cells(m - 14, 12)).Font.Size = 8 : .Range(.Cells(m - 14, 1), .Cells(m - 14, 12)).Font.Bold = True
            .Range(.Cells(m - 14, 1), .Cells(m - 14, 12)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m - 14, 1), .Cells(m - 14, 12)).VerticalAlignment = HorizontalAlignment.Center
            .Cells(m - 14, 1) = "S.No." : .Cells(m - 14, 2) = "TDS         Rs." : .Cells(m - 14, 3) = "Surcharge Rs."
            .Cells(m - 14, 4) = "   E.Cess Rs." : .Cells(m - 14, 5) = "Total Tax Deposited Rs." : .Cells(m - 14, 6) = "Cheque / DD No.                 (If any)"
            .Cells(m - 14, 7) = "BCR Code of Bank Branch" : .Cells(m - 14, 9) = "Date on which Tax Deposited(dd/mm/yy)" : .Cells(m - 14, 11) = "Transfer Voucher/Challan Identification No."
            .Cells(m - 1, 1) = "TOTAL" : .Range(.Cells(m - 1, 1), .Cells(m - 1, 1)).HorizontalAlignment = HorizontalAlignment.Right
            .Range(.Cells(m - 1, 1), .Cells(m - 1, 4)).Merge() : .Range(.Cells(m - 1, 6), .Cells(m - 1, 12)).Merge()
            .Range(.Cells(m - 14, 1), .Cells(m - 14, 12)).BorderAround()
            .Range(.Cells(m - 14, 1), .Cells(m - 14, 1)).BorderAround()
            .Range(.Cells(m - 14, 2), .Cells(m - 14, 2)).BorderAround()
            .Range(.Cells(m - 14, 3), .Cells(m - 14, 3)).BorderAround()
            .Range(.Cells(m - 14, 4), .Cells(m - 14, 4)).BorderAround()
            .Range(.Cells(m - 14, 5), .Cells(m - 14, 5)).BorderAround()
            .Range(.Cells(m - 14, 6), .Cells(m - 14, 6)).BorderAround()
            .Range(.Cells(m - 14, 7), .Cells(m - 14, 8)).BorderAround()
            .Range(.Cells(m - 14, 9), .Cells(m - 14, 10)).BorderAround()
            .Range(.Cells(m - 14, 11), .Cells(m - 14, 12)).BorderAround()

            'For Last row of Table
            .Range(.Cells(m - 1, 1), .Cells(m - 1, 4)).BorderAround()
            .Range(.Cells(m - 1, 5), .Cells(m - 1, 5)).BorderAround()
            .Range(.Cells(m - 1, 6), .Cells(m - 1, 12)).BorderAround()
            'Declaration
            m = m + 1
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            .Cells(m, 1) = "I          son/daughter of         working in the capacity of  (Designation)"
            m = m + 1
            .Range(.Cells(m, 1), .Cells(m, 3)).Merge() : .Cells(m, 1) = "do hereby certify that a sum of Rs."
            .Range(.Cells(m, 4), .Cells(m, 4)).Font.Bold = True : .Range(.Cells(m, 1), .Cells(m, 5)).Font.Size = 8
            .Range(.Cells(m, 6), .Cells(m, 12)).Merge() : .Range(.Cells(m, 6), .Cells(m, 12)).Font.Bold = True
            .Cells(m, 5) = " (in words)" : .Range(.Cells(m, 6), .Cells(m, 12)).Font.Size = 6
            m = m + 1
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            .Cells(m, 1) = "has been deducted at source and paid to the credit of the Central Government. I further certify that the information given above"
            m = m + 1
            .Range(.Cells(m, 1), .Cells(m, 12)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            .Cells(m, 1) = "is true and Correct based on the books of account,documents  and available records."
            m = m + 2
            '.Range(.Cells(m, 1), .Cells(m, 12)).Merge:
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            .Cells(m, 1) = "Place:" : .Range(.Cells(m, 1), .Cells(m, 1)).Font.Italic = True
            .Range(.Cells(m, 1), .Cells(m, 1)).HorizontalAlignment = HorizontalAlignment.Right
            m = m + 1
            .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8 : .Range(.Cells(m, 1), .Cells(m, 1)).HorizontalAlignment = HorizontalAlignment.Right
            .Cells(m, 1) = "Date:" : .Range(.Cells(m, 1), .Cells(m, 1)).Font.Italic = True
            m = m + 1
            .Range(.Cells(m, 6), .Cells(m, 12)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            .Range(.Cells(m, 6), .Cells(m, 6)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m, 6), .Cells(m, 6)).Font.Italic = True
            .Cells(m, 6) = "Signature of the person responsible for deduction of tax:"
            m = m + 1
            .Range(.Cells(m, 6), .Cells(m, 7)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            .Range(.Cells(m, 6), .Cells(m, 7)).HorizontalAlignment = HorizontalAlignment.Right : .Range(.Cells(m, 6), .Cells(m, 7)).Font.Italic = True
            .Cells(m, 6) = "Full Name    :" : .Range(.Cells(m, 8), .Cells(m, 8)).Font.Bold = True : .Range(.Cells(m, 8), .Cells(m, 12)).Merge()
            m = m + 1
            .Range(.Cells(m, 6), .Cells(m, 7)).Merge() : .Range(.Cells(m, 1), .Cells(m, 12)).Font.Size = 8
            .Range(.Cells(m, 6), .Cells(m, 7)).HorizontalAlignment = HorizontalAlignment.Right : .Range(.Cells(m, 6), .Cells(m, 7)).Font.Italic = True
            .Cells(m, 6) = "Designation :" : .Range(.Cells(m, 8), .Cells(m, 8)).Font.Bold = True : .Range(.Cells(m, 8), .Cells(m, 12)).Merge()
            m = m + 1
            .Range(.Cells(2, 1), .Cells(m, 12)).BorderAround() : .Range(.Cells(m, 7), .Cells(m, 12)).HorizontalAlignment = HorizontalAlignment.Center

        End With
        Exit Sub

ErrHandler:
        MsgBox(Err.Description, 0 + 16, "Error...")

    End Sub


    Private Sub FillForm16()
        Dim NoOfRowsInserted As Long, sql As String, sql2 As String
        Dim i As Long, rst, rstTmp, rsQrt As New DataSet, TmpDID As Long
        Dim SqlQurt As String
        Dim mDname As String, TmpCellAdd As Long, AddOfGrossTotInc As Long
        Dim Addof_TaxDeductedAtSourceUs192_2 As Long

        GenFromXl()
        rst = FetchDataSet("SELECT Form16Details.*, Form16MoreDetails.*, Form16Details.RetnID, Form16Details.DId" &
    " FROM Form16Details INNER JOIN Form16MoreDetails ON Form16Details.F16ID = Form16MoreDetails.F16ID" &
    " WHERE (((Form16Details.RetnID)=" & o16A.RetnID & ") AND ((Form16Details.F16Id)=" & o16A.F16ID & "));")

        TmpDID = o16A.did 'rst.Fields("Form16Details.DID")

        sql2 = "SELECT * from DeductMst WHERE DId=" & TmpDID & " and dname in ('" & cmbDeductee.Text & "')"

        rstTmp = FetchDataSet(sql2)
        With wrkst
            'Name and Address of Employer..
            .Cells(7, 1) = o16A.mCoName
            .Cells(8, 1) = o16A.mCoAdd1 & " " & o16A.mCoAdd2 & " " & o16A.mCoAdd3
            .Cells(9, 1) = o16A.mCoAdd4 & " " & o16A.mCoAdd5 & "-" & o16A.mCoPin
            .Cells(11, 1) = o16A.mCoPAN
            .Cells(11, 4) = o16A.mCoTAN
            'Name and Designation of Employee..
            .Cells(7, 7) = rstTmp.Tables(0).Rows(0)("DName").ToString()
            mDname = rstTmp.Tables(0).Rows(0)("DName").ToString()
            .Cells(8, 7) = rst.Tables(0).Rows(0)("DDesgn").ToString()
            .Cells(11, 7) = rstTmp.Tables(0).Rows(0)("DPan").ToString()
            .Cells(14, 7) = Format(rst.Tables(0).Rows(0)("EmpFromDt"), "dd/MMM/yy")
            .Cells(14, 9) = Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MMM/yy")
            '.Cells(14, 11) = o16A.mAYear

            .Cells(15, 1) = "1st Quarter"
            .Cells(16, 1) = "2nd Quarter"
            .Cells(17, 1) = "3rd Quarter"
            .Cells(18, 1) = "4th Quarter"
            'Qtry return data...
            SqlQurt = " SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#30/06/" & Year(FromDate) & "# between #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# and #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "#" _
                 & " or #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# < #30/06/" & Year(FromDate) & " #) and r.frmtype='24Q1'" _
                 & " Union All  SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#30/09/" & Year(FromDate) & "# between #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# and #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "#" _
                 & " or #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# between #01/07/" & Year(FromDate) & " # and  #30/09/" & Year(FromDate) & "#) and r.frmtype='24Q2'" _
                 & " Union All  SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#31/12/" & Year(FromDate) & "# between #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# and #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "#" _
                 & " or #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# between #01/10/" & Year(FromDate) & " # and  #31/12/" & Year(FromDate) & "#)and r.frmtype='24Q3'" _
                 & " Union All SELECT r.retnid, r.dtoffiling,r.prn,r.rprn,r.frmtype FROM  retnmst AS r, comst AS c  Where  c.coid = r.coid   and c.coid=" & selectedcoid & "  and (#031/03/" & Year(ToDate) & "# between #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# and #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "#" _
                 & " or #" & Format(rst.Tables(0).Rows(0)("EmpToDt"), "dd/MM/yyyy") & "# between #01/01/" & Year(ToDate) & " # and  #31/03/" & Year(ToDate) & "#) and r.frmtype='24Q4'"
            rsQrt = FetchDataSet(SqlQurt)
            For i = 0 To rsQrt.Tables(0).Rows.Count - 1
                If String.IsNullOrEmpty(rsQrt.Tables(0).Rows(i)("PRN").ToString()) Then
                    If Strings.Right(rsQrt.Tables(0).Rows(0)("FrmType"), 1) = 1 Then
                        .Cells(15, 4) = rsQrt.Tables(0).Rows(i)("PRN")
                    ElseIf Strings.Right(rsQrt.Tables(0).Rows(0)("FrmType"), 1) = 2 Then
                        .Cells(16, 4) = rsQrt.Tables(0).Rows(i)("PRN")
                    ElseIf Strings.Right(rsQrt.Tables(0).Rows(0)("FrmType"), 1) = 3 Then
                        .Cells(17, 4) = rsQrt.Tables(0).Rows(i)("PRN")
                    ElseIf Strings.Right(rsQrt.Tables(0).Rows(0)("FrmType"), 1) = 4 Then
                        .Cells(18, 4) = rsQrt.Tables(0).Rows(i)("PRN").ToString()
                    End If
                End If
                'rsQrt.MoveNext
            Next
            rsQrt.Dispose()
            rsQrt = Nothing
            'Details of Salary (fill all data from rst recordset first...after that we will fill
            'more detail records...
            .Cells(21, 6) = rst.Tables(0).Rows(0)("Gross1").ToString()
            .Cells(23, 6) = rst.Tables(0).Rows(0)("Gross2").ToString()
            .Cells(25, 6) = rst.Tables(0).Rows(0)("Gross3").ToString()
            .Cells(21, 6).NumberFormat = "0.00_);(0.00)"
            .Cells(23, 6).NumberFormat = "0.00_);(0.00)"
            .Cells(25, 6).NumberFormat = "0.00_);(0.00)"
            .Cells(27, 9) = "=SUM(F21:F26)"
            .Cells(27, 9).NumberFormat = "0.00_);(0.00)"
            .Cells(36, 1) = "Entertainment Allowance"
            .Cells(36, 4) = rst.Tables(0).Rows(0)("Sec16ii").ToString()
            .Cells(36, 4).NumberFormat = "0.00_);(0.00)"
            .Cells(37, 1) = "Tax on Employment"
            .Cells(37, 4) = rst.Tables(0).Rows(0)("Sec16iii").ToString()
            .Cells(37, 4).NumberFormat = "0.00_);(0.00)"
            .Cells(71, 11) = rst.Tables(0).Rows(0)("TaxAmt").ToString()
            .Cells(72, 11) = rst.Tables(0).Rows(0)("Surcharge").ToString()
            .Cells(73, 11) = rst.Tables(0).Rows(0)("ECess").ToString()
            .Cells(75, 11) = "=SUM(K71:K74)"
            .Cells(76, 11) = rst.Tables(0).Rows(0)("Relief89").ToString()
            .Cells(77, 11) = "=K75-K76"
            .Cells(79, 9) = rst.Tables(0).Rows(0)("TDSOnPerks").ToString()
            .Cells(79, 11) = "=SUM(I78:I79)"
            .Cells(83, 11) = "=K77-K79"
            .Cells(71, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(72, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(73, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(75, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(76, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(77, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(79, 9).NumberFormat = "0.00_);(0.00)"
            .Cells(79, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(83, 11).NumberFormat = "0.00_);(0.00)"

            .Cells(100, 1) = "         I " & txtSignByName.Text & " son/daughter of " & txtSignByFatherName.Text & " working in the capacity of " &
             txtSignByCapacity.Text & " (Desig.)"
            .Cells(105, 2) = txtPlace.Text
            .Cells(106, 2) = certidt.Text
            .Cells(108, 8) = txtSignByName.Text
            .Cells(109, 8) = txtSignByCapacity.Text
            'Fill data from more details table...
            Dim Arow As Long, Orow As Long, Erow As Long, Vrow As Long
            Arow = 30 : Erow = 0 : Vrow = 0
            'allowances details...
            'rst.Filter = "TypeOfDetail='A'"
            ' rst.MoveFirst
            For i = 0 To rst.Tables(0).Rows.Count - 1
                If Arow > 32 Then
                    .Rows(Arow).Insert
                    NoOfRowsInserted = NoOfRowsInserted + 1
                    .Range(.Cells(32 + NoOfRowsInserted, 1), .Cells(32 + NoOfRowsInserted, 3)).Merge
                    .Range(.Cells(32 + NoOfRowsInserted, 4), .Cells(32 + NoOfRowsInserted, 5)).Merge
                    .Range(.Cells(32 + NoOfRowsInserted, 6), .Cells(32 + NoOfRowsInserted, 8)).Merge
                    .Range(.Cells(32 + NoOfRowsInserted, 9), .Cells(32 + NoOfRowsInserted, 10)).Merge
                    .Range(.Cells(32 + NoOfRowsInserted, 11), .Cells(32 + NoOfRowsInserted, 12)).Merge
                    .Range(.Cells(32 + NoOfRowsInserted, 1), .Cells(32 + NoOfRowsInserted, 3)).BorderAround()
                    .Range(.Cells(32 + NoOfRowsInserted, 4), .Cells(32 + NoOfRowsInserted, 5)).BorderAround()
                End If
                .Cells(Arow, 1) = rst.Tables(0).Rows(0)("Particulars").ToString()
                .Cells(Arow, 4) = rst.Tables(0).Rows(0)("GrossAmt").ToString()
                .Cells(Arow, 4).NumberFormat = "0.00_);(0.00)"

                Arow = Arow + 1
                'rst.MoveNext
            Next
            .Cells(33 + NoOfRowsInserted, 4) = "=SUM(D30:D" & Arow - 1 & ")"
            .Cells(33 + NoOfRowsInserted, 9) = "=SUM(D30:D" & Arow - 1 & ")"
            .Cells(34 + NoOfRowsInserted, 9) = "=I27-I" & 33 + NoOfRowsInserted
            .Cells(38 + NoOfRowsInserted, 6) = "=D" & 36 + NoOfRowsInserted & "+D" & 37 + NoOfRowsInserted
            .Cells(39 + NoOfRowsInserted, 11) = "=I" & 34 + NoOfRowsInserted & "-F" & 38 + NoOfRowsInserted
            .Cells(33 + NoOfRowsInserted, 4).NumberFormat = "0.00_);(0.00)"
            .Cells(33 + NoOfRowsInserted, 9).NumberFormat = "0.00_);(0.00)"
            .Cells(34 + NoOfRowsInserted, 9).NumberFormat = "0.00_);(0.00)"
            .Cells(38 + NoOfRowsInserted, 6).NumberFormat = "0.00_);(0.00)"
            .Cells(39 + NoOfRowsInserted, 11).NumberFormat = "0.00_);(0.00)"
            TmpCellAdd = 39 + NoOfRowsInserted

            '--------------------------------
            'other income...
            Dim oRowStartsFrom As Integer
            Orow = 43 + NoOfRowsInserted
            oRowStartsFrom = Orow
            'rst.Filter = "TypeOfDetail='O'"
            'rst.MoveFirst
            For i = 0 To rst.Tables(0).Rows.Count - 1
                If Orow > (43 + NoOfRowsInserted) Then
                    .Rows(Orow).Insert
                    NoOfRowsInserted = NoOfRowsInserted + 1
                    .Range(.Cells(43 + NoOfRowsInserted, 1), .Cells(43 + NoOfRowsInserted, 3)).Merge
                    .Range(.Cells(43 + NoOfRowsInserted, 4), .Cells(43 + NoOfRowsInserted, 5)).Merge
                    .Range(.Cells(43 + NoOfRowsInserted, 6), .Cells(43 + NoOfRowsInserted, 8)).Merge
                    .Range(.Cells(43 + NoOfRowsInserted, 9), .Cells(43 + NoOfRowsInserted, 10)).Merge
                    .Range(.Cells(43 + NoOfRowsInserted, 11), .Cells(43 + NoOfRowsInserted, 12)).Merge
                    .Range(.Cells(43 + NoOfRowsInserted, 1), .Cells(43 + NoOfRowsInserted, 3)).BorderAround()
                    .Range(.Cells(43 + NoOfRowsInserted, 4), .Cells(43 + NoOfRowsInserted, 5)).BorderAround()
                    '                     .Range(.Cells(43 + NoOfRowsInserted, 6), .Cells(43 + NoOfRowsInserted, 8)).BorderAround ()
                    '                     .Range(.Cells(43 + NoOfRowsInserted, 9), .Cells(43 + NoOfRowsInserted, 10)).BorderAround ()
                    '                     .Range(.Cells(43 + NoOfRowsInserted, 11), .Cells(43 + NoOfRowsInserted, 12)).BorderAround ()
                    '.Cells(Orow, 11) = "=SUM(D43:D" & Orow & ")"
                    '.Cells(Orow, 11) = "=SUM(D" & 43 + NoOfRowsInserted & ":D" & Orow - 1 & ")"
                End If
                .Cells(Orow, 1) = rst.Tables(0).Rows(0)("Particulars").ToString()
                .Cells(Orow, 4) = rst.Tables(0).Rows(0)("GrossAmt").ToString()
                .Cells(Orow, 4).NumberFormat = "0.00_);(0.00)"
                Orow = Orow + 1
            Next

            If Orow < (44 + NoOfRowsInserted) Then
                .Cells(43 + NoOfRowsInserted, 11) = "=SUM(D" & oRowStartsFrom & ":D" & Orow - 1 & ")"
                .Cells(44 + NoOfRowsInserted, 11) = "=SUM(K" & 43 + NoOfRowsInserted & "+K" & Orow - 1 & ")"
                .Cells(43 + NoOfRowsInserted, 11).NumberFormat = "0.00_);(0.00)"
                .Cells(44 + NoOfRowsInserted, 11).NumberFormat = "0.00_);(0.00)"
            Else
                .Cells(Orow - 1, 11) = "=SUM(D" & oRowStartsFrom & ":D" & Orow - 1 & ")"
                .Cells(Orow, 11) = "=K" & TmpCellAdd & "+K" & Orow - 1
                .Cells(Orow - 1, 11).NumberFormat = "0.00_);(0.00)"
                .Cells(Orow, 11).NumberFormat = "0.00_);(0.00)"
                TmpCellAdd = Orow
                AddOfGrossTotInc = Orow
            End If

            '80c details...
            Dim ERowStartsFrom As Long
            Erow = 48 + NoOfRowsInserted
            ERowStartsFrom = Erow
            'rst.Filter = "TypeOfDetail='E'"
            'rst.MoveFirst
            For i = 0 To rst.Tables(0).Rows.Count - 1
                If Erow > (58 + NoOfRowsInserted) Then
                    .Rows(Erow).Insert
                    NoOfRowsInserted = NoOfRowsInserted + 1
                    .Range(.Cells(58 + NoOfRowsInserted, 1), .Cells(58 + NoOfRowsInserted, 5)).Merge()
                    .Range(.Cells(58 + NoOfRowsInserted, 6), .Cells(58 + NoOfRowsInserted, 8)).Merge()
                    .Range(.Cells(58 + NoOfRowsInserted, 9), .Cells(58 + NoOfRowsInserted, 10)).Merge()
                    .Range(.Cells(58 + NoOfRowsInserted, 11), .Cells(58 + NoOfRowsInserted, 12)).Merge()
                End If
                .Cells(Erow, 1) = rst.Tables(0).Rows(0)("Particulars").ToString()
                .Cells(Erow, 6) = rst.Tables(0).Rows(0)("GrossAmt").ToString()
                .Cells(Erow, 6).NumberFormat = "0.00_);(0.00)"
                .Cells(Erow, 9) = rst.Tables(0).Rows(0)("DeductibleAmt").ToString()
                .Cells(Erow, 9).NumberFormat = "0.00_);(0.00)"
                Erow = Erow + 1
            Next

            If Erow < (58 + NoOfRowsInserted) Then
                .Cells(58 + NoOfRowsInserted, 6) = "=SUM(F" & ERowStartsFrom & ":F" & (57 + NoOfRowsInserted) & ")"
                .Cells(58 + NoOfRowsInserted, 9) = "=SUM(I" & ERowStartsFrom & ":I" & (57 + NoOfRowsInserted) & ")"
                .Cells(58 + NoOfRowsInserted, 11) = "=SUM(I" & ERowStartsFrom & ":I" & (57 + NoOfRowsInserted) & ")"
                .Cells(58 + NoOfRowsInserted, 6).NumberFormat = "0.00_);(0.00)"
                .Cells(58 + NoOfRowsInserted, 9).NumberFormat = "0.00_);(0.00)"
                .Cells(58 + NoOfRowsInserted, 11).NumberFormat = "0.00_);(0.00)"
            Else
                .Cells(Erow, 6) = "=SUM(F" & ERowStartsFrom & ":F" & Erow - 1 & ")"
                .Cells(Erow, 9) = "=SUM(I" & ERowStartsFrom & ":I" & Erow - 1 & ")"
                .Cells(Erow, 11) = "=SUM(I" & ERowStartsFrom & ":I" & Erow - 1 & ")"
                .Cells(Erow, 6).NumberFormat = "0.00_);(0.00)"
                .Cells(Erow, 9).NumberFormat = "0.00_);(0.00)"
                .Cells(Erow, 11).NumberFormat = "0.00_);(0.00)"
                TmpCellAdd = Erow
            End If
            'chap 6a details...
            Dim VRowStartsFrom As Long
            Vrow = 63 + NoOfRowsInserted
            VRowStartsFrom = Vrow
            ' rst.Filter = "TypeOfDetail='V'"
            'rst.MoveFirst
            For i = 0 To rst.Tables(0).Rows.Count - 1
                If Vrow > (68 + NoOfRowsInserted) Then
                    .Rows(Vrow).Insert
                    NoOfRowsInserted = NoOfRowsInserted + 1
                End If
                .Cells(Vrow, 1) = rst.Tables(0).Rows(0)("Particulars").ToString()
                .Cells(Vrow, 4) = rst.Tables(0).Rows(0)("GrossAmt").ToString()
                .Cells(Vrow, 6) = rst.Tables(0).Rows(0)("QualifyAmt").ToString()
                .Cells(Vrow, 9) = rst.Tables(0).Rows(0)("DeductibleAmt").ToString()
                .Cells(Vrow, 4).NumberFormat = "0.00_);(0.00)"
                .Cells(Vrow, 6).NumberFormat = "0.00_);(0.00)"
                .Cells(Vrow, 9).NumberFormat = "0.00_);(0.00)"
                Vrow = Vrow + 1
            Next

            If Vrow < (68 + NoOfRowsInserted) Then
                .Cells(68 + NoOfRowsInserted, 4) = "=SUM(D" & VRowStartsFrom & ":D" & (67 + NoOfRowsInserted) & ")"
                .Cells(68 + NoOfRowsInserted, 6) = "=SUM(F" & VRowStartsFrom & ":F" & (67 + NoOfRowsInserted) & ")"
                .Cells(68 + NoOfRowsInserted, 9) = "=SUM(I" & VRowStartsFrom & ":I" & (67 + NoOfRowsInserted) & ")"
                .Cells(68 + NoOfRowsInserted, 11) = "=SUM(I" & VRowStartsFrom & ":I" & (67 + NoOfRowsInserted) & ")"
                .Cells(68 + NoOfRowsInserted, 4).NumberFormat = "0.00_);(0.00)"
                .Cells(68 + NoOfRowsInserted, 6).NumberFormat = "0.00_);(0.00)"
                .Cells(68 + NoOfRowsInserted, 9).NumberFormat = "0.00_);(0.00)"
                .Cells(68 + NoOfRowsInserted, 11).NumberFormat = "0.00_);(0.00)"

            Else
                .Cells(Vrow, 4) = "=SUM(D" & VRowStartsFrom & ":D" & Vrow - 1 & ")"
                .Cells(Vrow, 6) = "=SUM(E" & VRowStartsFrom & ":E" & Vrow - 1 & ")"
                .Cells(Vrow, 9) = "=SUM(H" & VRowStartsFrom & ":H" & Vrow - 1 & ")"
                .Cells(Vrow, 11) = "=SUM(H" & VRowStartsFrom & ":H" & Vrow - 1 & ")"
                .Cells(Vrow, 4).NumberFormat = "0.00_);(0.00)"
                .Cells(Vrow, 6).NumberFormat = "0.00_);(0.00)"
                .Cells(Vrow, 9).NumberFormat = "0.00_);(0.00)"
                .Cells(Vrow, 11).NumberFormat = "0.00_);(0.00)"
            End If
            .Cells(69 + NoOfRowsInserted, 11) = "=K" & 58 + NoOfRowsInserted & "+K" & 68 + NoOfRowsInserted
            'Total Income
            .Cells(70 + NoOfRowsInserted, 11) = "=K" & AddOfGrossTotInc & "-K" & 69 + NoOfRowsInserted
            .Cells(69 + NoOfRowsInserted, 11).NumberFormat = "0.00_);(0.00)"
            .Cells(70 + NoOfRowsInserted, 11).NumberFormat = "0.00_);(0.00)"


            'now the challan details...
            Dim CRow As Long, TotalTDSFromChallan As Double, strSql As String
            'If rstTmp.State = adStateOpen Then
            rstTmp.Dispose

            'Changes By Prakash on 25.05.2009

            '             rstTmp.Open "SELECT D24.RetnID, D24.DId, D24.TaxAmt, D24.Surcharge, D24.ECess, C24.ChqDDNo, C24.BankBrCode," & _
            '                 " C24.DtOfChallan, C24.BankChallanNo FROM Challan24Q AS C24 INNER JOIN Deductee24Q AS D24 ON " & _
            '                 " C24.ChallanID = D24.ChallanId WHERE (((D24.DId)=" & _
            '                 TmpDID & "));", cnn, adOpenKeyset, adLockReadOnly

            strSql = "SELECT D24.RetnID, D24.DId, D24.TaxAmt, D24.Surcharge, D24.ECess, C24.ChqDDNo, C24.BankBrCode," &
                 " C24.DtOfChallan, C24.BankChallanNo FROM Challan24Q AS C24 INNER JOIN Deductee24Q AS D24 ON " &
                 " C24.ChallanID = D24.ChallanId WHERE (((D24.DId)=" & TmpDID & ")) " &
                 " Union All select 0 as expr1, 0 as expr2, F.TaxAmt, F.Surcharge, F.ECess, F.ChqDDNo, " &
                 " F.BankBrCode, F.DtOfChallan, F.BankChallanNo FROM F16Challan as F where F.F16ID=" & o16A.F16ID

            rstTmp = FetchDataSet(strSql)

            Dim cRowStartsFrom As Long, SrNoCtr As Long
            Dim mCol As Integer
            CRow = 86 + NoOfRowsInserted
            Addof_TaxDeductedAtSourceUs192_2 = CRow
            cRowStartsFrom = CRow
            TotalTDSFromChallan = 0 : SrNoCtr = 1
            For i = 0 To rstTmp.Tables(0).Rows.Count - 1
                If CRow > (97 + NoOfRowsInserted) Then
                    .Rows(CRow).Insert
                    .Range(.Cells(CRow, 7), .Cells(CRow, 8)).HorizontalAlignment = HorizontalAlignment.Right
                    .Range(.Cells(CRow, 9), .Cells(CRow, 10)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(CRow, 11), .Cells(CRow, 12)).HorizontalAlignment = HorizontalAlignment.Right
                    .Range(.Cells(CRow, 7), .Cells(CRow, 8)).Merge
                    .Range(.Cells(CRow, 9), .Cells(CRow, 10)).Merge
                    .Range(.Cells(CRow, 11), .Cells(CRow, 12)).Merge
                    NoOfRowsInserted = NoOfRowsInserted + 1
                End If
                .Cells(CRow, 1) = SrNoCtr
                .Cells(CRow, 2) = rstTmp.Tables(0).Rows(0)("TaxAmt").ToString()
                .Cells(CRow, 3) = rstTmp.Tables(0).Rows(0)("Surcharge").ToString()
                .Cells(CRow, 4) = rstTmp.Tables(0).Rows(0)("ECess").ToString()
                'cell no 5 has totals
                .Cells(CRow, 6) = rstTmp.Tables(0).Rows(0)("ChqDDNo").ToString()
                .Cells(CRow, 7) = rstTmp.Tables(0).Rows(0)("BankBrCode").ToString()
                .Cells(CRow, 9) = Format(rstTmp.Tables(0).Rows(0)("DtOfChallan").ToString(), "dd/mmm/yy")
                .Cells(CRow, 11) = rstTmp.Tables(0).Rows(0)("BankChallanNo").ToString()
                .Cells(CRow, 5) = "=SUM(B" & CRow & ":D" & CRow & ")"
                .Cells(CRow, 2).NumberFormat = "0.00_);(0.00)"
                .Cells(CRow, 3).NumberFormat = "0.00_);(0.00)"
                .Cells(CRow, 4).NumberFormat = "0.00_);(0.00)"
                .Cells(CRow, 5).NumberFormat = "0.00_);(0.00)"
                CRow = CRow + 1
                SrNoCtr = SrNoCtr + 1
                'TotalTDSFromChallan = TotalTDSFromChallan + rstTmp!TaxAmt + rstTmp!Surcharge + rstTmp!ECess
            Next

            If CRow < (98 + NoOfRowsInserted) Then
                'Total Tax Deposited
                .Cells(98 + NoOfRowsInserted, 5) = "=SUM(E86" & ":E" & (97 + NoOfRowsInserted) & ")"
                'Total Tax Deposited in declaration part in figure
                .Cells(101 + NoOfRowsInserted, 4) = "=SUM(E86" & ":E" & (97 + NoOfRowsInserted) & ")"
                .Cells(101 + NoOfRowsInserted, 4) = Str(.Cells(101 + NoOfRowsInserted, 4)) & " /-"
                'Total Tax Deposited in declaration part in Words
                .Cells(101 + NoOfRowsInserted, 6) = SpellRupee(Val(.Cells(101 + NoOfRowsInserted, 4)))
                'Tax Deducted at source
                .Cells(Addof_TaxDeductedAtSourceUs192_2 - 8, 9) = "=SUM(E86" & ":E" & (97 + NoOfRowsInserted) & ")"

                .Cells(98 + NoOfRowsInserted, 5).NumberFormat = "0.00_);(0.00)"
                .Cells(Addof_TaxDeductedAtSourceUs192_2 - 8, 9).NumberFormat = "0.00_);(0.00)"
            Else
                'Total Tax Deposited
                .Cells(CRow, 5) = "=SUM(E" & cRowStartsFrom & ":E" & CRow - 1 & ")"
                'Total Tax Deposited in declaration part in figure
                .Cells(CRow + 3, 4) = "=SUM(E" & cRowStartsFrom & ":E" & CRow - 1 & ")"
                .Cells(CRow + 3, 4) = Str(.Cells(CRow + 3, 4)) & " /-"
                'Total Tax Deposited in declaration part in Words
                .Cells(CRow + 3, 6) = SpellRupee(Val(.Cells(CRow + 3, 4)))
                'Tax Deducted at source
                .Cells(Addof_TaxDeductedAtSourceUs192_2 - 8, 9) = "=SUM(E" & cRowStartsFrom & ":E" & CRow - 1 & ")"

                .Cells(CRow, 5).NumberFormat = "0.00_);(0.00)"
                .Cells(Addof_TaxDeductedAtSourceUs192_2 - 8, 9).NumberFormat = "0.00_);(0.00)"
            End If

            .Range(.Cells(85 + NoOfRowsInserted, 1), .Cells(97 + NoOfRowsInserted, 12)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 1), .Cells(97 + NoOfRowsInserted, 1)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 2), .Cells(97 + NoOfRowsInserted, 2)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 3), .Cells(97 + NoOfRowsInserted, 3)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 4), .Cells(97 + NoOfRowsInserted, 4)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 5), .Cells(97 + NoOfRowsInserted, 5)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 6), .Cells(97 + NoOfRowsInserted, 6)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 7), .Cells(97 + NoOfRowsInserted, 8)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 9), .Cells(97 + NoOfRowsInserted, 10)).BorderAround()
            .Range(.Cells(85 + NoOfRowsInserted, 11), .Cells(97 + NoOfRowsInserted, 12)).BorderAround()
            .Range(.Cells(2, 1), .Cells(110 + NoOfRowsInserted, 12)).BorderAround()

            '            'Page Setup
            '            .PageSetup.TopMargin = 1
            '            .PageSetup.BottomMargin = 1
            '            .PageSetup.LeftMargin = 2
            '            .PageSetup.RightMargin = 0.75
            '            .PageSetup.HeaderMargin = 0
            '            .PageSetup.FooterMargin = 0
            '            .PageSetup.LeftHeader = mDname
            '            .PageSetup.CenterFooter = "Page $" & .PageSetup.

        End With

    End Sub

End Class