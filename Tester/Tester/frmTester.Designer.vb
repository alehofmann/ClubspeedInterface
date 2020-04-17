<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTester
    Inherits System.Windows.Forms.Form

    'Form overrIdes dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected OverrIdes Sub Dispose(ByVal disposing As Boolean)
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
        Me.cmdCreate = New System.Windows.Forms.Button()
        Me.cmdPresale = New System.Windows.Forms.Button()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.cmdCommitPresale = New System.Windows.Forms.Button()
        Me.cmdRollbackPresale = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdCreate
        '
        Me.cmdCreate.Location = New System.Drawing.Point(12, 12)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(75, 42)
        Me.cmdCreate.TabIndex = 0
        Me.cmdCreate.Text = "Create Plugin"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'cmdPresale
        '
        Me.cmdPresale.Enabled = False
        Me.cmdPresale.Location = New System.Drawing.Point(93, 12)
        Me.cmdPresale.Name = "cmdPresale"
        Me.cmdPresale.Size = New System.Drawing.Size(75, 42)
        Me.cmdPresale.TabIndex = 1
        Me.cmdPresale.Text = "Select Race && Racers"
        Me.cmdPresale.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStatus.Location = New System.Drawing.Point(12, 149)
        Me.txtStatus.Multiline = True
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(377, 104)
        Me.txtStatus.TabIndex = 5
        '
        'txtResult
        '
        Me.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResult.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResult.Location = New System.Drawing.Point(12, 60)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ReadOnly = True
        Me.txtResult.Size = New System.Drawing.Size(377, 83)
        Me.txtResult.TabIndex = 4
        '
        'cmdCommitPresale
        '
        Me.cmdCommitPresale.Enabled = False
        Me.cmdCommitPresale.Location = New System.Drawing.Point(174, 12)
        Me.cmdCommitPresale.Name = "cmdCommitPresale"
        Me.cmdCommitPresale.Size = New System.Drawing.Size(75, 42)
        Me.cmdCommitPresale.TabIndex = 2
        Me.cmdCommitPresale.Text = "Make Reservation"
        Me.cmdCommitPresale.UseVisualStyleBackColor = True
        '
        'cmdRollbackPresale
        '
        Me.cmdRollbackPresale.Enabled = False
        Me.cmdRollbackPresale.Location = New System.Drawing.Point(314, 12)
        Me.cmdRollbackPresale.Name = "cmdRollbackPresale"
        Me.cmdRollbackPresale.Size = New System.Drawing.Size(75, 42)
        Me.cmdRollbackPresale.TabIndex = 3
        Me.cmdRollbackPresale.Text = "Kill Reservation"
        Me.cmdRollbackPresale.UseVisualStyleBackColor = True
        '
        'frmTester
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 265)
        Me.Controls.Add(Me.cmdRollbackPresale)
        Me.Controls.Add(Me.cmdCommitPresale)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.cmdPresale)
        Me.Controls.Add(Me.cmdCreate)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTester"
        Me.Text = "Tester"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdCreate As Button
    Friend WithEvents cmdPresale As Button
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents txtResult As TextBox
    Friend WithEvents cmdCommitPresale As Button
    Friend WithEvents cmdRollbackPresale As Button
End Class
