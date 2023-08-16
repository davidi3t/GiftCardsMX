<%@ Page Title="Contactame" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row" style="padding-left:40px">
        <div class="col-md-12">
                <section class="contact">
        <h2>Contactame</h2>&nbsp;



        <p style="padding-left:20px">
            <img src="Images/iphone_icon.png"  width="14" height="14" />
            <span class="label">Mobile:</span>
            <span>+1 (916) 741 93 57</span>
        </p>
       
        <p style="padding-left:20px">
            <img src="Images/EMAIL_ICON.jpg" width="14" height="14" />
            <span class="label">E-mail:</span>
            <a href="mailto:giftcardsmx@hotmail.com">giftcardsmx@hotmail.com</a>
        </p>
        
         <p style="padding-left:20px">
             <img src="Images/whatsapp.jpg"  width="14" height="14" />
             <span class="label">Whatsapp</span>
             <a  href="https://web.whatsapp.com/send?+19167419357" target="_blank">Enviame Whatsapp</a>
        </p>
       
        <p style="padding-left:20px">
            <img src="Images/facebook_icon.jpg" width="13" height="13"  />
           
            <span class="label">Facebook</span>


            <a  href="https://www.facebook.com/ENVIOUSAMEX" target="_blank">Visitanos en Facebook</a>
        </p>

        <br />
      <%-- &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;Verifica mi reputacion en mercado libre:
        <br />
         <p style="padding-left:20px">
            <img src="Images/ml_logo.png" width="14" height="14"  />
           
            <span class="label">Mercado Libre</span>


            <a  href="http://perfil.mercadolibre.com.mx/DAVID.IET" target="_blank">www.mercadolibre.com/DAVID.IET</a>
        </p>
         <br />

    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<img src="Images/reputacion.jpg" width="640" height="368"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>

   

        <div class="fb-like-box" data-href="https://www.facebook.com/ENVIOUSAMEX" data-colorscheme="light" data-show-faces="true" data-header="true" data-stream="false" data-show-border="true"></div>



    </section>
    
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

        </div>

    </div>






</asp:Content>