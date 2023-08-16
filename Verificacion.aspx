<%@ Page Title="Deposito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Verificacion.aspx.cs" Inherits="Verificacion" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>    Verifica tu deposito</h1>
    </hgroup>





    
    <table style="padding-left:80px">
        <tr>
            <td  style="text-align:right; vertical-align:central; padding-left:20px;">
                 <h3>No. de Autorizacion:</h3>
            </td>
            <td style="text-align:right;vertical-align:bottom; ">
                <asp:TextBox ID="txtNoAutorizacion" runat="server" Height="23px" Width="89px" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </td>
             <td>

            </td>
        </tr>

        <tr>
            <td style="text-align:right;vertical-align:bottom; padding-left:20px;">
                 <h3>Disponible:</h3>
            </td>
            <td style="text-align:right;vertical-align:bottom;">
                <asp:Label ID="lblImporte" Visible="False" runat="server" ForeColor="#009933" Text="0.0" Font-Size="X-Large" Font-Bold="True"></asp:Label> 
            </td>
            <td style="text-align:right; vertical-align:bottom; padding-left:20px;">
                <asp:Label ID="lblDisponible" Visible="False"  runat="server" ForeColor="#0066FF" Font-Size="X-Large" Font-Bold="True"></asp:Label> 
            </td>
        </tr>

                <tr>
            <td style="text-align:right;vertical-align:bottom;">

            </td>
            <td>
                
            </td>
        </tr>




                <tr>
            <td style="text-align:right;vertical-align:bottom;">

            </td>
            <td>
                 <asp:Button ID="btnValidar" class="btn btn-primary"  ForeColor="White"  OnClick="btnValidar_Click" runat="server" Font-Bold="true" Height="40px" Width="99px" BorderColor="Black" Text="Verificar" BackColor="#6699FF"   />
    
            </td>
        </tr>


    </table>

 
    <br />
    <br />

     <br />
    <br />

    <br />
    <br />

     <br />
    <br />

      <h2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pasos para comprar</h2>
    <ol class="round">
        <li class="one">         
        <h5>Selecciona la Gift Card de tu preferencia.</h5>&nbsp;
        </li>
        <li class="two">
            <h5>Realiza el pago en cualquier tienda <span style="color:red; font-weight:bold;">OXXO</span>.</h5>
            Contactame para otros medios de pago disponibles (bitcoin, transferencia, etc.)</li>
        <li class="three">
            <h5>Ingresa el folio de autorizacion de tu recibo o Contactame.</h5>
                Recibe tu codigo al instante!
        </li>
    </ol>

</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">


            <script type="text/javascript">
                function isNumberKey(e) {


                  //  debugger;

                    if (window.event) // IE 
                    {
                        if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 68 & e.keyCode != 100 & e.keyCode != 13) {
                            event.returnValue = false;
                            return false;

                        }


                        if (e.keyCode == 13)
                        {

                            if (document.getElementById("MainContent_btnValidar") != null)
                            {
                           
                                __doPostBack('<%=btnValidar.UniqueID %>', "");
                                return false;

                            }
                            else
                            {
                                return false;
                            }

                        }


                    }
                    else { // Fire Fox
                        if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 68 & e.keyCode != 100 & e.keyCode != 13) {
                            return false;
                                                    }

                        if (e.keyCode == 13) {

                            if (document.getElementById("MainContent_btnValidar") != null)
                            {
                                __doPostBack('<%=btnValidar.UniqueID %>', "");
                                return false;
                            }
                            else
                            {
                                return false;
                            }

                        }

                    }
                }

        </script>

</asp:Content>
