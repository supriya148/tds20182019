Imports System.Data.OleDb

Public Class clsForm16MoreDetails
    '    'local variable(s) to hold property value(s)
    Private mvarF16ID As Long 'local copy
    'To fire this event, use RaiseEvent with the following syntax:
    'RaiseEvent AfterDelete[(arg1, arg2, ... , argn)]
    Public Event AfterDelete()
    'To fire this event, use RaiseEvent with the following syntax:
    'RaiseEvent AfterSave[(arg1, arg2, ... , argn)]
    Public Event AfterSave()
    'To fire this event, use RaiseEvent with the following syntax:
    'RaiseEvent BeforeDelete[(arg1, arg2, ... , argn)]
    Public Event BeforeDelete(Cancel As Boolean)
    'To fire this event, use RaiseEvent with the following syntax:
    'RaiseEvent BeforeSave[(arg1, arg2, ... , argn)]
    Public Event BeforeSave(Cancel As Boolean)
    'To fire this event, use RaiseEvent with the following syntax:
    'RaiseEvent PrepareDataForSave[(arg1, arg2, ... , argn)]
    Public Event PrepareDataForSave(Cancel As Boolean, R As Long, grd As DataGridView)
    Public Event PrepareDataForSaveOtherInc(Cancel As Boolean, R As Long)
    Private mvarTypeOfDetail As String 'local copy
    Private mvarParticulars As String 'local copy
    Private mvarGrossAmt As Double 'local copy
    Private mvarQualifyAmt As Double 'local copy
    Private mvarDeductibleAmt As Double 'local copy
    'local variable(s) to hold property value(s)
    Private mvarID As Long 'local copy

    Public Property ID As String
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.Category
            ID = mvarID
        End Get
        Set(ByVal vData As String)
            mvarID = vData
        End Set
    End Property


    Public Property DeductibleAmt As String
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.DPin
            DeductibleAmt = mvarDeductibleAmt
        End Get
        Set(ByVal vData As String)
            mvarDeductibleAmt = vData
        End Set
    End Property


    Public Property QualifyAmt As String
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.DPin
            QualifyAmt = mvarQualifyAmt
        End Get
        Set(ByVal vData As String)
            mvarQualifyAmt = vData
        End Set
    End Property

    Public Property GrossAmt As String
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.DPin
            GrossAmt = mvarGrossAmt
        End Get
        Set(ByVal vData As String)
            mvarGrossAmt = vData
        End Set
    End Property

    Public Property Particulars As String
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.Category
            Particulars = mvarParticulars
        End Get
        Set(ByVal vData As String)
            mvarParticulars = vData
        End Set
    End Property


    Public Property TypeOfDetail As String
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.Category
            TypeOfDetail = mvarTypeOfDetail
        End Get
        Set(ByVal vData As String)
            mvarTypeOfDetail = vData
        End Set
    End Property



    '    Public Sub Update(F16Details As clsForm16Details)
    '    End Sub

    Public Function MaxID() As Long
        Dim rs As New DataSet
        Dim sql As String
        sql = "Select Max(id) as Id From Form16MoreDetails"
        rs = FetchDataSet(sql)
        If Not String.IsNullOrEmpty(rs.Tables(0).Rows(0)("ID").ToString()) Then
            MaxID = rs.Tables(0).Rows(0)("ID").ToString()
        Else
            MaxID = 0
        End If

        rs.Dispose()
        rs = Nothing
    End Function

    '    Public Sub Insert(F16Details As clsForm16Details)
    '    End Sub

    Public Function Delete(ID As Long) As Boolean
        Dim cnl As Boolean
        Dim transaction As OleDb.OleDbTransaction
        Dim cmd As New OleDb.OleDbCommand
        ' On Error GoTo DelErr
        cnl = False : RaiseEvent BeforeDelete(cnl)

        If cnl = True Then
            Delete = False
            Exit Function
        End If
        Dim sql As String

        sql = "Delete * From Form16MoreDetails Where F16ID = " & ID

        cmd.Connection = cn
        transaction = cn.BeginTransaction()
        cmd.Transaction = transaction

        Try
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()
            Delete = True
            transaction.Commit()

        Catch ex As Exception
            MessageBox.Show("Message:", ex.Message)

            transaction.Rollback()

            Delete = False
        End Try

        cmd.Dispose()
        transaction.Dispose()
DelErr:
        MsgBox(Err.Description, , Err.Number)
    End Function

    Public Sub Fetch(ByVal ID As Long)
    End Sub


    Public Property F16ID As String
        Get
            'used when retrieving value of a property, on the right side of an assignment.
            'Syntax: Debug.Print X.DId
            F16ID = mvarF16ID
        End Get
        Set(ByVal vData As String)
            mvarF16ID = vData
        End Set
    End Property
    Public Function Insert(F16MoreDetails As clsForm16MoreDetails, R As Long, grd As DataGridView, TypeOfDetail As String) As Boolean ' ID As Long, F16ID As Long, TypeOfDetail As String, Particulars As String, GrossAmt As Double, QualifyAmt As Double, DeductibleAmt As Double) 
        'On Error GoTo InErr
        Dim sql As String, cnl As Boolean


        cnl = False : RaiseEvent BeforeSave(cnl)
        If cnl = True Then
            Insert = False
            Exit Function
        End If
        cnl = False : RaiseEvent PrepareDataForSave(cnl, R, grd)
        If cnl = True Then
            Insert = False
            Exit Function
        End If
        With F16MoreDetails
            .ID = .MaxID + 1
            sql = "Insert into Form16MoreDetails (ID, F16ID,TypeOfDetail,Particulars,GrossAmt,QualifyAmt,DeductibleAmt) " &
           " Values ( " _
           & .ID & "," _
           & .F16ID & ",'" _
           & TypeOfDetail & "','" _
           & IIf(Trim(.Particulars) = "", "", .Particulars) & "'," _
           & IIf(.GrossAmt = 0, 0, .GrossAmt) & "," _
           & IIf(.QualifyAmt = 0, 0, .QualifyAmt) & "," _
           & IIf(.DeductibleAmt = 0, 0, .DeductibleAmt) & ")"
        End With
        Dim cmd As New OleDbCommand
        Try
            cmd.CommandText = sql
            cmd.Connection = cn
            cmd.ExecuteNonQuery()
            Insert = True
        Catch ex As Exception
            Dim merror As String
            merror = ex.Message
            MsgBox(merror)
            Insert = False
        End Try

    End Function


End Class

