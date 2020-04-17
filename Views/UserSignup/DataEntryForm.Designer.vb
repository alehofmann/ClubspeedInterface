<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataEntryForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataEntryForm))
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.vkKeyboard = New DCS_SAR_OnlineSalesInterface.VirtualKeyboard()
        Me.mtbData = New System.Windows.Forms.MaskedTextBox()
        Me.SuspendLayout()
        '
        'btnNext
        '
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(909, 488)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(127, 54)
        Me.btnNext.TabIndex = 4
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPrev.FlatAppearance.BorderSize = 0
        Me.btnPrev.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btnPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrev.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrev.Location = New System.Drawing.Point(739, 488)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(127, 54)
        Me.btnPrev.TabIndex = 3
        Me.btnPrev.Text = "<"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(12, 18)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(118, 30)
        Me.lblDescription.TabIndex = 0
        Me.lblDescription.Text = "Description"
        '
        'vkKeyboard
        '
        Me.vkKeyboard.BackColor = System.Drawing.Color.Black
        Me.vkKeyboard.BackgroundImage = CType(resources.GetObject("vkKeyboard.BackgroundImage"), System.Drawing.Image)
        Me.vkKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.vkKeyboard.Location = New System.Drawing.Point(12, 119)
        Me.vkKeyboard.Name = "vkKeyboard"
        Me.vkKeyboard.Size = New System.Drawing.Size(1024, 330)
        Me.vkKeyboard.TabIndex = 2
        '
        'mtbData
        '
        Me.mtbData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.mtbData.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtbData.Location = New System.Drawing.Point(12, 51)
        Me.mtbData.Name = "mtbData"
        Me.mtbData.Size = New System.Drawing.Size(1024, 28)
        Me.mtbData.TabIndex = 1
        Me.mtbData.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        '
        'DataEnterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 554)
        Me.Controls.Add(Me.vkKeyboard)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.mtbData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DataEnterForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DataEnterForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNext As Button
    Friend WithEvents btnPrev As Button
    Friend WithEvents lblDescription As Label
    Friend WithEvents vkKeyboard As DCS_SAR_OnlineSalesInterface.VirtualKeyboard
    Friend WithEvents mtbData As MaskedTextBox
End Class
