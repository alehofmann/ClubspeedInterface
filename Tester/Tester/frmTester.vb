Public Class frmTester

    Private _plug As DCS.KioskV15.ClubSpeedInterface.Plugin,
            _purchaseParamsIn As System.Collections.Generic.IDictionary(Of String, String),
            _purchaseParamsOut As System.Collections.Generic.IDictionary(Of String, String)

    Private Sub frmTester_Load(ByVal sender As Object,
                               ByVal e As EventArgs
                              ) Handles Me.Load

        Me.Text = "DCS.ClubSpeedKioskInterface Test Harness"
        Me.Location = New System.Drawing.Point(0, 0)
        log4net.Config.XmlConfigurator.Configure()

    End Sub

    Private Sub cmdCreate_Click(ByVal sender As Object,
                                    ByVal e As EventArgs
                                   ) Handles cmdCreate.Click

        Me.cmdCreate.Enabled = False
        Me.cmdPresale.Enabled = False
        Me.cmdCommitPresale.Enabled = False
        Me.cmdRollbackPresale.Enabled = False

        Try

            Me.Cursor = Cursors.WaitCursor
            Me.txtResult.Text = $"Creating plugin...{System.Environment.NewLine}"

            Me._plug = New DCS.KioskV15.ClubSpeedInterface.Plugin
            txtStatus.Text = ">>> Create done OK <<<"
            Me.cmdPresale.Enabled = True

        Catch ex As Exception
            Me.txtStatus.Text = $"Error creating plugin: {ex.ToString()}."
        End Try

        Me.cmdCreate.Enabled = True
        Me.txtResult.Text &= $"Create finished.{System.Environment.NewLine}"
        Me.Cursor = Cursors.Arrow

    End Sub

    Private Sub cmdPresale_Click(ByVal sender As Object,
                                 ByVal e As EventArgs
                                ) Handles cmdPresale.Click

        Me.txtResult.Text = System.String.Empty
        Me._purchaseParamsIn = New Dictionary(Of String, String)
        Me._purchaseParamsOut = New Dictionary(Of String, String)

        Cursor = Cursors.WaitCursor
        Me.txtResult.Text = $"Preselling...{System.Environment.NewLine}"

        If (Me._plug.PurchaseProduct(_purchaseParamsIn, _purchaseParamsOut)) Then

            DumpParametersStr(_purchaseParamsIn, _purchaseParamsOut)
            txtStatus.Text = ">>> Presale done OK <<<"

            Me.cmdCommitPresale.Enabled = True
            Me.cmdRollbackPresale.Enabled = True

        Else
            Me.txtStatus.Text = Me._plug.LastErrorString
        End If

        Me.txtResult.Text &= $"Presale finished.{System.Environment.NewLine}"
        Cursor = Cursors.Arrow

    End Sub

    Private Sub cmdCommitPresale_Click(ByVal sender As Object,
                                       ByVal e As EventArgs
                                      ) Handles cmdCommitPresale.Click

        Dim paramsIn As System.Collections.Generic.IDictionary(Of String, String) = _purchaseParamsOut,
            paramsOut As New System.Collections.Generic.Dictionary(Of String, Object)

        Cursor = Cursors.WaitCursor
        Me.txtResult.Text = $"Committing transaction...{System.Environment.NewLine}"

        If (_plug.FulfillTransaction(paramsIn, paramsOut)) Then

            DumpParametersObj(paramsIn, paramsOut)
            txtStatus.Text = ">>> Fulfill done OK <<<"

            Me.cmdRollbackPresale.Enabled = True

        Else
            txtStatus.Text = _plug.LastErrorString
        End If

        Me.txtResult.Text &= $"Commit finished.{System.Environment.NewLine}"
        Cursor = Cursors.Arrow

    End Sub

    Private Sub cmdRollbackPresale_Click(sender As Object, e As EventArgs) Handles cmdRollbackPresale.Click


        Dim paramsIn As System.Collections.Generic.IDictionary(Of String, String) = _purchaseParamsOut,
            paramsOut As New System.Collections.Generic.Dictionary(Of String, Object)

        Cursor = Cursors.WaitCursor
        Me.txtResult.Text = $"Rolling transaction back...{System.Environment.NewLine}"

        If (_plug.RollbackTransaction(paramsIn, paramsOut)) Then

            DumpParametersObj(paramsIn, paramsOut)
            txtStatus.Text = ">>> Rollback done OK <<<"

        Else
            txtStatus.Text = _plug.LastErrorString
        End If

        Me.txtResult.Text &= $"Rollback finished.{System.Environment.NewLine}"
        Cursor = Cursors.Arrow

    End Sub

    Private Sub DumpParametersStr(ByVal paramsIn As System.Collections.Generic.IDictionary(Of String, String),
                                  ByVal paramsOut As System.Collections.Generic.IDictionary(Of String, String)
                                 )

        For Each kvp As KeyValuePair(Of String, String) In paramsIn
            Me.txtResult.Text &= $"  (tx) {kvp.Key}:= '{kvp.Value}'{System.Environment.NewLine}"
        Next kvp

        For Each kvp As KeyValuePair(Of String, String) In paramsOut
            Me.txtResult.Text &= $"  (rx) {kvp.Key}:= '{kvp.Value}'{System.Environment.NewLine}"
        Next kvp

    End Sub

    Private Sub DumpParametersObj(ByVal paramsIn As System.Collections.Generic.IDictionary(Of String, String),
                                  ByVal paramsOut As System.Collections.Generic.IDictionary(Of String, Object)
                                 )

        For Each kvp As KeyValuePair(Of String, String) In paramsIn
            Me.txtResult.Text &= $"  (tx) {kvp.Key}:= '{kvp.Value}'{System.Environment.NewLine}"
        Next kvp

        For Each kvp As KeyValuePair(Of String, Object) In paramsOut
            Me.txtResult.Text &= $"  (rx) {kvp.Key}:= '{kvp.Value}'{System.Environment.NewLine}"
        Next kvp

    End Sub

End Class