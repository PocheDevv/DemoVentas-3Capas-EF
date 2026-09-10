<%@ Page Title="" Language="C#" MasterPageFile="~/MasterModelo.master" AutoEventWireup="true" CodeBehind="RegistrarOrden.aspx.cs" Inherits="SitioVentasWEB_GUI.Transacciones.RegistrarOrden" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cabecera" runat="server">
    <style type="text/css">
        .auto-style4 {
            height: 18px;
        }
    .auto-style5 {
        height: 20px;
    }
    .auto-style6 {
        width: 700px;
        height: 20px;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Principal" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
                <h2 class="tituloForm">Registrar Orden de Compra </h2>
    <table>
                        
                        <tr>
                            <td class="labelContenido">Ingrese fecha OC:</td>
                            <td colspan="2" style="width: 100px; height: 21px">
                                <asp:TextBox ID="txtFecOco" runat="server" CssClass="TextBoxDerecha" Width="96px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFecOco_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFecOco" />
                            </td>
                        </tr>
                        <tr>
                            <td class="labelContenido">Seleccione Proveedor:</td>
                            <td class="auto-style15" colspan="2">
                                <asp:DropDownList ID="cboProveedor" runat="server" AutoPostBack="True" CssClass="DropDownList" OnSelectedIndexChanged="cboProveedor_SelectedIndexChanged" Width="300px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelContenido">Ingrese fecha esperada de atencion:</td>
                            <td colspan="2" style="width: 100px">
                                <asp:TextBox ID="txtFecAte" runat="server" CssClass="TextBoxDerecha" Width="96px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFecAte_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFecAte" />
                            </td>
                        </tr>
                        <tr>
                            <td class="labelContenido">&nbsp;</td>
                            <td colspan="2" style="width: 100px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style20">
                                <asp:Button ID="btnAgregar" runat="server" CssClass="boton2" onclick="btnAgregar_Click" Text="Agregar Detalle" Width="150px" />
                            </td>
                            <td class="auto-style21">
                                <asp:Button ID="btnVerGrabar" runat="server" CssClass="boton2" onclick="btnVerGrabar_Click" Text="Grabar" Width="150px" />
                            </td>
                            <td class="auto-style21">
                                <asp:Button ID="btnCancelar" runat="server" CssClass="boton-new" Text="Cancelar" Width="150px" OnClick="btnCancelar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20">
                                <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Red" NavigateUrl="~/Transacciones/Transacciones.aspx">Retornar Menu </asp:HyperLink>
                            </td>
                            <td class="auto-style21">&nbsp;</td>
                            <td class="auto-style21">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;<asp:GridView ID="grDetalles" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" onrowcommand="grDetalles_RowCommand" ShowHeaderWhenEmpty="True" Width="1076px" OnSelectedIndexChanged="grDetalles_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/Images/Borrar.png" Text="Eliminar" />
                                    <asp:BoundField DataField="cod_pro" HeaderText="Codigo Producto" />
                                    <asp:BoundField DataField="Des_pro" HeaderText="Descripción" />
                                    <asp:BoundField DataField="can_ped" HeaderText="Cantidad Pedida" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style5" colspan="3">&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Width="1084px">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Button" CommandName="Agregar" ImageUrl="~/Images/Cancelar.png" Text="Botón" />
                                        <asp:BoundField DataField="Cod_pro" HeaderText="Codigo producto" />
                                        <asp:BoundField DataField="Des_pro" HeaderText="Descripción producto" />
                                        <asp:BoundField DataField="Can_ped" HeaderText="Cantidad pedida" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                </asp:GridView>
                            </td>
                            <td class="auto-style5"></td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
        </table>
            <%-- Este el modalpopup de ingreso de  Detalles de OC --%>
            <%--El link button o cualquiero otro control para el manejo del modal (TargetControl)--%>
           <asp:LinkButton Text="" ID = "lnkDetalle" runat="server" />
            <%--El  panel con el contenido--%>
              <asp:Panel ID="PanelDetalle" runat="server" CssClass="CajaDialogo" align="center" Style="display:none" Width="600px">
          
            <table style="border: Solid 3px #D55500;"
                cellpadding="0" cellspacing="0" width="600px" >
                <tr style="background-color: darkred">
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                        align="center">
                        Registrar Detalle
                    </td>
                </tr>
                <tr>
                    <td align="right" class="auto-style5">
                        &nbsp;</td>
                      <td align="left" class="auto-style6">
                          &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="auto-style5"></td>
                    <td align="left" class="auto-style6"></td>
                </tr>
                <tr>
                    <td align="right" class="labelContenido">Seleccione Producto:</td>
                    <td align="left" class="auto-style18">
                        <asp:DropDownList ID="cboProducto" runat="server"  Width="192px" CssClass="DropDownList">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="labelContenido">&nbsp;</td>
                    <td align="left" class="auto-style18">&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="labelContenido">
                        Cantidad Pedida:
                    </td>
                    <td align="left" class="auto-style8">
                        <asp:TextBox ID="txtCanPed" runat="server" Width="70px" CssClass="TextBoxDerecha"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtCanPed_MaskedEditExtender" runat="server" BehaviorID="txtCanPed_MaskedEditExtender" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="999" MaskType="Number" TargetControlID="txtCanPed" />
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style9">
                        <asp:Label ID="lblMensajeDetalle" runat="server" CssClass="labelErrores" Width="583px"></asp:Label>
                    </td>
                </tr>
               
                     <tr>
                         <td class="auto-style4">
                             </td>
                         <td class="auto-style4">
                             </td>
                </tr>
              
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td class="auto-style26">&nbsp;</td>
                </tr>
              
                <tr>
                    <td class="center">
                        <asp:Button ID="btnGrabarDetalle" runat="server" OnClick="btnGrabarDetalle_Click" Text="Grabar" Width="100px" CssClass="boton2" />
                    </td>
                    <td class="center">
                        <asp:Button ID="btnCancelarDetalle" runat="server" Text="Cancelar" Width="100px" CssClass="boton-new" />
                    </td>
                </tr>
              
                <tr>
                    <td class="auto-style27"></td>
                    <td class="auto-style27"></td>
                </tr>
              
                <tr>
                    <td class="auto-style27">&nbsp;</td>
                    <td class="auto-style27">&nbsp;</td>
                </tr>
              
            </table>
                        
        </asp:Panel>
            <%--El modalpoput extender : vease el TargetControl que apunta al linkbutton y el popuconttol ID que apunhta al panel--%> 
      <ajaxToolkit:ModalPopupExtender ID="PopDetalle" runat="server" BackgroundCssClass="FondoAplicacion"  
        TargetControlID="lnkDetalle" PopupControlID="PanelDetalle"  >
    </ajaxToolkit:ModalPopupExtender>
  
    <%--Este es el ModalPopup para enviar los mensajes--%>
             <%--El link button o cualquiero otro control para el manejo del modal--%>
              <asp:LinkButton ID="lnkMensaje" runat="server" ></asp:LinkButton>
             <%--El  panel con el contenido del mensaje--%>
               <asp:Panel ID="pnlMensaje" runat="server" CssClass="CajaDialogo" Style="display: none;" Width="500"> 
                    <table border="0" width="500px" > 
                        <tr style="margin: 0px; padding: 0px; background-color:darkred ; 
                        color: #FFFFFF;"> 
                            <td align="center" class="auto-style7" > 
                                <asp:Label ID="lblTitulo" runat="server" Text="Mensaje" /> 
                            </td> 
                            <td width="12%" class="center"> 
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Cancelar.png" Style="vertical-align: top;" 
                                    ImageAlign="Right" /> 
                            </td> 
                        </tr> 
                         <tr>
                             <td class="center" colspan="2">
                                  &nbsp;</td>
                         </tr>
                         <tr>
                             <td class="center" colspan="2">
                                 <asp:Label ID="lblMensajePopup" runat="server" CssClass="labelTitulo" />
                             </td>
                        </tr>
                         <tr>
                             <td class="auto-style7">&nbsp;</td>
                        </tr>
                         <tr>
                             <td class="center" colspan="2">
                                  <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="boton-new" /> 
                             </td>
                         </tr>
                        <tr>
                            <td class="center" colspan="2">&nbsp;</td>
                        </tr>
                    </table> 
                 </asp:Panel> 
            <ajaxToolkit:ModalPopupExtender ID="PopMensaje" runat="server" TargetControlID="lnkMensaje" 
                    PopupControlID="pnlMensaje" BackgroundCssClass="FondoAplicacion" OkControlID="btnAceptar" 
                     />
      
     </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>


