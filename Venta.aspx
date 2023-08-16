<%@ Page Title="Detalle de Pago" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Venta.aspx.cs" Inherits="Venta" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   

    <div class="row">
        <div class="col-md-7" style="padding-left:40px;">
               <hgroup class="title">
              <h1>Resumen de tu compra:</h1>
             </hgroup>

     <table >
        <tr>
            <td  style="text-align:right; vertical-align:central; padding-left:5px;">
                 <span style="font-size:x-large; font-family:Bahnschrift; color:midnightblue;">No. de Autorizacion:</span>
            </td>
            <td style="text-align:left;vertical-align:bottom; ">
                <asp:TextBox ID="txtNoAutorizacion" runat="server" Height="23px" Width="89px" MaxLength="10" onkeypress="return isNumberKey(event)" TabIndex="1" style="font-size:x-large; font-family:Bahnschrift; color:midnightblue;"></asp:TextBox>
            </td>
             <td style="text-align:left; vertical-align:bottom; padding-left:5px;">
               &nbsp; &nbsp; &nbsp; &nbsp; <asp:Button ID="btnValidar" ForeColor="White"  OnClick="btnValidar_Click" runat="server" Font-Bold="true" Height="40px" Width="110px" BorderColor="Black" Text="Pagar" BackColor="Green" style="margin-left: 0px" TabIndex="3"   />
    
            </td>
            <td>

            </td>
        </tr>
        
        <tr id="trDisponible" runat="server" >
            <td style="text-align:right;vertical-align:bottom; padding-left:5px;">
                 <span style="font-size:x-large; font-family:Bahnschrift; color:midnightblue;">Total de tu Compra:</span>
            </td>
            <td style="text-align:left;vertical-align:bottom;">
                <asp:Label ID="lblTotal" runat="server" ForeColor="Blue" Text="$0.0" Font-Size="X-Large" Font-Bold="True"></asp:Label> 
            </td>
            <td style="text-align:right; vertical-align:bottom; padding-left:5px;">
                
            </td>
             <td>

            </td>
        </tr>




        </table>

            
         <table style="padding-left:80px">
        
         <%--Playstation Network 10--%>
         <tr  runat="server" style="padding-bottom:20px; padding-top:20px; padding-left:10px;" >
            
           <%--  <td>
            </td>--%>
             
            <td style="padding-bottom:20px; padding-top:20px; text-align:center; vertical-align:middle;" >
                    <img src="<%= productImage  %>" class="img-thumbnail" style="max-width:100px;"/>

            </td>
            <td style="vertical-align:text-top;  padding-top:30px;" >

                 <span style="font-size:x-large; font-family:Bahnschrift; color:midnightblue;"><%= productName %></span>
                <table style="width:91%; height:auto;">                 

                      <tr>
                        <td style="text-align:right; vertical-align:bottom;">
                           
                        </td>
                          <td style="text-align:left; vertical-align:bottom;">
                            
                          
                      
                          </td>
                          <td></td>
                    </tr>
                    <tr>
                        <td style="text-align:right; vertical-align:central">
                            <asp:Label ID="lblImporte" Visible="False" runat="server" ForeColor="#009933" Text="0.0" Font-Size="X-Large" Font-Bold="True"></asp:Label>
                       </td>
                        <td style="text-align:left; vertical-align:bottom;">
                             <asp:Label ID="lblDisponible" Visible="False"  runat="server" ForeColor="#0066FF" Font-Size="X-Large" Font-Bold="True"></asp:Label> 
                 

                        </td>

                    </tr>

                </table>

            </td>

              <td>
              <%--<asp:TextBox ID="txtCodigosPSN10"  runat="server" Height="120px" Width="217px" MaxLength="500" onkeypress="return isNumberKey(event)" ReadOnly="False" TextMode="MultiLine" Font-Bold="True" ForeColor="Red"></asp:TextBox>--%>
             </td>
        </tr>




        </table>


        </div>
        <div class="col-md-5">
            &nbsp;
            <br />
            <img style="height:30%;" class="img-thumbnail"  alt="Responsive image" src="Images/oxxoPay.jpeg" onclick="window.open(this.src)" />
        </div>

    </div>
  






    <br />
    <br />
    <br />
    <br />



</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">


            <script type="text/javascript">


                function isNumberKey(e) {

                    if (window.event) // IE 
                    {
                        if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13) {
                            event.returnValue = false;
                            return false;

                        }

                        
                        if (e.keyCode == 13) {
                            __doPostBack('<%=btnValidar.UniqueID %>', "");
                            return false;
                            // document.getElementById("btnValidar").onclick();
                             }


                    }
                    else { // Fire Fox
                        if ((e.which < 48 || e.which > 57) & e.which != 8 & e.keyCode != 13) {
                            e.preventDefault();
                            return false;

                        }

                        if (e.keyCode == 13) {
                            __doPostBack('<%=btnValidar.UniqueID %>', "");
                            return false;
                             }

                    }
                }



                function isEnterKey(e) {

                    if (window.event) // IE 
                    {



                        if (e.keyCode == 13) {
                            __doPostBack('<%=btnValidar.UniqueID %>', "");
                            return false;

                        }


                    }
                    else { // Fire Fox


                        if (e.keyCode == 13) {
                            __doPostBack('<%=btnValidar.UniqueID %>', "");
                            return false;
                        }

                    }
                }

        </script>

</asp:Content>
