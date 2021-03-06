﻿

Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SearchRacersByPhoneView
        Inherits System.Windows.Forms.Form

        'Form overrIdes dispose to clean up the component list.
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SearchRacersByPhoneView))
            Me.lstRacers = New System.Windows.Forms.ListBox()
            Me.cmdCancel = New System.Windows.Forms.Button()
            Me.cmdOK = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.VirtualKeyboard = New DCS_SAR_OnlineSalesInterface.VirtualKeyboard()
            Me.lblSearchHits = New System.Windows.Forms.Label()
            Me.pnlSearch = New System.Windows.Forms.Panel()
            Me.txtSearch = New System.Windows.Forms.MaskedTextBox()
            Me.cmdSearch = New System.Windows.Forms.Button()
            Me.Panel1.SuspendLayout()
            Me.pnlSearch.SuspendLayout()
            Me.SuspendLayout()
            '
            'lstRacers
            '
            Me.lstRacers.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.lstRacers.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lstRacers.FormattingEnabled = True
            Me.lstRacers.ItemHeight = 21
            Me.lstRacers.Location = New System.Drawing.Point(37, 136)
            Me.lstRacers.Name = "lstRacers"
            Me.lstRacers.Size = New System.Drawing.Size(937, 147)
            Me.lstRacers.TabIndex = 2
            '
            'cmdCancel
            '
            Me.cmdCancel.BackColor = System.Drawing.Color.Transparent
            Me.cmdCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.FlatAppearance.BorderSize = 0
            Me.cmdCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cmdCancel.Location = New System.Drawing.Point(606, 663)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(160, 49)
            Me.cmdCancel.TabIndex = 2
            Me.cmdCancel.UseVisualStyleBackColor = False
            '
            'cmdOK
            '
            Me.cmdOK.BackColor = System.Drawing.Color.Transparent
            Me.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.cmdOK.FlatAppearance.BorderSize = 0
            Me.cmdOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.cmdOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdOK.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cmdOK.Location = New System.Drawing.Point(818, 663)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.Size = New System.Drawing.Size(156, 49)
            Me.cmdOK.TabIndex = 3
            Me.cmdOK.UseVisualStyleBackColor = False
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.VirtualKeyboard)
            Me.Panel1.Location = New System.Drawing.Point(12, 305)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(993, 334)
            Me.Panel1.TabIndex = 4
            '
            'VirtualKeyboard
            '
            Me.VirtualKeyboard.BackColor = System.Drawing.Color.Black
            Me.VirtualKeyboard.BackgroundImage = CType(resources.GetObject("VirtualKeyboard.BackgroundImage"), System.Drawing.Image)
            Me.VirtualKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.VirtualKeyboard.Location = New System.Drawing.Point(0, 0)
            Me.VirtualKeyboard.Name = "VirtualKeyboard"
            Me.VirtualKeyboard.Size = New System.Drawing.Size(993, 334)
            Me.VirtualKeyboard.TabIndex = 5
            Me.VirtualKeyboard.TabStop = False
            '
            'lblSearchHits
            '
            Me.lblSearchHits.AutoSize = True
            Me.lblSearchHits.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSearchHits.Location = New System.Drawing.Point(3, 68)
            Me.lblSearchHits.Name = "lblSearchHits"
            Me.lblSearchHits.Size = New System.Drawing.Size(0, 12)
            Me.lblSearchHits.TabIndex = 3
            '
            'pnlSearch
            '
            Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
            Me.pnlSearch.Controls.Add(Me.txtSearch)
            Me.pnlSearch.Controls.Add(Me.lblSearchHits)
            Me.pnlSearch.Location = New System.Drawing.Point(151, 21)
            Me.pnlSearch.Name = "pnlSearch"
            Me.pnlSearch.Size = New System.Drawing.Size(462, 33)
            Me.pnlSearch.TabIndex = 0
            '
            'txtSearch
            '
            Me.txtSearch.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtSearch.Location = New System.Drawing.Point(5, 3)
            Me.txtSearch.Mask = "(999) 000-0000"
            Me.txtSearch.Name = "txtSearch"
            Me.txtSearch.Size = New System.Drawing.Size(385, 29)
            Me.txtSearch.TabIndex = 4
            '
            'cmdSearch
            '
            Me.cmdSearch.BackColor = System.Drawing.Color.Transparent
            Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cmdSearch.Location = New System.Drawing.Point(702, 21)
            Me.cmdSearch.Name = "cmdSearch"
            Me.cmdSearch.Size = New System.Drawing.Size(174, 68)
            Me.cmdSearch.TabIndex = 5
            Me.cmdSearch.UseVisualStyleBackColor = False
            '
            'SearchRacersByPhoneView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.DCS.KioskV15.ClubSpeedInterface.My.Resources.Resources.ByPhone
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(1008, 729)
            Me.ControlBox = False
            Me.Controls.Add(Me.cmdSearch)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.lstRacers)
            Me.Controls.Add(Me.cmdOK)
            Me.Controls.Add(Me.cmdCancel)
            Me.Controls.Add(Me.pnlSearch)
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Name = "SearchRacersByPhoneView"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Panel1.ResumeLayout(False)
            Me.pnlSearch.ResumeLayout(False)
            Me.pnlSearch.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents lstRacers As ListBox
        Friend WithEvents cmdCancel As Button
        Friend WithEvents cmdOK As Button
        Friend WithEvents Panel1 As Panel
        Friend WithEvents lblSearchHits As Label
        Friend WithEvents pnlSearch As Panel
        Friend WithEvents VirtualKeyboard As DCS_SAR_OnlineSalesInterface.VirtualKeyboard
        Friend WithEvents cmdSearch As Button
        Friend WithEvents txtSearch As MaskedTextBox
    End Class
End Namespace