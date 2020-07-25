'Opening ports on a router that supports uPnP
Imports NATUPNPLib
Imports upnp

Public Class Form1
    Dim upnpnat As New NATUPNPLib.UPnPNAT
    Dim mappings As NATUPNPLib.IStaticPortMappingCollection = upnpnat.StaticPortMappingCollection
    Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
    Dim internalip As String = h.AddressList.GetValue(0).ToString
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim b As New upnp.portmapper
            Try
                For Each portmapping As NATUPNPLib.IStaticPortMapping In mappings
                    Dim lstring() As String = {portmapping.Protocol, portmapping.ExternalPort, portmapping.Description}
                    Dim litem As New ListViewItem(lstring)
                    ListView1.Items.Add(litem)
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox("UPnP doesn't seem to be enabled on your router")
            End Try
        Catch
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mappings.Add(TextBox3.Text, ComboBox1.Text, TextBox3.Text, internalip, True, TextBox2.Text)
        ListView1.Items.Clear()
        Try
            For Each portmapping As NATUPNPLib.IStaticPortMapping In mappings
                Dim lstring() As String = {portmapping.Protocol, portmapping.ExternalPort, portmapping.Description}
                Dim litem As New ListViewItem(lstring)
                ListView1.Items.Add(litem)
        Next
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        For Each item As ListViewItem In ListView1.SelectedItems
            Dim portmove As String
            Dim protocolmove As String
            protocolmove = item.Text
            portmove = item.SubItems(1).Text
            mappings.Remove(portmove, protocolmove)
        Next
        ListView1.Items.Clear()
        Try
            For Each portmapping As NATUPNPLib.IStaticPortMapping In mappings
                Dim lstring() As String = {portmapping.Protocol, portmapping.ExternalPort, portmapping.Description}
                Dim litem As New ListViewItem(lstring)
                ListView1.Items.Add(litem)
            Next
        Catch
        End Try
    End Sub
End Class
