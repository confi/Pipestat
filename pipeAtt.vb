Public Structure pipeAtt
    Dim DN As String
    Dim material As String
    Dim length As Double
    Dim endPoint1Name As String
    Dim endPoint1Spec As String
    Dim endPoint2Name As String
    Dim endPoint2Spec As String
    Dim ID As Long


    Public ReadOnly Property items() As String()
        Get
            Dim str As String() = {ID, DN, material, length, endPoint1Name, endPoint1Spec, endPoint2Name, endPoint2Spec}
            Return str
        End Get

    End Property


    Public Function sortPipe() As String(,)
        Dim data(2, 5) As String

        '管子和管件的编号、材质相同，管件的数量和单位相同
        Dim i As Integer
        For i = 0 To 2
            data(i, 0) = items(0)
            data(i, 2) = items(2)
            If i > 0 Then
                data(i, 4) = 1
                data(i, 5) = "个"
            End If
        Next

        '设置管子属性
        data(0, 1) = "管子"
        data(0, 3) = items(1)
        data(0, 4) = items(3)
        data(0, 5) = "米"

        '设置管件属性

        data(1, 1) = items(4)
        data(1, 3) = items(5)
        data(2, 1) = items(6)
        data(2, 3) = items(7)

        Return data
    End Function

    Public ReadOnly Property pipe() As String()
        Get
            Dim myPipe(5) As String
            Dim i As Integer
            For i = 0 To 5
                myPipe(i) = sortPipe(0, i)
            Next
            Return myPipe
        End Get
    End Property

    Public ReadOnly Property endPoint() As String(,)
        Get
            Dim myEndPoint(1, 5) As String
            Dim i As Integer, j As Integer
            For i = 0 To 1
                For j = 0 To 5
                    myEndPoint(i, j) = sortPipe(i + 1, j)
                Next
            Next
            Return myEndPoint
        End Get
    End Property
End Structure
