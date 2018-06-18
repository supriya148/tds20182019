Imports System.ComponentModel
Imports Microsoft.Office.Interop

Public Class frmretsumm
    Public tablnm As String
    Private Sub frmretsumm_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        frmCoMst.EnterTab(e)
    End Sub

    Private Sub ComboBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbrepttyp.Leave
        cmbrepttyp.BackColor = Color.White
    End Sub

    Private Sub ComboBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbrepttyp.Enter
        cmbrepttyp.BackColor = Color.LightYellow
    End Sub

    Private Sub frmretsumm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Top = 1000
        ' Me.Left = 400
        cmbrepttyp.Items.Add("Select")
        cmbrepttyp.Items.Add("Company Detail")
        cmbrepttyp.Items.Add("Return Summary")
        cmbrepttyp.Items.Add("eTDS/TCS - Companywise Return Detail")
        cmbrepttyp.SelectedIndex = 0
        Fraretnsumm.Visible = False
        Fraretndetail.Visible = False
        fracompdetail.Visible = False
        grids.Visible = False
        grid.Visible = False
        gridb.Visible = False
        Label2.Visible = False
        grid.Columns.Clear()
        gridhead()
        gridheads()
        grid.ColumnCount = 4
        'grid.EditMode = False
    End Sub
    Public Sub gridhead()
        With grid
            .ColumnCount = 6
            .Columns(0).HeaderText() = "Order By"
            .Columns(1).HeaderText() = "Tables(0).columns"
            .Columns(2).HeaderText() = "Col.Name"
            .Columns(3).HeaderText() = "Changed Name"
            Dim chkCol As New DataGridViewCheckBoxColumn
            grid.Columns.Insert(4, chkCol)
            chkCol.HeaderText = "Select"
            '.Columns(4).HeaderText() = "Sel"
            .Columns(5).HeaderText() = "Address"
            .Columns(0).Width = 800
            .Columns(1).Visible = False
            .Columns(0).Visible = False
            .Columns(2).Width = 100
            .Columns(3).Width = 200
            .Columns(3).Visible = False
            .Columns(5).Visible = False
            .Columns(4).Width = 80
            ' .FixedCols = 0
            .Columns(0).Visible = False
        End With
    End Sub
    Private Sub gridorder()
        Dim i, maxnum As Integer
        maxnum = grid.Rows(grid.RowCount).Cells(0).Value
        grid.Rows(grid.RowCount).Cells(0).Value = ""
        For i = 1 To grid.Rows.Count - 2
            If grid.Rows(i).Cells(0).Value > maxnum Then
                grid.Rows(i).Cells(0).Value = grid.Rows(i).Cells(0).Value - 1
                ' gridr.Columns = gridr.ColumnCount - 1
            End If
        Next
    End Sub
    Public Sub gridheads()
        With grids
            Dim chk1, chk2, chk3, chk4 As New DataGridViewCheckBoxColumn
            .ColumnCount = 26
            .Columns(0).HeaderText() = "Company Name"
            .Columns(1).HeaderText() = "TAN"
            .Columns(2).HeaderText() = "PAN"
            .Columns(3).HeaderText() = "Status"
            .Columns.Insert(4, chk1)
            chk1.HeaderText() = "24Q"
            .Columns.Insert(5, chk2)
            chk2.HeaderText() = "26Q"
            .Columns.Insert(6, chk3)
            chk3.HeaderText() = "27Q"
            .Columns.Insert(7, chk4)
            chk4.HeaderText() = "27EQ"
            .Columns(8).HeaderText() = "Sel"
            .Columns(9).HeaderText() = "CoId"
            .Columns(10).HeaderText() = "24Q1"
            .Columns(11).HeaderText() = "24Q2"
            .Columns(12).HeaderText() = "24Q3"
            .Columns(13).HeaderText() = "24Q4"
            .Columns(14).HeaderText() = "26Q1"
            .Columns(15).HeaderText() = "26Q2"
            .Columns(16).HeaderText() = "26Q3"
            .Columns(17).HeaderText() = "26Q4"
            .Columns(18).HeaderText() = "27EQ1"
            .Columns(19).HeaderText() = "27EQ2"
            .Columns(20).HeaderText() = "27EQ3"
            .Columns(21).HeaderText() = "27EQ4"
            .Columns(22).HeaderText() = "27Q1"
            .Columns(23).HeaderText() = "27Q2"
            .Columns(24).HeaderText() = "27Q3"
            .Columns(25).HeaderText() = "27Q4"
            .Columns(0).Width = 150
            .Columns(3).Width = 80
            .Columns(4).Width = 50
            .Columns(5).Width = 50
            .Columns(6).Width = 50
            .Columns(7).Width = 50
            ' .FixedCols = 0
            .Columns(8).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .Columns(22).Visible = False
            .Columns(23).Visible = False
            .Columns(24).Visible = False
            .Columns(25).Visible = False
        End With
    End Sub

    Private Sub frmretsumm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Timer1.Enabled = False
        Timer2.Enabled = False
    End Sub

    Private Sub grid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grid.KeyPress
        'If eKeyAscii = 32 Then KeyAscii = 0
        If grid.ColumnCount = 3 And Asc(e.KeyChar) >= 32 Then
            'KeyAscii = 0
            e.KeyChar = ""
        End If
    End Sub

    Private Function grdord() As Integer
        grdord = grid.Rows(1).Cells(0).Value
        For i = 1 To grid.RowCount - 1
            If IIf(String.IsNullOrEmpty(grid.Rows(i).Cells(0).Value), 0, grid.Rows(i).Cells(0).Value) > grdord Then
                grdord = grid.Rows(i).Cells(0).Value
            End If
        Next
    End Function

    Private Sub gridb_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles gridb.CellEndEdit

        If gridb.ColumnCount = 3 Then
            If gridb.Rows(0).Cells(1).Value.Checked = False Then
                For R = 1 To gridb.RowCount - 1
                    gridb.Rows(R).Cells(8).Value = ""
                    gridb.Rows(R).Cells(9).Value = ""
                    gridb.Rows(R).Cells(14).Value = ""
                    gridb.Rows(R).Cells(15).Value = ""
                    gridb.Rows(R).Cells(16).Value = ""
                Next
            End If
        End If
        If gridb.ColumnCount = 4 Then
            If gridb.Rows(0).Cells(1).Value.Checked = False Then
                For R = 1 To gridb.RowCount - 1
                    gridb.Rows(R).Cells(10).Value = ""
                    gridb.Rows(R).Cells(11).Value = ""
                    gridb.Rows(R).Cells(17).Value = ""
                    gridb.Rows(R).Cells(18).Value = ""
                    gridb.Rows(R).Cells(19).Value = ""
                Next
            End If
        End If
        If gridb.ColumnCount = 5 Then
            If gridb.Rows(0).Cells(1).Value.Checked = False Then
                For R = 1 To gridb.RowCount - 1
                    gridb.Rows(R).Cells(12).Value = ""
                    gridb.Rows(R).Cells(13).Value = ""
                    gridb.Rows(R).Cells(20).Value = ""
                    gridb.Rows(R).Cells(21).Value = ""
                    gridb.Rows(R).Cells(22).Value = ""
                Next
            End If
        End If
    End Sub

    Private Sub grid_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grid.CellEndEdit
        If grid.ColumnCount = 4 Then
            If grid.Rows(0).Cells(1).Value.Checked = True Then
                grid.Rows(grid.RowCount).Cells(0).Value = grdord() + 1
            ElseIf grid.Rows(0).Cells(1).Value.Checked = False Then
                gridorder()
            End If
        End If
    End Sub

    Private Sub gridb_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles gridb.CellEnter
        With gridb
            If .ColumnCount = 0 Then .ColumnCount = 3
            If .ColumnCount = 1 Then .ColumnCount = 3
            If .ColumnCount = 2 Then .ColumnCount = 3
        End With
    End Sub

    Private Sub grids_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grids.CellEndEdit
        If grids.ColumnCount = 4 Then
            If grids.Rows(0).Cells(1).Value.Checked = False Then
                For R = 1 To grids.Rows.Count - 1
                    gridb.Rows(R).Cells(10).Value = ""
                    gridb.Rows(R).Cells(11).Value = ""
                    gridb.Rows(R).Cells(12).Value = ""
                    gridb.Rows(R).Cells(13).Value = ""
                Next
            End If
        End If
        If grids.ColumnCount = 5 Then
            If grids.Rows(0).Cells(1).Value.Checked = False Then
                For R = 1 To grids.Rows.Count - 1
                    gridb.Rows(R).Cells(14).Value = ""
                    gridb.Rows(R).Cells(15).Value = ""
                    gridb.Rows(R).Cells(16).Value = ""
                    gridb.Rows(R).Cells(17).Value = ""
                Next
            End If
        End If
        If grids.ColumnCount = 7 Then
            If grids.Rows(0).Cells(1).Value.Checked = False Then
                For R = 1 To grids.Rows.Count - 1
                    gridb.Rows(R).Cells(18).Value = ""
                    gridb.Rows(R).Cells(19).Value = ""
                    gridb.Rows(R).Cells(20).Value = ""
                    gridb.Rows(R).Cells(21).Value = ""
                Next
            End If
        End If
    End Sub

    Private Sub grids_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles grids.CellEnter
        With grids
            If .ColumnCount = 2 Then .ColumnCount = 4
            If .ColumnCount = 0 Then .ColumnCount = 4
            If .ColumnCount = 1 Then .ColumnCount = 4
            If .ColumnCount = 3 Then .ColumnCount = 4
        End With
    End Sub

    Private Sub grids_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grids.KeyPress
        If grids.ColumnCount = 3 And Asc(e.KeyChar) >= 32 Then
            'KeyAscii = 0
            e.KeyChar = ""
        End If
    End Sub

    Private Function grsdord() As Integer
        grsdord = grids.Rows(1).Cells(0).Value
        For i = 1 To grids.Rows.Count - 1
            If IIf(IsDBNull(grids.Rows(i).Cells(0).Value), 0, (grids.Rows(i).Cells(0).Value)) > grsdord Then
                grsdord = grids.Rows(i).Cells(0).Value
            End If
        Next
    End Function
    Private Sub gridsorder()
        Dim i, maxnum As Integer
        maxnum = grids.Rows(grids.RowCount).Cells(0).Value
        grids.Rows(grid.RowCount).Cells(0).Value = ""
        For i = 1 To grid.Rows.Count - 2
            If grids.Rows(i).Cells(0).Value > maxnum Then
                grids.Rows(i).Cells(0).Value = grids.Rows(i).Cells(0).Value - 1
                'gridr.Columns = gridr.Columns.Count - 1
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.ForeColor = Color.Red
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label2.ForeColor = Color.Blue
    End Sub

    Private Sub fillgrids()
        Dim tbnm As String
        Dim rs As New DataSet
        Dim sql As String
        Dim R As Integer
        For R = 0 To grids.Rows.Count - 1
            If grids.Rows(R).Cells(4).Value = CheckState.Checked Then
                tbnm = ""
                If grids.Columns(10).HeaderText = "24Q1" Then
                    sql = "SELECT Challan24Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan24Q ON RetnMst.RetnID =Challan24Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(10).HeaderText & "' "
                End If
                rs = FetchDataSet(sql)
                tbnm = rs.Tables(0).Rows.Count
                If tbnm > 0 Then
                    tbnm = "Y"
                Else
                    tbnm = "N"
                End If
                grids.Rows(R).Cells(10).Value = tbnm
                rs.Dispose()

                If grids.Columns(11).HeaderText = "24Q2" Then
                    tbnm = ""
                    sql = "SELECT Challan24Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan24Q ON RetnMst.RetnID =Challan24Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(11).HeaderText & "' "
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(11).Value = tbnm
                    rs.Dispose()
                End If

                If grids.Columns(12).HeaderText = "24Q3" Then
                    tbnm = ""
                    sql = "SELECT Challan24Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan24Q ON RetnMst.RetnID =Challan24Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(12).HeaderText & "' "
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(12).Value = tbnm
                    rs.Dispose()
                End If

                If grids.Columns(13).HeaderText = "24Q4" Then
                    tbnm = ""
                    sql = "SELECT Challan24Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan24Q ON RetnMst.RetnID =Challan24Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(13).HeaderText & "' "
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(13).Value = tbnm
                    rs.Dispose()
                End If
            End If
            If grids.Rows(R).Cells(5).Value = CheckState.Checked Then
                tbnm = ""
                If grids.Columns(14).HeaderText = "26Q1" Then
                    sql = "SELECT Challan26Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan26Q ON RetnMst.RetnID =Challan26Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(14).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(14).Value = tbnm
                    rs.Dispose()
                End If
                If grids.Columns(15).HeaderText = "26Q2" Then
                    tbnm = ""
                    sql = "SELECT Challan26Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan26Q ON RetnMst.RetnID =Challan26Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(15).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(15).Value = tbnm
                    rs.Dispose()
                End If
                If grids.Columns(16).HeaderText = "26Q3" Then
                    tbnm = ""
                    sql = "SELECT Challan26Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan26Q ON RetnMst.RetnID =Challan26Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(16).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(16).Value = tbnm
                    rs.Dispose()
                End If
                If grids.Columns(17).HeaderText = "26Q4" Then
                    tbnm = ""
                    sql = "SELECT Challan26Q.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan26Q ON RetnMst.RetnID =Challan26Q.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(17).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(17).Value = tbnm
                    rs.Dispose()
                End If
            End If
            If grids.Rows(R).Cells(7).Value = CheckState.Checked Then
                tbnm = ""
                If grids.Columns(18).HeaderText = "27EQ1" Then
                    sql = "SELECT Challan27EQ.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan27EQ ON RetnMst.RetnID =Challan27EQ.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(18).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(18).Value = tbnm
                    rs.Dispose()
                End If
                tbnm = ""
                If grids.Columns(19).HeaderText = "27EQ2" Then
                    sql = "SELECT Challan27EQ.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan27EQ ON RetnMst.RetnID =Challan27EQ.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(19).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(19).Value = tbnm
                    rs.Dispose()
                End If
                tbnm = ""
                If grids.Columns(20).HeaderText = "27EQ3" Then
                    sql = "SELECT Challan27EQ.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan27EQ ON RetnMst.RetnID =Challan27EQ.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(20).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(20).Value = tbnm
                    rs.Dispose()
                End If
                tbnm = ""
                If grids.Columns(21).HeaderText = "27EQ4" Then
                    sql = "SELECT Challan27EQ.*  FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID)" _
                    & " INNER JOIN Challan27EQ ON RetnMst.RetnID =Challan27EQ.RetnID" _
                    & " Where Comst.Coid = " & grids.Rows(R).Cells(9).Value & " and RetnMst.FrmType='" & grids.Columns(21).HeaderText & "'"
                    rs = FetchDataSet(sql)
                    tbnm = rs.Tables(0).Rows.Count
                    If tbnm > 0 Then
                        tbnm = "Y"
                    Else
                        tbnm = "N"
                    End If
                    grids.Rows(R).Cells(21).Value = tbnm
                    rs.Dispose()
                End If
            End If
            '        If grids.Cell(flexcpChecked, r, 5) = 1 Then
            '            tbnm = "Table27EQ"
            '        End If

        Next
    End Sub

    Public Sub gridheadb()
        With gridb
            Dim chk1, chk2, chk3, chk4 As New DataGridViewCheckBoxColumn
            .ColumnCount = 26
            '.RowCount = 2
            .Columns(0).HeaderText() = "Company Name"
            .Columns(1).HeaderText() = "TAN"
            .Columns(2).HeaderText() = "PAN"
            .Columns.Insert(3, chk1)
            chk1.HeaderText() = "24Q"
            .Columns.Insert(4, chk2)
            chk2.HeaderText() = "26Q"
            .Columns.Insert(5, chk3)
            'chk3.HeaderText = "27Q"
            ' .Columns.Insert(6, chk4)
            chk3.HeaderText() = "27EQ"
            .Columns(6).HeaderText() = "CoId"
            .Columns(7).HeaderText() = "Quter"
            .Columns(8).HeaderText() = "24challanNo"
            .Columns(9).HeaderText() = "24challanAmt"
            .Columns(10).HeaderText() = "26challanNo"
            .Columns(11).HeaderText() = "26challanAmt"
            .Columns(12).HeaderText() = "27EchallanNo"
            .Columns(13).HeaderText() = "27EchallanAmt"
            .Columns(14).HeaderText() = "24count"
            .Columns(15).HeaderText() = "24depo"
            .Columns(16).HeaderText() = "24deduct"
            .Columns(17).HeaderText() = "26count"
            .Columns(18).HeaderText() = "26depo"
            .Columns(19).HeaderText() = "26deduct"
            .Columns(20).HeaderText() = "27Ecount"
            .Columns(21).HeaderText() = "27Edepo"
            .Columns(22).HeaderText() = "27Ededuct"
            .Columns(23).HeaderText() = "Status"
            .Columns(0).Width = 100
            .Columns(1).Width = 100
            .Columns(2).Width = 50
            .Columns(4).Width = 50
            .Columns(5).Width = 50
            ' .FixedCols = 0
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
            .Columns(21).Visible = False
            .Columns(22).Visible = False

        End With
    End Sub

    Private Sub cmbrepttyp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbrepttyp.SelectedIndexChanged
        Dim rs, rs1 As New DataSet

        Dim com As String
        Dim sql As String
        Dim i, m, c As Integer
        grid.Visible = True
        grids.Visible = False
        Fraretnsumm.Visible = False
        Fraretndetail.Visible = False
        Label2.Visible = True
        gridb.Visible = False
        ' grid.Dispose()
        gridhead()
        grid.Enabled = True
        If cmbrepttyp.SelectedIndex = 0 Then
            Label2.Visible = False
            grid.Visible = False
            fracompdetail.Visible = False
        End If
        If cmbrepttyp.SelectedIndex = 1 Then
            fracompdetail.Visible = True
            sql = " SELECT CoMst.CoName AS CompanyName, CoMst.CoBrDiv AS Branch, CoMst.CoAdd1 & CoMst.CoAdd2 & CoMst.CoAdd3 & CoMst.CoAdd4 & CoMst.CoAdd5 & ' '& StateMst.StateName & ' - ' &CoMst.CoPin AS Address," _
    & " CoMst.CoStd & ' ' & CoMst.CoPhone as Phone  , CoMst.CoEmail as Email, CoMst.CoTAN as TAN, CoMst.CoPAN as PAN, CoMst.CoStatus as Status, CoMst.PR24Name & ' - ' &CoMst.PR24Desg as ContectPerson24, CoMst.PR24Add1 & CoMst.PR24Add2 & CoMst.PR24Add3 & CoMst.PR24Add4 & CoMst.PR24Add5 & ' ' & StateMst.StateName & ' - ' & CoMst.PR24Pin AS 24Address," _
    & " CoMst.PR24Std & '   ' & CoMst.PR24Phone as 24Phone, CoMst.PR24Email as 24Email,  CoMst.PR26Name & ' - ' & CoMst.PR26Desg as ContectPerson26, CoMst.PR26Add1 & CoMst.PR26Add2 & CoMst.PR26Add4 & CoMst.PR26Add5  & ' ' & StateMst.StateName & ' - ' & CoMst.PR26Pin AS 26Address, CoMst.PR26Std & '   ' & CoMst.PR26Phone as 26Phone, CoMst.PR26Email as 26Email," _
    & " CoMst.PR27Name & ' - ' & CoMst.PR27Desg as ContectPerson27, CoMst.PR27Add1 & CoMst.PR27Add2 & CoMst.PR27Add3 & CoMst.PR27Add4 & CoMst.PR27Add5 & ' '  & StateMst.StateName & ' - ' & CoMst.PR27Pin as  27Address," _
    & " CoMst.PR27Std & '   ' & CoMst.PR27Phone as 27Phone,  CoMst.PR27Email as 27Email,CoMst.PR27EName &' - ' & CoMst.PR27EDesg as ContectPerson27E, CoMst.PR27EAdd1 & CoMst.PR27EAdd2 & CoMst.PR27EAdd3 & CoMst.PR27EAdd4 & CoMst.PR27EAdd5 & ' ' & StateMst.StateName & ' - ' & CoMst.PR27EPin AS 27EAddress, CoMst.PR27EStd & '    ' & CoMst.PR27EPhone as 27EPhone ,CoMst.PR27EEmail as 27EEmail,COMST.mobile as Mobile  FROM CoMst INNER JOIN StateMst ON (CoMst.CoStateID = StateMst.StateID) AND (CoMst.PR24StateID = StateMst.StateID) AND (CoMst.PR27StateID = StateMst.StateID) AND (CoMst.PR27EStateID = StateMst.StateID)"
            rs = FetchDataSet(sql)

            grid.RowCount = rs.Tables(0).Columns.Count + 1
            If rs.Tables(0).Rows.Count > 0 Then
                For i = 0 To rs.Tables(0).Columns.Count - 1
                    grid.Rows(i).Cells(2).Value = rs.Tables(0).Columns(i).ColumnName
                    grid.Rows(i).Cells(3).Value = CheckState.Checked
                    'grid.Rows(i).Cells(4).Value = CheckState.Checked
                    grid.Rows(0).Cells(4).Value = CheckState.Checked
                    grid.Rows(2).Cells(4).Value = CheckState.Checked
                    grid.Rows(5).Cells(4).Value = CheckState.Checked
                    grid.Rows(3).Cells(4).Value = CheckState.Checked
                    grid.Rows(12).Cells(4).Value = CheckState.Checked
                    grid.Rows(7).Cells(4).Value = CheckState.Checked
                    grid.Rows(8).Cells(4).Value = CheckState.Checked
                    grid.Rows(20).Cells(4).Value = CheckState.Checked
                    grid.Rows(6).Cells(4).Value = CheckState.Checked
                    grid.Rows(16).Cells(4).Value = CheckState.Checked

                    'If grid.Rows(i + 1).Cells(4).Value.Checked = True Then
                    'End If
                Next i
                ' rs.MoveFirst
            End If
        ElseIf cmbrepttyp.SelectedIndex = 2 Then
            grid.Rows.Clear()
            'grid.EditMode = False
            grid.Visible = False
            grids.Visible = True
            fracompdetail.Visible = False
            Fraretnsumm.Visible = True
            gridb.Visible = False
            For i = 0 To grid.RowCount - 1
                grid.Rows(i).Cells(4).Value = CheckState.Checked
            Next i
            sql = "SELECT CoMst.CoID, CoMst.CoName From CoMst"
            rs1 = FetchDataSet(sql)
            If Not rs1.Tables(0).Rows.Count Then
                CmbCoName.Items.Clear()
                CmbCoName.Items.Add("Select")
                CmbCoName.Items.Add("All")
                For i = 0 To rs1.Tables(0).Rows.Count - 1
                    CmbCoName.Items.Add(rs1.Tables(0).Rows(i)("CoName").ToString())
                    CmbCoName.SelectedIndex = rs1.Tables(0).Rows(i)("coid").ToString()
                Next

            End If
            If rs1.Tables(0).Rows.Count = False Then
                CmbCoName.SelectedIndex = 0
            Else
                CmbCoName.SelectedIndex = -1
            End If

        ElseIf cmbrepttyp.SelectedIndex = 3 Then
            grid.Visible = False
            grids.Visible = False
            gridb.Visible = True
            'gridb.EditMode = False
            Fraretnsumm.Visible = False
            fracompdetail.Visible = False
            Fraretndetail.Visible = True
            sql = "SELECT CoMst.CoID, CoMst.CoName From CoMst"
            rs1 = FetchDataSet(sql)
            If Not rs1.Tables(0).Rows.Count Then
                cmbCNM.Items.Clear()
                cmbCNM.Items.Add("Select")
                cmbCNM.Items.Add("All")
                For i = 0 To rs1.Tables(0).Rows.Count - 1
                    cmbCNM.Items.Add(rs1.Tables(0).Rows(i)("CoName").ToString())
                    ' cmbCNM.SelectedIndex = rs1.Tables(0).Rows(i)("coid")
                Next
            End If
            If rs1.Tables(0).Rows.Count = False Then
                cmbCNM.SelectedIndex = 0
            Else
                cmbCNM.SelectedIndex = -1
            End If

            'MsgBox "hi"
        End If
        rs.Dispose()
        rs = Nothing


    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        grid.Dispose()
        gridhead()

        For i = 1 To grid.Rows.Count - 1
            grid.Rows(i).Cells(4).Value.Checked = False
        Next
        cmbrepttyp.SelectedIndex = 0
        grid.EditMode = False

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdReport_Click(sender As Object, e1 As EventArgs) Handles cmdReport.Click

        Dim rs As New DataSet
        Dim R As Integer, m, N, e, c, d, a, i, F As Integer, icolm As Integer, ocolm As Integer, hcolm As Integer
        Dim xlapp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        xlapp = Nothing
        xlBook = Nothing
        xlSheet = Nothing
        xlapp = New Excel.Application
        xlBook = xlapp.Workbooks.Add
        xlSheet = xlBook.Worksheets("sheet1")
        xlapp.Visible = True
        icolm = 7 : ocolm = 9 : hcolm = 1
        If cmbrepttyp.SelectedIndex = 0 Then
            MsgBox(" Select Report Type First")
            Exit Sub
        End If
        With xlSheet
            If cmbrepttyp.SelectedIndex = 1 Then
                m = 2 : i = 0
                .Range(.Cells(m, 1), .Cells(m, 10)).Merge() : .Cells(m, 1) = cmbrepttyp.Text
                .Range(.Cells(m, 1), .Cells(m, 10)).Font.Bold = True : .Range(.Cells(m, 1), .Cells(m, 10)).HorizontalAlignment = HorizontalAlignment.Center
                .Range(.Cells(m, 1), .Cells(m, 10)).Font.Size = 15 : .Range(.Cells(m, 1), .Cells(m, 10)).Font.Underline = True : .Range(.Cells(m, 1), .Cells(m, 10)).Font.Color = Color.Red
                For R = 0 To grid.Rows.Count - 1
                    If grid.Rows(R).Cells(4).Value = CheckState.Checked Then
                        ok()

                        xlSheet.Cells(m + 1, N + 1) = "Sr.No"
                        .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).Font.Bold = True
                        .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).BorderAround()
                        .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).Merge()
                        '.Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).VerticalAlignment = xlTop

                        xlSheet.Cells(m + 1, N + 2) = grid.Rows(R).Cells(2).Value
                        .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 3)).Merge()
                        .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 3)).BorderAround()
                        .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 3)).Font.Bold = True : .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 3)).HorizontalAlignment = HorizontalAlignment.Center : .Range(.Cells(m + 1, N + 3), .Cells(m + 1, N + 3)).Font.Size = 10
                        .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 3)).WrapText = True
                        ' .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 3)).VerticalAlignment = xlTop

                        N = N + 2
                    End If
                Next

                For t = 0 To gridr.Rows.Count - 2

                    xlSheet.Cells(d + 4, i + 1) = a + 1
                    .Range(.Cells(d + 4, i + 1), .Cells(d + 4, i + 1)).WrapText = True
                    '.Range(.Cells(d + 4, i + 1), .Cells(d + 4, i + 1)).VerticalAlignment = xlTop
                    .Range(.Cells(d + 4, i + 1), .Cells(d + 4, i + 1)).HorizontalAlignment = HorizontalAlignment.Center
                    xlSheet.Cells(d + 4, i + 2) = gridr.Rows(t).Cells(c).Value
                    .Range(.Cells(d + 4, i + 2), .Cells(d + 4, i + 3)).Merge()
                    .Range(.Cells(d + 4, i + 2), .Cells(d + 4, i + 3)).BorderAround()
                    .Range(.Cells(d + 4, i + 2), .Cells(d + 4, i + 2)).WrapText = True
                    ' .Range(.Cells(d + 4, i + 2), .Cells(d + 4, i + 2)).VerticalAlignment = xlTop


                    d = d + 1
                    a = a + 1
                Next t
                For c = 1 To gridr.ColumnCount - 1

                    For t = 0 To gridr.Rows.Count - 2

                        If gridr.Rows(t).Cells(c).Value = "O" Then
                            gridr.Rows(t).Cells(c).Value = "Other"
                        End If
                        If gridr.Rows(t).Cells(c).Value = "C" Then
                            gridr.Rows(t).Cells(c).Value = "Government"
                        End If

                        xlSheet.Cells(e + 4, i + 4) = gridr.Rows(t).Cells(c).Value
                        '.Range(.Cells(e + 4, i + 4), .Cells(e + 4, i + 5)).VerticalAlignment = xlTop
                        .Range(.Cells(e + 4, i + 4), .Cells(e + 4, i + 5)).Merge()
                        .Range(.Cells(e + 4, i + 1), .Cells(e + 4, i + 5)).BorderAround()

                        e = e + 1
                    Next t
                    e = 0
                    i = i + 2
                Next c
            ElseIf cmbrepttyp.SelectedIndex = 2 Then
                m = 1 : i = 0
                .Range(.Cells(1, 1), .Cells(1, 16)).Merge() : .Cells(1, 1) = cmbrepttyp.Text
                .Range(.Cells(1, 1), .Cells(1, 10)).Font.Bold = True : .Range(.Cells(1, 1), .Cells(1, 10)).HorizontalAlignment = HorizontalAlignment.Center
                .Range(.Cells(1, 1), .Cells(1, 10)).Font.Size = 12 : .Range(.Cells(1, 1), .Cells(1, 10)).Font.Underline = True : .Range(.Cells(m, 1), .Cells(m, 10)).Font.Color = Color.Red
                fillgrids()
                .Range(.Cells(2, 1), .Cells(3, 1)).Merge() : .Range(.Cells(2, 1), .Cells(3, 1)).BorderAround()
                .Range(.Cells(2, 2), .Cells(3, 2)).Merge() : .Range(.Cells(2, 2), .Cells(3, 2)).BorderAround()
                .Range(.Cells(2, 3), .Cells(3, 3)).Merge() : .Range(.Cells(2, 3), .Cells(3, 3)).BorderAround()
                .Range(.Cells(2, 4), .Cells(3, 4)).Merge() : .Range(.Cells(2, 4), .Cells(3, 4)).BorderAround()
                xlSheet.Cells(2, 1) = "Company Name"
                xlSheet.Cells(2, 2) = "TAN"
                xlSheet.Cells(2, 3) = "PAN"
                xlSheet.Cells(2, 4) = "Status"
                .Range(.Cells(2, 5), .Cells(2, 8)).Merge() : .Range(.Cells(2, 5), .Cells(2, 8)).BorderAround() : .Cells(2, 5) = "24Q"
                .Range(.Cells(2, 9), .Cells(2, 12)).Merge() : .Range(.Cells(2, 9), .Cells(2, 12)).BorderAround() : .Cells(2, 9) = "26Q"
                .Range(.Cells(2, 13), .Cells(2, 16)).Merge() : .Range(.Cells(2, 13), .Cells(2, 16)).BorderAround() : .Cells(2, 13) = "27EQ"
                xlSheet.Cells(3, 5) = "Q1"
                xlSheet.Cells(3, 6) = "Q2"
                xlSheet.Cells(3, 7) = "Q3"
                xlSheet.Cells(3, 8) = "Q4"
                xlSheet.Cells(3, 9) = "Q1"
                xlSheet.Cells(3, 10) = "Q2"
                xlSheet.Cells(3, 11) = "Q3"
                xlSheet.Cells(3, 12) = "Q4"
                xlSheet.Cells(3, 13) = "Q1"
                xlSheet.Cells(3, 14) = "Q2"
                xlSheet.Cells(3, 15) = "Q3"
                xlSheet.Cells(3, 16) = "Q4"
                .Range(.Cells(2, 1), .Cells(2, 16)).WrapText = True
                .Range(.Cells(2, 1), .Cells(2, 16)).BorderAround()
                .Range(.Cells(2, 1), .Cells(2, 16)).Font.Bold = True
                .Range(.Cells(2, 1), .Cells(2, 16)).Font.Size = 10
                .Range(.Cells(2, 1), .Cells(2, 16)).VerticalAlignment = HorizontalAlignment.Center
                .Range(.Cells(3, 5), .Cells(3, 16)).WrapText = True
                .Range(.Cells(3, 5), .Cells(3, 16)).BorderAround()
                .Range(.Cells(3, 5), .Cells(3, 16)).Font.Bold = True
                .Range(.Cells(3, 5), .Cells(3, 16)).Font.Size = 10
                .Range(.Cells(3, 5), .Cells(3, 16)).VerticalAlignment = HorizontalAlignment.Center
                'xlSheet.Cells(m + 1, N + 1) = grids.Rows(0).Cells(0).Value
                '    .Range(.Cells(m + 1, N + 1), .Cells(m + 2, N + 1)).Merge()
                '    .Range(.Cells(m + 1, N + 1), .Cells(m + 2, N + 1)).WrapText = True
                '    .Range(.Cells(m + 1, N + 1), .Cells(m + 2, N + 1)).BorderAround()
                '    .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).Font.Bold = True
                '    .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).Font.Size = 10
                '    .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).VerticalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 1, N + 2) = grids.Rows(0).Cells(1).Value
                '    .Range(.Cells(m + 1, N + 2), .Cells(m + 2, N + 2)).Merge()
                '    .Range(.Cells(m + 1, N + 2), .Cells(m + 2, N + 2)).BorderAround()
                '    .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 2)).Font.Bold = True
                '    .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 2)).Font.Size = 10
                '    .Range(.Cells(m + 1, N + 2), .Cells(m + 1, N + 2)).VerticalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 1, N + 3) = grids.Rows(0).Cells(2).Value
                '    .Range(.Cells(m + 1, N + 3), .Cells(m + 2, N + 3)).Merge()
                '    .Range(.Cells(m + 1, N + 3), .Cells(m + 2, N + 3)).BorderAround()
                '    .Range(.Cells(m + 1, N + 3), .Cells(m + 1, N + 3)).Font.Bold = True
                '    .Range(.Cells(m + 1, N + 3), .Cells(m + 1, N + 3)).Font.Size = 10
                '    .Range(.Cells(m + 1, N + 3), .Cells(m + 1, N + 3)).VerticalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 1, N + 4) = grids.Rows(0).Cells(3).Value
                '    .Range(.Cells(m + 1, N + 4), .Cells(m + 2, N + 4)).Merge()
                '    .Range(.Cells(m + 1, N + 4), .Cells(m + 2, N + 4)).BorderAround()
                '    .Range(.Cells(m + 1, N + 4), .Cells(m + 1, N + 4)).Font.Bold = True
                '    .Range(.Cells(m + 1, N + 4), .Cells(m + 1, N + 4)).Font.Size = 10
                '    .Range(.Cells(m + 1, N + 4), .Cells(m + 1, N + 4)).VerticalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 1, N + 5) = grids.Rows(0).Cells(4).Value
                '    .Range(.Cells(m + 1, N + 5), .Cells(m + 1, N + 8)).Merge()
                '    .Range(.Cells(m + 1, N + 5), .Cells(m + 1, N + 8)).BorderAround()
                '    .Range(.Cells(m + 1, N + 5), .Cells(m + 1, N + 8)).Font.Bold = True
                '    .Range(.Cells(m + 1, N + 5), .Cells(m + 1, N + 8)).Font.Size = 10
                '    .Range(.Cells(m + 1, N + 5), .Cells(m + 1, N + 8)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 1, N + 9) = grids.Rows(0).Cells(5).Value
                '    .Range(.Cells(m + 1, N + 9), .Cells(m + 1, N + 12)).Merge()
                '    .Range(.Cells(m + 1, N + 9), .Cells(m + 1, N + 12)).BorderAround()
                '    .Range(.Cells(m + 1, N + 9), .Cells(m + 1, N + 12)).Font.Bold = True
                '    .Range(.Cells(m + 1, N + 9), .Cells(m + 1, N + 12)).Font.Size = 10
                '    .Range(.Cells(m + 1, N + 9), .Cells(m + 1, N + 12)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 5) = "Q1"
                '    '.Range(.Cells(m + 1, n + 5), .Cells(m + 1, n + 8)).Merge:
                '    .Range(.Cells(m + 2, N + 5), .Cells(m + 2, N + 5)).BorderAround()
                '    .Range(.Cells(m + 2, N + 5), .Cells(m + 2, N + 5)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 5), .Cells(m + 2, N + 5)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 5), .Cells(m + 2, N + 5)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 6) = "Q2"
                '    '.Range(.Cells(m + 1, n + 5), .Cells(m + 1, n + 8)).Merge:
                '    .Range(.Cells(m + 2, N + 6), .Cells(m + 2, N + 6)).BorderAround()
                '    .Range(.Cells(m + 2, N + 6), .Cells(m + 2, N + 6)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 6), .Cells(m + 2, N + 6)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 6), .Cells(m + 2, N + 6)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 7) = "Q3"
                '    '.Range(.Cells(m + 1, n + 5), .Cells(m + 1, n + 8)).Merge:
                '    .Range(.Cells(m + 2, N + 7), .Cells(m + 2, N + 7)).BorderAround()
                '    .Range(.Cells(m + 2, N + 7), .Cells(m + 2, N + 7)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 7), .Cells(m + 2, N + 7)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 7), .Cells(m + 2, N + 7)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 8) = "Q4"
                '    .Range(.Cells(m + 2, N + 8), .Cells(m + 2, N + 8)).BorderAround()
                '    .Range(.Cells(m + 2, N + 8), .Cells(m + 2, N + 8)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 8), .Cells(m + 2, N + 8)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 8), .Cells(m + 2, N + 8)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 10) = "Q2"
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).BorderAround()
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).HorizontalAlignment = HorizontalAlignment.Center

                '    xlSheet.Cells(m + 2, N + 9) = "Q1"
                '    .Range(.Cells(m + 2, N + 9), .Cells(m + 2, N + 9)).BorderAround()
                '    .Range(.Cells(m + 2, N + 9), .Cells(m + 2, N + 9)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 9), .Cells(m + 2, N + 9)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 9), .Cells(m + 2, N + 9)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 10) = "Q2"
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).BorderAround()
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 10), .Cells(m + 2, N + 10)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 11) = "Q3"
                '    .Range(.Cells(m + 2, N + 11), .Cells(m + 2, N + 11)).BorderAround()
                '    .Range(.Cells(m + 2, N + 11), .Cells(m + 2, N + 11)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 11), .Cells(m + 2, N + 11)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 11), .Cells(m + 2, N + 11)).HorizontalAlignment = HorizontalAlignment.Center

                '    xlSheet.Cells(m + 2, N + 12) = "Q4"
                '    .Range(.Cells(m + 2, N + 12), .Cells(m + 2, N + 12)).BorderAround()
                '    .Range(.Cells(m + 2, N + 12), .Cells(m + 2, N + 12)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 12), .Cells(m + 2, N + 12)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 12), .Cells(m + 2, N + 12)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 1, N + 13) = grids.Rows(0).Cells(7).Value
                '    .Range(.Cells(m + 1, N + 13), .Cells(m + 1, N + 16)).Merge()
                '    .Range(.Cells(m + 1, N + 13), .Cells(m + 1, N + 16)).BorderAround()
                '    .Range(.Cells(m + 1, N + 13), .Cells(m + 1, N + 16)).Font.Bold = True
                '    .Range(.Cells(m + 1, N + 13), .Cells(m + 1, N + 16)).Font.Size = 10
                '    .Range(.Cells(m + 1, N + 13), .Cells(m + 1, N + 16)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 13) = "Q1"
                '    .Range(.Cells(m + 2, N + 13), .Cells(m + 2, N + 13)).BorderAround()
                '    .Range(.Cells(m + 2, N + 13), .Cells(m + 2, N + 13)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 13), .Cells(m + 2, N + 13)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 13), .Cells(m + 2, N + 13)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 14) = "Q2"
                '    .Range(.Cells(m + 2, N + 14), .Cells(m + 2, N + 14)).BorderAround()
                '    .Range(.Cells(m + 2, N + 14), .Cells(m + 2, N + 14)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 14), .Cells(m + 2, N + 14)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 14), .Cells(m + 2, N + 14)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 15) = "Q3"
                '    .Range(.Cells(m + 2, N + 15), .Cells(m + 2, N + 15)).BorderAround()
                '    .Range(.Cells(m + 2, N + 15), .Cells(m + 2, N + 15)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 15), .Cells(m + 2, N + 15)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 15), .Cells(m + 2, N + 15)).HorizontalAlignment = HorizontalAlignment.Center
                '    xlSheet.Cells(m + 2, N + 16) = "Q4"
                '    .Range(.Cells(m + 2, N + 16), .Cells(m + 2, N + 16)).BorderAround()
                '    .Range(.Cells(m + 2, N + 16), .Cells(m + 2, N + 16)).Font.Bold = True
                '    .Range(.Cells(m + 2, N + 16), .Cells(m + 2, N + 16)).Font.Size = 10
                '    .Range(.Cells(m + 2, N + 16), .Cells(m + 2, N + 16)).HorizontalAlignment = HorizontalAlignment.Center
                For R = 0 To grids.Rows.Count - 2
                    If grids.Rows(R).Cells(3).Value = "O" Then
                        grids.Rows(R).Cells(3).Value = "Other"
                    End If
                    If grids.Rows(R).Cells(3).Value = "C" Then
                        grids.Rows(R).Cells(3).Value = "Government"
                    End If

                    N = 0
                    xlSheet.Cells(m + 3, N + 1) = a + 1 & ".  " & grids.Rows(R).Cells(0).Value
                    .Range(.Cells(m + 3, N + 1), .Cells(m + 3, N + 1)).BorderAround()
                    .Range(.Cells(m + 3, N + 1), .Cells(m + 3, N + 1)).WrapText = True
                    ' .Range(.Cells(m + 3, N + 1), .Cells(m + 3, N + 1)).VerticalAlignment = xlTop

                    xlSheet.Cells(m + 3, N + 2) = grids.Rows(R).Cells(1).Value
                    .Range(.Cells(m + 3, N + 2), .Cells(m + 3, N + 2)).BorderAround()
                    .Range(.Cells(m + 3, N + 2), .Cells(m + 3, N + 2)).WrapText = True
                    '.Range(.Cells(m + 3, N + 2), .Cells(m + 3, N + 2)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 3) = grids.Rows(R).Cells(2).Value
                    .Range(.Cells(m + 3, N + 3), .Cells(m + 3, N + 3)).BorderAround()
                    .Range(.Cells(m + 3, N + 3), .Cells(m + 3, N + 3)).WrapText = True
                    '.Range(.Cells(m + 3, N + 3), .Cells(m + 3, N + 3)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 4) = grids.Rows(R).Cells(3).Value
                    .Range(.Cells(m + 3, N + 4), .Cells(m + 3, N + 4)).BorderAround()
                    .Range(.Cells(m + 3, N + 4), .Cells(m + 3, N + 4)).WrapText = True
                    '.Range(.Cells(m + 3, N + 4), .Cells(m + 3, N + 4)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 5) = grids.Rows(R).Cells(10).Value
                    .Range(.Cells(m + 3, N + 5), .Cells(m + 2, N + 5)).BorderAround()
                    .Range(.Cells(m + 3, N + 5), .Cells(m + 2, N + 5)).WrapText = True
                    '.Range(.Cells(m + 3, N + 5), .Cells(m + 2, N + 5)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 6) = grids.Rows(R).Cells(11).Value
                    .Range(.Cells(m + 3, N + 6), .Cells(m + 2, N + 6)).BorderAround()
                    .Range(.Cells(m + 3, N + 6), .Cells(m + 2, N + 6)).WrapText = True
                    '.Range(.Cells(m + 3, N + 6), .Cells(m + 2, N + 6)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 7) = grids.Rows(R).Cells(12).Value
                    .Range(.Cells(m + 3, N + 7), .Cells(m + 2, N + 7)).BorderAround()
                    .Range(.Cells(m + 3, N + 7), .Cells(m + 2, N + 7)).WrapText = True
                    '.Range(.Cells(m + 3, N + 7), .Cells(m + 2, N + 7)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 8) = grids.Rows(R).Cells(13).Value
                    .Range(.Cells(m + 3, N + 8), .Cells(m + 2, N + 8)).BorderAround()
                    .Range(.Cells(m + 3, N + 8), .Cells(m + 2, N + 8)).WrapText = True
                    '.Range(.Cells(m + 3, N + 8), .Cells(m + 2, N + 8)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 9) = grids.Rows(R).Cells(14).Value
                    .Range(.Cells(m + 3, N + 9), .Cells(m + 2, N + 9)).BorderAround()
                    .Range(.Cells(m + 3, N + 9), .Cells(m + 2, N + 9)).WrapText = True
                    '.Range(.Cells(m + 3, N + 9), .Cells(m + 2, N + 9)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 10) = grids.Rows(R).Cells(15).Value
                    .Range(.Cells(m + 3, N + 10), .Cells(m + 2, N + 10)).BorderAround()
                    .Range(.Cells(m + 3, N + 10), .Cells(m + 2, N + 10)).WrapText = True
                    '.Range(.Cells(m + 3, N + 10), .Cells(m + 2, N + 10)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 11) = grids.Rows(R).Cells(16).Value
                    .Range(.Cells(m + 3, N + 11), .Cells(m + 2, N + 11)).BorderAround()
                    .Range(.Cells(m + 3, N + 11), .Cells(m + 2, N + 11)).WrapText = True
                    ' .Range(.Cells(m + 3, N + 11), .Cells(m + 2, N + 11)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 12) = grids.Rows(R).Cells(17).Value
                    .Range(.Cells(m + 3, N + 12), .Cells(m + 2, N + 12)).BorderAround()
                    .Range(.Cells(m + 3, N + 12), .Cells(m + 2, N + 12)).WrapText = True
                    ' .Range(.Cells(m + 3, N + 12), .Cells(m + 2, N + 12)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 13) = grids.Rows(R).Cells(18).Value
                    .Range(.Cells(m + 3, N + 13), .Cells(m + 2, N + 13)).BorderAround()
                    .Range(.Cells(m + 3, N + 13), .Cells(m + 2, N + 13)).WrapText = True
                    ' .Range(.Cells(m + 3, N + 13), .Cells(m + 2, N + 13)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 14) = grids.Rows(R).Cells(19).Value
                    .Range(.Cells(m + 3, N + 14), .Cells(m + 2, N + 14)).BorderAround()
                    .Range(.Cells(m + 3, N + 14), .Cells(m + 2, N + 14)).WrapText = True
                    '.Range(.Cells(m + 3, N + 14), .Cells(m + 2, N + 14)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 15) = grids.Rows(R).Cells(20).Value
                    .Range(.Cells(m + 3, N + 15), .Cells(m + 2, N + 15)).BorderAround()
                    .Range(.Cells(m + 3, N + 15), .Cells(m + 2, N + 15)).WrapText = True
                    ' .Range(.Cells(m + 3, N + 15), .Cells(m + 2, N + 15)).VerticalAlignment = xlTop
                    xlSheet.Cells(m + 3, N + 16) = grids.Rows(R).Cells(21).Value
                    .Range(.Cells(m + 3, N + 16), .Cells(m + 2, N + 16)).BorderAround()
                    .Range(.Cells(m + 3, N + 16), .Cells(m + 2, N + 16)).WrapText = True
                    ' .Range(.Cells(m + 3, N + 16), .Cells(m + 2, N + 16)).VerticalAlignment = xlTop
                    m = m + 1
                    N = N + 1
                    a = a + 1
                Next

                xlSheet.Cells(m + 3, N + 1) = " Y : Return Created "
                xlSheet.Cells(m + 4, N + 1) = " N : Return Not Created"

            ElseIf cmbrepttyp.SelectedIndex = 3 Then
                m = 2 : i = 0
                .Range(.Cells(m, 1), .Cells(m, 9)).Merge() : .Cells(m, 1) = cmbrepttyp.Text
                .Range(.Cells(m, 1), .Cells(m, 9)).Font.Bold = True : .Range(.Cells(m, 1), .Cells(m, 9)).HorizontalAlignment = HorizontalAlignment.Center
                .Range(.Cells(m, 1), .Cells(m, 9)).Font.Size = 12 : .Range(.Cells(m, 1), .Cells(m, 9)).Font.Underline = True : .Range(.Cells(m, 1), .Cells(m, 9)).Font.Color = Color.Red

                For R = 1 To gridb.Rows.Count - 1
                    xlSheet.Cells(m + 1, N + 1) = "Company Name :-"
                    .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).Font.Bold = True  '.Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).HorizontalAlignment = HorizontalAlignment.Left : .Range(.Cells(m + 1, N + 1), .Cells(m + 1, N + 1)).Font.Size = 9
                    xlSheet.Cells(m + 2, N + 1) = "TAN :-"
                    .Range(.Cells(m + 2, N + 1), .Cells(m + 2, N + 1)).Font.Bold = True  '.Range(.Cells(m + 2, N + 1), .Cells(m + 2, N + 1)).HorizontalAlignment = HorizontalAlignment.Left : .Range(.Cells(m + 2, N + 1), .Cells(m + 2, N + 1)).Font.Size = 9
                    xlSheet.Cells(m + 2, N + 4) = "PAN :-"
                    .Range(.Cells(m + 2, N + 4), .Cells(m + 2, N + 4)).Font.Bold = True  '.Range(.Cells(m + 2, N + 4), .Cells(m + 2, N + 4)).HorizontalAlignment = HorizontalAlignment.Left : .Range(.Cells(m + 2, N + 4), .Cells(m + 2, N + 4)).Font.Size = 9
                    xlSheet.Cells(m + 2, N + 7) = "Status :-"
                    .Range(.Cells(m + 2, N + 7), .Cells(m + 2, N + 7)).Font.Bold = True  '.Range(.Cells(m + 2, N + 7), .Cells(m + 2, N + 7)).HorizontalAlignment = HorizontalAlignment.Left : .Range(.Cells(m + 2, N + 7), .Cells(m + 2, N + 7)).Font.Size = 9
                    xlSheet.Cells(m + 3, N + 5) = "24Q"
                    .Range(.Cells(m + 3, N + 2), .Cells(m + 3, N + 5)).Font.Bold = True  '.Range(.Cells(m + 3, N + 2), .Cells(m + 3, N + 5)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 3, N + 2), .Cells(m + 3, N + 5)).Font.Size = 9 : .Range(.Cells(m + 3, N + 2), .Cells(m + 3, N + 6)).Merge() : .Range(.Cells(m + 3, N + 1), .Cells(m + 9, N + 16)).Borders.LineStyle = 1
                    xlSheet.Cells(m + 3, N + 7) = "26Q"
                    .Range(.Cells(m + 3, N + 7), .Cells(m + 3, N + 11)).Font.Bold = True  '.Range(.Cells(m + 3, N + 7), .Cells(m + 3, N + 11)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 3, N + 7), .Cells(m + 3, N + 11)).Font.Size = 9 : .Range(.Cells(m + 3, N + 7), .Cells(m + 3, N + 11)).Merge()
                    xlSheet.Cells(m + 3, N + 12) = "27EQ"
                    .Range(.Cells(m + 3, N + 12), .Cells(m + 3, N + 16)).Font.Bold = True  '.Range(.Cells(m + 3, N + 12), .Cells(m + 3, N + 16)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 3, N + 12), .Cells(m + 3, N + 16)).Font.Size = 9 : .Range(.Cells(m + 3, N + 12), .Cells(m + 3, N + 16)).Merge()
                    xlSheet.Cells(m + 4, N + 2) = "Challan"
                    .Range(.Cells(m + 4, N + 2), .Cells(m + 4, N + 3)).Font.Bold = True ' .Range(.Cells(m + 4, N + 2), .Cells(m + 4, N + 3)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 4, N + 2), .Cells(m + 4, N + 3)).Font.Size = 9 : .Range(.Cells(m + 4, N + 2), .Cells(m + 4, N + 3)).Merge()
                    xlSheet.Cells(m + 4, N + 4) = "Deductee"
                    .Range(.Cells(m + 4, N + 4), .Cells(m + 4, N + 6)).Font.Bold = True ' .Range(.Cells(m + 4, N + 4), .Cells(m + 4, N + 6)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 4, N + 4), .Cells(m + 4, N + 6)).Font.Size = 9 : .Range(.Cells(m + 4, N + 4), .Cells(m + 4, N + 6)).Merge()
                    xlSheet.Cells(m + 4, N + 8) = "Challan"
                    .Range(.Cells(m + 4, N + 7), .Cells(m + 4, N + 8)).Font.Bold = True  '.Range(.Cells(m + 4, N + 7), .Cells(m + 4, N + 8)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 4, N + 7), .Cells(m + 4, N + 8)).Font.Size = 9 : .Range(.Cells(m + 4, N + 7), .Cells(m + 4, N + 8)).Merge()
                    xlSheet.Cells(m + 4, N + 11) = "Deductee"
                    .Range(.Cells(m + 4, N + 9), .Cells(m + 4, N + 11)).Font.Bold = True ' .Range(.Cells(m + 4, N + 9), .Cells(m + 4, N + 11)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 4, N + 9), .Cells(m + 4, N + 11)).Font.Size = 9 : .Range(.Cells(m + 4, N + 9), .Cells(m + 4, N + 11)).Merge()
                    xlSheet.Cells(m + 4, N + 13) = "Challan"
                    .Range(.Cells(m + 4, N + 12), .Cells(m + 4, N + 13)).Font.Bold = True  '.Range(.Cells(m + 4, N + 12), .Cells(m + 4, N + 13)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 4, N + 12), .Cells(m + 4, N + 13)).Font.Size = 9 : .Range(.Cells(m + 4, N + 12), .Cells(m + 4, N + 13)).Merge()
                    xlSheet.Cells(m + 4, N + 16) = "Deductee"
                    .Range(.Cells(m + 4, N + 14), .Cells(m + 4, N + 16)).Font.Bold = True  '.Range(.Cells(m + 4, N + 14), .Cells(m + 4, N + 16)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 4, N + 14), .Cells(m + 4, N + 16)).Font.Size = 9 : .Range(.Cells(m + 4, N + 14), .Cells(m + 4, N + 16)).Merge()
                    xlSheet.Cells(m + 5, N + 2) = "Count"
                    .Range(.Cells(m + 5, N + 2), .Cells(m + 5, N + 2)).Font.Bold = True  '.Range(.Cells(m + 5, N + 2), .Cells(m + 5, N + 2)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 2), .Cells(m + 5, N + 2)).Font.Size = 9 : .Range(.Cells(m + 5, N + 2), .Cells(m + 5, N + 2)).Merge()
                    xlSheet.Cells(m + 5, N + 3) = "Amount"
                    .Range(.Cells(m + 5, N + 3), .Cells(m + 5, N + 3)).Font.Bold = True  '.Range(.Cells(m + 5, N + 3), .Cells(m + 5, N + 3)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 3), .Cells(m + 5, N + 3)).Font.Size = 9 : .Range(.Cells(m + 5, N + 3), .Cells(m + 5, N + 3)).Merge()
                    xlSheet.Cells(m + 5, N + 4) = "Count"
                    .Range(.Cells(m + 5, N + 4), .Cells(m + 5, N + 4)).Font.Bold = True  '.Range(.Cells(m + 5, N + 2), .Cells(m + 5, N + 2)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 4), .Cells(m + 5, N + 4)).Font.Size = 9 : .Range(.Cells(m + 5, N + 2), .Cells(m + 5, N + 2)).Merge()
                    xlSheet.Cells(m + 5, N + 5) = "Deposit Amt"
                    .Range(.Cells(m + 5, N + 5), .Cells(m + 5, N + 5)).Font.Bold = True : .Range(.Cells(m + 5, N + 5), .Cells(m + 5, N + 5)).Font.Size = 9 : .Range(.Cells(m + 5, N + 5), .Cells(m + 5, N + 5)).Merge()
                    xlSheet.Cells(m + 5, N + 6) = "Deducted Amt"
                    .Range(.Cells(m + 5, N + 6), .Cells(m + 5, N + 6)).Font.Bold = True ' .Range(.Cells(m + 5, N + 6), .Cells(m + 5, N + 6)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 6), .Cells(m + 5, N + 6)).Font.Size = 9 : .Range(.Cells(m + 5, N + 6), .Cells(m + 5, N + 6)).Merge()
                    xlSheet.Cells(m + 5, N + 7) = "Count"
                    .Range(.Cells(m + 5, N + 7), .Cells(m + 5, N + 7)).Font.Bold = True  '.Range(.Cells(m + 5, N + 7), .Cells(m + 5, N + 7)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 7), .Cells(m + 5, N + 7)).Font.Size = 9 : .Range(.Cells(m + 5, N + 7), .Cells(m + 5, N + 7)).Merge()
                    xlSheet.Cells(m + 5, N + 8) = "Amount"
                    .Range(.Cells(m + 5, N + 8), .Cells(m + 5, N + 8)).Font.Bold = True  '.Range(.Cells(m + 5, N + 8), .Cells(m + 5, N + 8)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 8), .Cells(m + 5, N + 8)).Font.Size = 9 : .Range(.Cells(m + 5, N + 8), .Cells(m + 5, N + 8)).Merge()
                    xlSheet.Cells(m + 5, N + 9) = "Count"
                    .Range(.Cells(m + 5, N + 9), .Cells(m + 5, N + 9)).Font.Bold = True ' .Range(.Cells(m + 5, N + 9), .Cells(m + 5, N + 9)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 9), .Cells(m + 5, N + 9)).Font.Size = 9
                    xlSheet.Cells(m + 5, N + 10) = "Deposit Amt"
                    .Range(.Cells(m + 5, N + 10), .Cells(m + 5, N + 10)).Font.Bold = True  '.Range(.Cells(m + 5, N + 10), .Cells(m + 5, N + 10)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 10), .Cells(m + 5, N + 10)).Font.Size = 9
                    xlSheet.Cells(m + 5, N + 11) = "Deducted Amt"
                    .Range(.Cells(m + 5, N + 11), .Cells(m + 5, N + 11)).Font.Bold = True  '.Range(.Cells(m + 5, N + 11), .Cells(m + 5, N + 11)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 11), .Cells(m + 5, N + 11)).Font.Size = 9 : .Range(.Cells(m + 5, N + 11), .Cells(m + 5, N + 11)).Merge()
                    .Range(.Cells(m + 5, N + 1), .Cells(m + 5, N + 16)).WrapText = True
                    xlSheet.Cells(m + 5, N + 12) = "Count"
                    .Range(.Cells(m + 5, N + 12), .Cells(m + 5, N + 12)).Font.Bold = True  '.Range(.Cells(m + 5, N + 12), .Cells(m + 5, N + 12)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 12), .Cells(m + 5, N + 12)).Font.Size = 9 : .Range(.Cells(m + 5, N + 12), .Cells(m + 5, N + 12)).Merge()
                    xlSheet.Cells(m + 5, N + 13) = "Amount"
                    .Range(.Cells(m + 5, N + 13), .Cells(m + 5, N + 13)).Font.Bold = True ' .Range(.Cells(m + 5, N + 13), .Cells(m + 5, N + 13)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 13), .Cells(m + 5, N + 13)).Font.Size = 9 : .Range(.Cells(m + 5, N + 13), .Cells(m + 5, N + 13)).Merge()
                    xlSheet.Cells(m + 5, N + 14) = "Count"
                    .Range(.Cells(m + 5, N + 14), .Cells(m + 5, N + 14)).Font.Bold = True  '.Range(.Cells(m + 5, N + 14), .Cells(m + 5, N + 14)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 14), .Cells(m + 5, N + 14)).Font.Size = 9
                    xlSheet.Cells(m + 5, N + 15) = "Deposit Amt"
                    .Range(.Cells(m + 5, N + 15), .Cells(m + 5, N + 15)).Font.Bold = True  '.Range(.Cells(m + 5, N + 15), .Cells(m + 5, N + 15)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 15), .Cells(m + 5, N + 15)).Font.Size = 9
                    xlSheet.Cells(m + 5, N + 16) = "Deducted Amt"
                    .Range(.Cells(m + 5, N + 16), .Cells(m + 5, N + 16)).Font.Bold = True  '.Range(.Cells(m + 5, N + 16), .Cells(m + 5, N + 16)).HorizontalAlignment = HorizontalAlignment.Center
                    .Range(.Cells(m + 5, N + 16), .Cells(m + 5, N + 16)).Font.Size = 9 : .Range(.Cells(m + 5, N + 16), .Cells(m + 5, N + 16)).Merge()
                    xlSheet.Cells(m + 6, N + 1) = "Q1"
                    xlSheet.Cells(m + 7, N + 1) = "Q2"
                    xlSheet.Cells(m + 8, N + 1) = "Q3"
                    xlSheet.Cells(m + 9, N + 1) = "Q4"
                    m = m + 11
                    R = R + 4
                Next R

                For R = 0 To gridb.Rows.Count - 1
                    N = 0
                    xlSheet.Cells(c + 3, N + 3) = gridb.Rows(R).Cells(0).Value : .Range(.Cells(c + 3, N + 3), .Cells(m + 5, N + 16)).Font.Size = 9
                    .Range(.Cells(c + 3, N + 3), .Cells(c + 3, N + 16)).Merge()
                    xlSheet.Cells(c + 4, N + 2) = gridb.Rows(R).Cells(1).Value
                    .Range(.Cells(c + 4, N + 2), .Cells(c + 4, N + 3)).Font.Size = 9 : .Range(.Cells(c + 4, N + 2), .Cells(c + 4, N + 3)).Merge()
                    xlSheet.Cells(c + 4, N + 5) = gridb.Rows(R).Cells(2).Value
                    .Range(.Cells(c + 4, N + 5), .Cells(c + 4, N + 6)).Font.Size = 9 : .Range(.Cells(c + 4, N + 5), .Cells(c + 4, N + 6)).Merge()
                    xlSheet.Cells(c + 8, N + 2) = gridb.Rows(R).Cells(8).Value
                    xlSheet.Cells(c + 8, N + 3) = gridb.Rows(R).Cells(9).Value
                    xlSheet.Cells(c + 8, N + 7) = gridb.Rows(R).Cells(10).Value
                    xlSheet.Cells(c + 8, N + 8) = gridb.Rows(R).Cells(11).Value
                    xlSheet.Cells(c + 8, N + 12) = gridb.Rows(R).Cells(12).Value
                    xlSheet.Cells(c + 8, N + 13) = gridb.Rows(R).Cells(13).Value
                    xlSheet.Cells(c + 8, N + 4) = gridb.Rows(R).Cells(14).Value
                    xlSheet.Cells(c + 8, N + 5) = gridb.Rows(R).Cells(15).Value
                    xlSheet.Cells(c + 8, N + 6) = gridb.Rows(R).Cells(16).Value
                    xlSheet.Cells(c + 8, N + 9) = gridb.Rows(R).Cells(17).Value
                    xlSheet.Cells(c + 8, N + 10) = gridb.Rows(R).Cells(18).Value
                    xlSheet.Cells(c + 8, N + 11) = gridb.Rows(R).Cells(19).Value
                    xlSheet.Cells(c + 8, N + 14) = gridb.Rows(R).Cells(20).Value
                    xlSheet.Cells(c + 8, N + 15) = gridb.Rows(R).Cells(21).Value
                    xlSheet.Cells(c + 8, N + 16) = gridb.Rows(R).Cells(22).Value
                    xlSheet.Cells(c + 4, N + 8) = gridb.Rows(R).Cells(23).Value
                    c = c + 11
                    R = R + 3
                Next R
                For R = 1 To gridb.Rows.Count - 1
                    xlSheet.Cells(d + 9, N + 2) = gridb.Rows(R).Cells(8).Value
                    xlSheet.Cells(d + 9, N + 3) = gridb.Rows(R).Cells(9).Value
                    xlSheet.Cells(d + 9, N + 7) = gridb.Rows(R).Cells(10).Value
                    xlSheet.Cells(d + 9, N + 8) = gridb.Rows(R).Cells(11).Value
                    xlSheet.Cells(d + 9, N + 12) = gridb.Rows(R).Cells(12).Value
                    xlSheet.Cells(d + 9, N + 13) = gridb.Rows(R).Cells(13).Value
                    xlSheet.Cells(d + 9, N + 4) = gridb.Rows(R).Cells(14).Value
                    xlSheet.Cells(d + 9, N + 5) = gridb.Rows(R).Cells(15).Value
                    xlSheet.Cells(d + 9, N + 6) = gridb.Rows(R).Cells(16).Value
                    xlSheet.Cells(d + 9, N + 9) = gridb.Rows(R).Cells(17).Value
                    xlSheet.Cells(d + 9, N + 10) = gridb.Rows(R).Cells(18).Value
                    xlSheet.Cells(d + 9, N + 11) = gridb.Rows(R).Cells(19).Value
                    xlSheet.Cells(d + 9, N + 14) = gridb.Rows(R).Cells(20).Value
                    xlSheet.Cells(d + 9, N + 15) = gridb.Rows(R).Cells(21).Value
                    xlSheet.Cells(d + 9, N + 16) = gridb.Rows(R).Cells(22).Value

                    d = d + 11
                    R = R + 3
                Next R
                For R = 2 To gridb.Rows.Count - 1
                    xlSheet.Cells(e + 10, N + 2) = gridb.Rows(R).Cells(8).Value
                    xlSheet.Cells(e + 10, N + 3) = gridb.Rows(R).Cells(9).Value
                    xlSheet.Cells(e + 10, N + 7) = gridb.Rows(R).Cells(10).Value
                    xlSheet.Cells(e + 10, N + 8) = gridb.Rows(R).Cells(11).Value
                    xlSheet.Cells(e + 10, N + 12) = gridb.Rows(R).Cells(12).Value
                    xlSheet.Cells(e + 10, N + 13) = gridb.Rows(R).Cells(13).Value
                    xlSheet.Cells(e + 10, N + 4) = gridb.Rows(R).Cells(14).Value
                    xlSheet.Cells(e + 10, N + 5) = gridb.Rows(R).Cells(15).Value
                    xlSheet.Cells(e + 10, N + 6) = gridb.Rows(R).Cells(16).Value
                    xlSheet.Cells(e + 10, N + 9) = gridb.Rows(R).Cells(17).Value
                    xlSheet.Cells(e + 10, N + 10) = gridb.Rows(R).Cells(18).Value
                    xlSheet.Cells(e + 10, N + 11) = gridb.Rows(R).Cells(19).Value
                    xlSheet.Cells(e + 10, N + 14) = gridb.Rows(R).Cells(20).Value
                    xlSheet.Cells(e + 10, N + 15) = gridb.Rows(R).Cells(21).Value
                    xlSheet.Cells(e + 10, N + 16) = gridb.Rows(R).Cells(22).Value

                    e = e + 11
                    R = R + 3
                Next R

                For R = 3 To gridb.Rows.Count - 1
                    xlSheet.Cells(F + 11, N + 2) = gridb.Rows(R).Cells(8).Value
                    xlSheet.Cells(F + 11, N + 3) = gridb.Rows(R).Cells(9).Value
                    xlSheet.Cells(F + 11, N + 7) = gridb.Rows(R).Cells(10).Value
                    xlSheet.Cells(F + 11, N + 8) = gridb.Rows(R).Cells(11).Value
                    xlSheet.Cells(F + 11, N + 12) = gridb.Rows(R).Cells(12).Value
                    xlSheet.Cells(F + 11, N + 13) = gridb.Rows(R).Cells(13).Value
                    xlSheet.Cells(F + 11, N + 4) = gridb.Rows(R).Cells(14).Value
                    xlSheet.Cells(F + 11, N + 5) = gridb.Rows(R).Cells(15).Value
                    xlSheet.Cells(F + 11, N + 6) = gridb.Rows(R).Cells(16).Value
                    xlSheet.Cells(F + 11, N + 9) = gridb.Rows(R).Cells(17).Value
                    xlSheet.Cells(F + 11, N + 10) = gridb.Rows(R).Cells(18).Value
                    xlSheet.Cells(F + 11, N + 11) = gridb.Rows(R).Cells(19).Value
                    xlSheet.Cells(F + 11, N + 14) = gridb.Rows(R).Cells(20).Value
                    xlSheet.Cells(F + 11, N + 15) = gridb.Rows(R).Cells(21).Value
                    xlSheet.Cells(F + 11, N + 16) = gridb.Rows(R).Cells(22).Value

                    F = F + 11
                    R = R + 3
                Next R
            End If
        End With
        xlapp.Visible = True
    End Sub

    Private Sub cmbCNM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCNM.SelectedIndexChanged
        Dim rs, rs1, rsstatus As New DataSet
        Dim sqlstatus As String
        Dim sql As String
        Dim sql1 As String
        Dim i, m As Integer
        gridheadb()

        If cmbCNM.SelectedIndex = 0 Then

            For R = 1 To gridb.Rows.Count - 1
                gridb.Rows(R).Cells(3).Value = CheckState.Unchecked
                gridb.Rows(R).Cells(4).Value = CheckState.Unchecked
                gridb.Rows(R).Cells(5).Value = CheckState.Unchecked

            Next
            gridb.RowCount = 2
        End If
        If cmbCNM.SelectedIndex = 1 Then
            gridb.Rows.Clear()
            gridheadb()

            sql = "SELECT  distinct CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN,comst.coid, CoMst.CoStatus FROM CoMst"
            rs = FetchDataSet(sql)
            If rs.Tables(0).Rows.Count > 0 Then
                gridb.RowCount = rs.Tables(0).Rows.Count * 4

                ' If rs.Tables(0).Rows.Count <> 0 Then
                'For m = 0 To gridb.RowCount
                Dim s = rs.Tables(0).Rows.Count
                For s = 0 To rs.Tables(0).Rows.Count - 1 'gridb.RowCount
                    gridb.RowCount = gridb.RowCount + 1
                    'Dim s = rs.Tables(0).Rows.Count
                    gridb.Rows(m + 0).Cells(7).Value = "Q1"
                    gridb.Rows(m + 1).Cells(7).Value = "Q2"
                    gridb.Rows(m + 2).Cells(7).Value = "Q3"
                    gridb.Rows(m + 3).Cells(7).Value = "Q4"
                    gridb.Rows(m + 0).Cells(0).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s)(0).ToString()), "", rs.Tables(0).Rows(s)(0).ToString())
                    gridb.Rows(m + 0).Cells(1).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s)(1).ToString()), "", rs.Tables(0).Rows(s)(1).ToString())
                    gridb.Rows(m + 0).Cells(2).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s)(2).ToString()), "", rs.Tables(0).Rows(s)(2).ToString())
                    gridb.Rows(m + 0).Cells(6).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s)(3).ToString()), "", rs.Tables(0).Rows(s)(3).ToString())
                    gridb.Rows(m + 0).Cells(23).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s)(4).ToString()), "", rs.Tables(0).Rows(s)(4).ToString())
                    gridb.Rows(m + 0).Cells(3).Value = CheckState.Checked
                    gridb.Rows(m + 0).Cells(4).Value = CheckState.Checked
                    gridb.Rows(m + 0).Cells(5).Value = CheckState.Checked
                    m = m + 4
                    's = s + 1
                    '      End While
                Next

                'End If

            End If
            For R = 0 To gridb.Rows.Count - 1
                If gridb.Rows(R).Cells(23).Value = "O" Then
                    gridb.Rows(R).Cells(23).Value = "Other"
                ElseIf gridb.Rows(R).Cells(23).Value = "C" Then
                    gridb.Rows(R).Cells(23).Value = "Government"
                End If
                R = R + 3
            Next R

            rs.Dispose()
            rs = Nothing
            If gridb.Columns(3).HeaderText = "24Q" Then
                If gridb.Rows(0).Cells(3).Value = CheckState.Checked Then
                    If gridb.Rows(0).Cells(7).Value = "Q1" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q1'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q1'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            'rs = Nothing
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(1).Cells(7).Value = "Q2" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q2'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 1).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()
                                Next
                            End If
                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q2'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R + 1).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 1).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If

                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()

                        Next R
                    End If

                    If gridb.Rows(2).Cells(7).Value = "Q3" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q3'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 2).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q3'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    gridb.Rows(R + 2).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 2).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()
                                Next
                            End If

                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(3).Cells(7).Value = "Q4" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q4'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 3).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q4'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R + 3).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 3).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If
            ''add
            If gridb.Columns(4).HeaderText = "26Q" Then
                If gridb.Rows(0).Cells(4).Value = CheckState.Checked Then
                    If gridb.Rows(0).Cells(7).Value = "Q1" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q1'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q1'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    gridb.Rows(R).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If

                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(1).Cells(7).Value = "Q2" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q2'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 1).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q2'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R + 1).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 1).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If


                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(2).Cells(7).Value = "Q3" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q3'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 2).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q3'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 2).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 2).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If


                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(3).Cells(7).Value = "Q4" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q4'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 3).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q4'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 3).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 3).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If


                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If

            If gridb.Columns(5).HeaderText = "27EQ" Then
                If gridb.Rows(0).Cells(5).Value = CheckState.Checked Then
                    If gridb.Rows(0).Cells(7).Value = "Q1" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ1'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(13).Value = rs.Tables(0).Rows(0)(0).ToString()
                                Next
                            End If
                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ1'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R).Cells(20).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(21).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R).Cells(22).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If

                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(1).Cells(7).Value = "Q2" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ2'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 1).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(13).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If

                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ2'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 1).Cells(20).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(21).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 1).Cells(22).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If

                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(2).Cells(7).Value = "Q3" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ3'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 2).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(13).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ3'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 2).Cells(20).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(21).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 2).Cells(22).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If

                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If

                    If gridb.Rows(3).Cells(7).Value = "Q4" Then
                        For R = 0 To gridb.Rows.Count - 1
                            'r = 0
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ4'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 3).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(13).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                        & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ4'" _
                    & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 3).Cells(20).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(21).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 3).Cells(22).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If

                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If



        Else
            gridb.Rows.Clear()
            gridheadb()
            'gridb.EditMode = True
            sql = "  SELECT  distinct CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN,comst.coid,CoMst.CoStatus FROM CoMst" _
    & " where comst.coid =" & cmbCNM.SelectedIndex - 1
            rs = FetchDataSet(sql)
            If rs.Tables(0).Rows.Count > 0 Then
                gridb.RowCount = rs.Tables(0).Rows.Count + 1
                Dim s1 = rs.Tables(0).Rows.Count
                For s1 = 0 To rs.Tables(0).Rows.Count - 1
                    gridb.RowCount = gridb.Rows.Count + 4
                    'For m = 1 To grids.Rows - 1
                    'c = 0
                    gridb.Rows(m + 0).Cells(7).Value = "Q1"
                    gridb.Rows(m + 1).Cells(7).Value = "Q2"
                    gridb.Rows(m + 2).Cells(7).Value = "Q3"
                    gridb.Rows(m + 3).Cells(7).Value = "Q4"
                    ' c = c + 1
                    'Next m
                    gridb.Rows(m + 0).Cells(0).Value = rs.Tables(0).Rows(s1)(0).ToString()
                    gridb.Rows(m + 0).Cells(1).Value = rs.Tables(0).Rows(s1)(1).ToString()
                    gridb.Rows(m + 0).Cells(2).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s1)(2).ToString()), "", rs.Tables(0).Rows(s1)(2).ToString())   'IIf(IsNull(rs1(2), "", rs1(2)))
                    gridb.Rows(m + 0).Cells(6).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s1)(3).ToString()), "", rs.Tables(0).Rows(s1)(3).ToString())
                    gridb.Rows(m + 0).Cells(23).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(s1)(4).ToString()), "", rs.Tables(0).Rows(s1)(4).ToString())
                    gridb.Rows(m + 0).Cells(3).Value = CheckState.Checked
                    gridb.Rows(m + 0).Cells(4).Value = CheckState.Checked
                    gridb.Rows(m + 0).Cells(5).Value = CheckState.Checked
                    m = m + 4

                Next
            End If
            For R = 0 To gridb.Rows.Count - 1
                If gridb.Rows(R).Cells(23).Value = "O" Then
                    gridb.Rows(R).Cells(23).Value = "Other"
                ElseIf gridb.Rows(R).Cells(23).Value = "C" Then
                    gridb.Rows(R).Cells(23).Value = "Government"
                End If
                R = R + 3
            Next R
            rs.Dispose()
            rs = Nothing

            If gridb.Columns(3).HeaderText = "24Q" Then
                If gridb.Rows(0).Cells(3).Value = CheckState.Checked Then
                    If gridb.Rows(0).Cells(7).Value = "Q1" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
        & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
        & " WHERE RetnMst.FrmType='24Q1'" _
        & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    gridb.Rows(R).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()
                                Next
                            End If

                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                        & " WHERE RetnMst.FrmType='24Q1'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    gridb.Rows(R).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()
                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If

            If gridb.Columns(3).HeaderText = "24Q" Then
                If gridb.Rows(0).Cells(3).Value = CheckState.Checked Then
                    If gridb.Rows(1).Cells(7).Value = "Q2" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q2'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    gridb.Rows(R + 1).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If

                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                        & " WHERE RetnMst.FrmType='24Q2'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    gridb.Rows(R + 1).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 1).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If
            '
            If gridb.Columns(3).HeaderText = "24Q" Then
                If gridb.Rows(0).Cells(3).Value = CheckState.Checked Then
                    If gridb.Rows(2).Cells(7).Value = "Q3" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q3'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 2).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If

                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                        & " WHERE RetnMst.FrmType='24Q3'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 2).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 2).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If
            '
            If gridb.Columns(3).HeaderText = "24Q" Then
                If gridb.Rows(0).Cells(3).Value = CheckState.Checked Then
                    If gridb.Rows(3).Cells(7).Value = "Q4" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan24Q.RetnID) AS CountOfRetnID, Sum(Challan24Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan24Q ON RetnMst.RetnID = Challan24Q.RetnID" _
                    & " WHERE RetnMst.FrmType='24Q4'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count - 1
                                    gridb.Rows(R + 3).Cells(8).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(9).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If

                            sql1 = " SELECT Count(Deductee24Q.DId) AS CountOfDId, Sum(Deductee24Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee24Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee24Q ON RetnMst.RetnID = Deductee24Q.RetnID" _
                        & " WHERE RetnMst.FrmType='24Q4'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count - 1
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 3).Cells(14).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(15).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 3).Cells(16).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs1.Dispose()
                            rs.Dispose()
                        Next R
                    End If
                End If
            End If

            ''add
            If gridb.Columns(4).HeaderText = "26Q" Then
                If gridb.Rows(0).Cells(4).Value = CheckState.Checked Then
                    If gridb.Rows(0).Cells(7).Value = "Q1" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q1'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then

                                For j = 0 To rs.Tables(0).Rows.Count - 1
                                    gridb.Rows(R).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If

                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                        & " WHERE RetnMst.FrmType='26Q1'" _
                        & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count - 1
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If
            If gridb.Columns(4).HeaderText = "26Q" Then
                If gridb.Rows(0).Cells(4).Value = CheckState.Checked Then
                    If gridb.Rows(1).Cells(7).Value = "Q2" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q2'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count - 1

                                    gridb.Rows(R + 1).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If
                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                        & " WHERE RetnMst.FrmType='26Q2'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R + 1).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 1).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs1.Dispose()
                            rs.Dispose()
                        Next R
                    End If
                End If
            End If
            If gridb.Columns(4).HeaderText = "26Q" Then
                If gridb.Rows(0).Cells(4).Value = CheckState.Checked Then
                    If gridb.Rows(2).Cells(7).Value = "Q3" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q3'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 2).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If
                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                        & " WHERE RetnMst.FrmType='26Q3'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R + 2).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 2).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            rs1.Dispose()
                            rs.Dispose()
                            R = R + 3
                        Next R
                    End If
                End If
            End If
            If gridb.Columns(4).HeaderText = "26Q" Then
                If gridb.Rows(0).Cells(4).Value = CheckState.Checked Then
                    If gridb.Rows(3).Cells(7).Value = "Q4" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan26Q.RetnID) AS CountOfRetnID, Sum(Challan26Q.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan26Q ON RetnMst.RetnID = Challan26Q.RetnID" _
                    & " WHERE RetnMst.FrmType='26Q4'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 3).Cells(10).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(11).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If
                            sql1 = " SELECT Count(Deductee26Q.DId) AS CountOfDId, Sum(Deductee26Q.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee26Q.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee26Q ON RetnMst.RetnID = Deductee26Q.RetnID" _
                        & " WHERE RetnMst.FrmType='26Q4'" _
                        & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    gridb.Rows(R + 3).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 3).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs1.Dispose()
                            rs.Dispose()

                        Next R
                    End If
                End If
            End If
            '        ''add
            '
            If gridb.Columns(5).HeaderText = "27EQ" Then
                ' If gridb.Cell(flexcpChecked, 1, 5) = 1 Then
                If gridb.Rows(0).Cells(5).Value = CheckState.Checked Then
                    If gridb.Rows(0).Cells(7).Value = "Q1" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ1'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(13).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If
                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                        & " WHERE RetnMst.FrmType='27EQ1'" _
                        & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count - 1
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                    If gridb.Rows(1).Cells(7).Value = "Q2" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ2'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count

                                    gridb.Rows(R + 1).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(13).Value = rs.Tables(0).Rows(0)(1).ToString()

                                Next
                            End If
                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                        & " WHERE RetnMst.FrmType='27EQ2'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R + 1).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 1).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 1).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            rs.Dispose()
                            rs1.Dispose()
                            R = R + 3
                        Next R
                    End If
                    If gridb.Rows(2).Cells(7).Value = "Q3" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ3'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 2).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(13).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If
                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                        & " WHERE RetnMst.FrmType='27EQ3'" _
                        & " and Comst.Coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count

                                    gridb.Rows(R + 2).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 2).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 2).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                    If gridb.Rows(3).Cells(7).Value = "Q4" Then
                        For R = 0 To gridb.Rows.Count - 1
                            sql = " SELECT Count(Challan27EQ.RetnID) AS CountOfRetnID, Sum(Challan27EQ.TotalTax) AS SumOfTotalTax" _
                    & " FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Challan27EQ ON RetnMst.RetnID = Challan27EQ.RetnID" _
                    & " WHERE RetnMst.FrmType='27EQ4'" _
                    & " and comst.coid =" & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                    & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs = FetchDataSet(sql)
                            If rs.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs.Tables(0).Rows.Count
                                    'For r = 1 To gridb.Rows - 1
                                    gridb.Rows(R + 3).Cells(12).Value = rs.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(13).Value = rs.Tables(0).Rows(0)(1).ToString()


                                Next
                            End If
                            sql1 = " SELECT Count(Deductee27EQ.DId) AS CountOfDId, Sum(Deductee27EQ.TotalTaxDeposited) AS SumOfTotalTaxDeposited," _
                            & " Sum(Deductee27EQ.TotalTaxDeducted) AS SumOfTotalTaxDeducted FROM (CoMst INNER JOIN RetnMst ON CoMst.CoID = RetnMst.CoID) INNER JOIN Deductee27EQ ON RetnMst.RetnID = Deductee27EQ.RetnID" _
                        & " WHERE RetnMst.FrmType='27EQ4'" _
                        & " and Comst.Coid = " & IIf(String.IsNullOrEmpty(gridb.Rows(R).Cells(6).Value), "0", (gridb.Rows(R).Cells(6).Value)) & " " _
                        & " GROUP BY CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN"
                            rs1 = FetchDataSet(sql1)
                            If rs1.Tables(0).Rows.Count > 0 Then
                                For j = 0 To rs1.Tables(0).Rows.Count
                                    gridb.Rows(R + 3).Cells(17).Value = rs1.Tables(0).Rows(0)(0).ToString()
                                    gridb.Rows(R + 3).Cells(18).Value = rs1.Tables(0).Rows(0)(1).ToString()
                                    gridb.Rows(R + 3).Cells(19).Value = rs1.Tables(0).Rows(0)(2).ToString()

                                Next
                            End If
                            R = R + 3
                            rs.Dispose()
                            rs1.Dispose()
                        Next R
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CmbCoName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCoName.SelectedIndexChanged
        Dim rs, rs1 As New DataSet
        Dim sql As String, sqlded As String
        Dim i, m, c As Integer

        If CmbCoName.SelectedIndex = 0 Then
            grids.Dispose()
            gridheads()

            For R = 0 To grids.Rows.Count - 1
                grids.Rows(R).Cells(4).Value = CheckState.Unchecked
                grids.Rows(R).Cells(5).Value = CheckState.Unchecked
                grids.Rows(R).Cells(7).Value = CheckState.Unchecked
            Next
            grids.RowCount = 2
        End If
        If CmbCoName.SelectedIndex = 1 Then

            sql = "SELECT CoMst.CoID, CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN, CoMst.CoStatus FROM CoMst"
            rs = FetchDataSet(sql)
            If Not rs.Tables(0).Rows.Count Then
                grids.RowCount = rs.Tables(0).Rows.Count + 1

                For m = 0 To rs.Tables(0).Rows.Count - 1

                    sqlded = "SELECT DeductorTypeMst.DeductorTypeDescription From DeductorTypeMst WHERE deductortype=" & Chr(34) & rs.Tables(0).Rows(m)(4).ToString() & Chr(34)
                    rs1 = FetchDataSet(sqlded)

                    grids.Rows(m).Cells(9).Value = rs.Tables(0).Rows(m)(0).ToString()
                    grids.Rows(m).Cells(0).Value = rs.Tables(0).Rows(m)(1).ToString()
                    grids.Rows(m).Cells(1).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(m)(2).ToString()), "", rs.Tables(0).Rows(m)(2).ToString())   'IIf(IsNull(rs1(2), "", rs1(2)))
                    grids.Rows(m).Cells(2).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(m)(3).ToString()), "", rs.Tables(0).Rows(m)(3).ToString())
                    grids.Rows(m).Cells(3).Value = IIf(String.IsNullOrEmpty(rs1.Tables(0).Rows(0)(0).ToString()), "", rs1.Tables(0).Rows(0)(0).ToString())
                    grids.Rows(m).Cells(4).Value = CheckState.Checked
                    grids.Rows(m).Cells(5).Value = CheckState.Checked
                    grids.Rows(m).Cells(6).Value = CheckState.Checked
                    grids.Rows(m).Cells(7).Value = CheckState.Checked
                    grids.Rows(m).Cells(8).Value = CheckState.Checked
                    ' m = m + 1
                    rs1.Dispose()
                    sqlded = Nothing

                Next

            End If

        Else
            sql = "SELECT CoMst.CoID, CoMst.CoName, CoMst.CoTAN, CoMst.CoPAN, CoMst.CoStatus FROM CoMst" _
               & " where comst.coid =" & CmbCoName.SelectedIndex - 1
            rs = FetchDataSet(sql)
            If Not rs.Tables(0).Rows.Count Then
                grids.RowCount = rs.Tables(0).Rows.Count + 1

                For m = 0 To rs.Tables(0).Rows.Count - 1
                    For i = 1 To grids.Rows.Count - 1


                        grids.Rows(m).Cells(9).Value = rs.Tables(0).Rows(m)(0).ToString()
                        grids.Rows(m).Cells(0).Value = rs.Tables(0).Rows(m)(1).ToString()
                        grids.Rows(m).Cells(1).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(m)(2).ToString()), "", rs.Tables(0).Rows(m)(2).ToString())   'IIf(IsNull(rs1(2), "", rs1(2)))
                        grids.Rows(m).Cells(2).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(m)(3).ToString()), "", rs.Tables(0).Rows(m)(3).ToString())
                        grids.Rows(m).Cells(3).Value = IIf(String.IsNullOrEmpty(rs.Tables(0).Rows(m)(4).ToString()), "", rs.Tables(0).Rows(m)(4).ToString())
                        grids.Rows(m).Cells(4).Value = CheckState.Checked
                        grids.Rows(m).Cells(5).Value = CheckState.Checked
                        grids.Rows(m).Cells(6).Value = CheckState.Checked
                        grids.Rows(m).Cells(7).Value = CheckState.Checked
                        grids.Rows(m).Cells(8).Value = CheckState.Checked


                    Next
                Next
            End If

        End If
        grids.Visible = True
    End Sub

    Private Sub cmbrepttyp_Click(sender As Object, e As EventArgs) Handles cmbrepttyp.Click

    End Sub

    Private Sub ok()
        Dim c, d, m, N, i As Integer
        Dim R As Integer
        Dim sql As String
        Dim sqlwh As String
        Dim rsin, rsstatus As New DataSet

        Dim sqlstatus As String
        For R = 0 To grid.Rows.Count - 1
            If grid.Rows(R).Cells(4).Value = CheckState.Checked Then
                d = d + 1
                gridr.ColumnCount = d

                gridr.Rows(0).Cells(d - 1).Value = grid.Rows(R).Cells(2).Value

            Else
                d = d
            End If
        Next R

        sql = " SELECT CoMst.CoName AS CompanyName, CoMst.CoBrDiv AS Branch, CoMst.CoAdd1 & CoMst.CoAdd2 & CoMst.CoAdd3 & CoMst.CoAdd4 & CoMst.CoAdd5 & ' '& StateMst.StateName & ' - ' &CoMst.CoPin AS Address," _
            & " CoMst.CoStd & ' ' & CoMst.CoPhone as Phone  , CoMst.CoEmail as Email, CoMst.CoTAN as TAN, CoMst.CoPAN as PAN, CoMst.CoStatus as Status, CoMst.PR24Name & ' - ' &CoMst.PR24Desg as ContectPerson24, CoMst.PR24Add1 & CoMst.PR24Add2 & CoMst.PR24Add3 & CoMst.PR24Add4 & CoMst.PR24Add5 & ' ' & StateMst.StateName & ' - ' & CoMst.PR24Pin AS 24Address," _
            & " CoMst.PR24Std & '  ' & CoMst.PR24Phone as 24Phone, CoMst.PR24Email as 24Email,  CoMst.PR26Name & ' - ' & CoMst.PR26Desg as ContectPerson26, CoMst.PR26Add1 & CoMst.PR26Add2 & CoMst.PR26Add4 & CoMst.PR26Add5  & ' ' & StateMst.StateName & ' - ' & CoMst.PR26Pin AS 26Address, CoMst.PR26Std & ' ' & CoMst.PR26Phone as 26Phone, CoMst.PR26Email as 26Email," _
            & " CoMst.PR27Name & ' - ' & CoMst.PR27Desg as ContectPerson27, CoMst.PR27Add1 & CoMst.PR27Add2 & CoMst.PR27Add3 & CoMst.PR27Add4 & CoMst.PR27Add5 & ' '  & StateMst.StateName & ' - ' & CoMst.PR27Pin as  27Address," _
            & " CoMst.PR27Std & ' ' & CoMst.PR27Phone as 27Phone,  CoMst.PR27Email as 27Email,CoMst.PR27EName &' - ' & CoMst.PR27EDesg as ContectPerson27E, CoMst.PR27EAdd1 & CoMst.PR27EAdd2 & CoMst.PR27EAdd3 & CoMst.PR27EAdd4 & CoMst.PR27EAdd5 & ' ' & StateMst.StateName & ' - ' & CoMst.PR27EPin AS 27EAddress, CoMst.PR27EStd & ' ' & CoMst.PR27EPhone as 27EPhone ,CoMst.PR27EEmail as 27EEmail,comst.mobile as Mobile FROM CoMst INNER JOIN StateMst ON (CoMst.CoStateID = StateMst.StateID) AND (CoMst.PR24StateID = StateMst.StateID) AND (CoMst.PR27StateID = StateMst.StateID) AND (CoMst.PR27EStateID = StateMst.StateID)"

        rsin = FetchDataSet(sql)
        gridr.RowCount = rsin.Tables(0).Rows.Count + 1

        If rsin.Tables(0).Rows.Count > 0 Then
            For c = 0 To gridr.ColumnCount - 1

                For i = 0 To rsin.Tables(0).Columns.Count - 1
                    If rsin.Tables(0).Columns(i).ColumnName = gridr.Rows(0).Cells(c).Value Then
                        For m = 0 To gridr.Rows.Count - 2
                            If rsin.Tables(0).Columns(i).ColumnName = "ContectPerson26" Then

                                If gridr.Rows(m).Cells(c - 1).Value = rsin.Tables(0).Rows(0)(i).ToString() Then gridr.Rows(m).Cells(c).Value = "" Else gridr.Rows(m).Cells(c).Value = rsin.Tables(0).Rows(0)(i).ToString()
                            ElseIf rsin.Tables(0).Columns(i).ColumnName = "ContectPerson27E" Then

                                If (gridr.Rows(m).Cells(c - 1).Value = rsin.Tables(0).Rows(0)(i).ToString() Or gridr.Rows(m).Cells(c - 2).Value = rsin.Tables(0).Rows(0)(i).ToString()) Then gridr.Rows(m).Cells(c).Value = "" Else gridr.Rows(m).Cells(c).Value = rsin.Tables(0).Rows(0)(i).ToString()
                            Else
                                If rsin.Tables(0).Columns(i).ColumnName = "Status" Then
                                    sqlstatus = " SELECT DeductorTypeMst.DeductorTypeDescription FROM DeductorTypeMst where deductortype=" & Chr(34) & rsin.Tables(0).Rows(0)(i).ToString() & Chr(34)
                                    rsstatus = FetchDataSet(sqlstatus)
                                    gridr.Rows(m).Cells(c).Value = rsstatus.Tables(0).Rows(0)(0).ToString() & " "
                                    rsstatus.Dispose()
                                    rsstatus = Nothing
                                    sqlstatus = ""
                                Else
                                    gridr.Rows(m).Cells(c).Value = IIf(String.IsNullOrEmpty(rsin.Tables(0).Rows(m)(i).ToString()), " ", rsin.Tables(0).Rows(m)(i).ToString())
                                End If
                            End If

                            'rsin.MoveNext
                        Next m
                    End If
                Next i
                'rsin.MoveFirst
            Next c
        End If

        rsin.dispose
        rsin = Nothing


    End Sub

    Private Sub grid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles grid.CellEnter
        With grid
            If .Columns.Count = 2 Then .ColumnCount = 3
            If .ColumnCount = 0 Then .ColumnCount = 3
            If .ColumnCount = 1 Then .ColumnCount = 3

        End With
    End Sub


End Class