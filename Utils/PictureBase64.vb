Module PictureBase64

    Friend Enum ImageFormats As Integer
        if_Bmp = 1
        if_EMF = 2
        if_Exif = 3
        if_GIF = 4
        if_Icon = 5
        if_JPG = 6
        if_PNG = 7
        if_TIFF = 8
        if_WMF = 9
    End Enum

    Friend Function JSONDecodeBase64ToBitmap(ByVal picData As String) As System.Drawing.Bitmap

        'Must return just the 'base64Data' portion. Reference:
        '   data:image/{formatName};base64,{base64Data}
        Return (Image.FromStream(New IO.MemoryStream(Convert.FromBase64String(picData.Substring(picData.IndexOf(",") + 1)))))

    End Function

    Friend Function JSONEncodeBitmapToBase64(ByVal picture As Bitmap,
                                             Optional ByVal desiredFormat As ImageFormats = ImageFormats.if_JPG
                                            ) As String

        Dim retVal As String = String.Empty,
            imgFormat As Imaging.ImageFormat = Imaging.ImageFormat.Jpeg,
            formatNme As String = "jpg"

        Using memStream As New IO.MemoryStream

            Select Case desiredFormat
                Case ImageFormats.if_Bmp
                    imgFormat = Imaging.ImageFormat.Bmp
                    formatNme = "bmp"
                Case ImageFormats.if_EMF
                    imgFormat = Imaging.ImageFormat.Emf
                    formatNme = "emf"
                Case ImageFormats.if_Exif
                    imgFormat = Imaging.ImageFormat.Exif
                    formatNme = "exif"
                Case ImageFormats.if_GIF
                    imgFormat = Imaging.ImageFormat.Gif
                    formatNme = "gif"
                Case ImageFormats.if_Icon
                    imgFormat = Imaging.ImageFormat.Icon
                    formatNme = "icon"
                Case ImageFormats.if_JPG
                    imgFormat = Imaging.ImageFormat.Jpeg
                    formatNme = "jpg"
                Case ImageFormats.if_PNG
                    imgFormat = Imaging.ImageFormat.Png
                    formatNme = "png"
                Case ImageFormats.if_WMF
                    imgFormat = Imaging.ImageFormat.Wmf
                    formatNme = "wmf"
            End Select

            picture.Save(memStream, imgFormat)
            memStream.Position = 0 'Reset stream pointer to stream start

            Using binReader As New IO.BinaryReader(memStream)
                Dim bytes As Byte() = binReader.ReadBytes(memStream.Length)
                retVal = Convert.ToBase64String(bytes, 0, bytes.Length)
            End Using

        End Using

        Return ($"data:image/{formatNme};base64,{retVal}")

    End Function

End Module
