<%@ Page Title="GiftCardsMX.com - USA" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


 
<asp:Content  runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <div class="header-section">
        <div class="row" style="vertical-align:central; text-align:left; height:85px;" >
            
            <div class="col-sm-3" style="text-align:right; padding-left:30px;">     
                   &nbsp;&nbsp;    &nbsp;  <span>&nbsp;</span><asp:TextBox ID="txtBuscar"  class="form-control" runat="server" Height="35px" Width="100%"  onkeypress="return isNumberKey(event)" BorderColor="#00539F" BorderStyle="Solid" Font-Bold="False" Font-Size="Large" ForeColor="#0033CC"  ></asp:TextBox>
            </div>
            <div class="col-sm-1 my-auto" style="text-align:left;">
                  
                <input type="button" class="btn btn-primary btn-lg" value="Buscar" title="Buscar" onclick="filterSearch();" />
            </div>
          <div class="col-sm-2 my-auto">
                &nbsp;
            </div>
            <div class="col-sm-5 my-auto" style="text-align:right;">               
                <div class="fb-like" data-href="https://www.facebook.com/ENVIOUSAMEX" data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>&nbsp;
              
                    <a href="https://www.facebook.com/ENVIOUSAMEX" target="_blank"><img class="img-thumbnail" src="Images/facebook-icon.png" title="Sigueme en Facebook"  height="30" width="30"/></a>&nbsp;
                    <a href="https://twitter.com/giftcardsmx" target="_blank"><img class="img-thumbnail" src="Images/twitter.png" title="Sigueme en Twitter" height="30" width="30"/></a>&nbsp;
                    <a href="https://www.youtube.com/channel/UCM7JRMAeoAwciy9ZhrmJflg" target="_blank"><img class="img-thumbnail" src="Images/youtube.png" title="Suscribete en YouTube"  height="30" width="30"/></a>
                                 
            </div>                    
                    
         </div>
    </div>

</asp:Content>





<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


    
    <br />
    <br />


    <asp:Repeater ID="rptProducts" runat="server">
    <HeaderTemplate>
  
    </HeaderTemplate>
    <ItemTemplate>
      <div class="row" id="<%# Eval("ProductCode")%>">
         <div class="col-md-2"> </div>

          <div class="col-md-2">
           <img src="<%# Eval("ProductImage") %>" class="img-thumbnail" width="180" height="243" />
          </div>
          <div class="col-md-6 my-auto" > 

              <span id="label<%# Eval("ProductCode")%>" style="font-size:x-large; font-family:Bahnschrift; color:midnightblue;"><%# Eval("ProductName") %></span>
             
               <br />
               <br />
              <span style="font-size:x-large; font-family:Bahnschrift; color:midnightblue;" >Precio: </span>
              
              <asp:Label  runat="server" style="font-size:x-large; font-family:Bahnschrift; color:midnightblue;" Text='<%# FormatPrice(Eval("ProductPrice").ToString())%>'></asp:Label>
              <br /><br />
              &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<a href="Venta.aspx?ID=<%# Eval("ProductCode")%>" class="btn btn-primary" role="button" aria-pressed="true">Comprar</a>
               <br />
               <br />
           </div>
          <div class="col-md-2"> </div>
     </div>
        <br />
    </ItemTemplate>
    <FooterTemplate>
        
    </FooterTemplate>
</asp:Repeater>



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



      
            <script type="text/javascript">

                $(document).ready(function () {

                });

                function isNumberKey(e) {

                    if (window.event) // IE 
                    {
                        if (e.keyCode == 13) {
                            filterSearch();
                            return false;
                        }
                    }
                    else { // Fire Fox

                        if (e.keyCode == 13) {
                            filterSearch();
                            return false;
                        }

                    }
                }


                function filterSearch() {


                    var divsToHide = document.getElementsByClassName("row");

                    var searchTEXT = document.getElementById("FeaturedContent_txtBuscar");

                    for (var i = 0; i < divsToHide.length; i++) {

                        if (divsToHide[i].id != '') {

                            var productDesc = document.getElementById("label" + divsToHide[i].id);

                            if (productDesc.outerText.toLowerCase().includes(searchTEXT.value.toLowerCase()))
                                divsToHide[i].style.display = "";
                            else
                                divsToHide[i].style.display = "none";
                        }


                    }

                }

            </script>

</asp:Content>







