﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppBata_WS_Electronico.Bata_Util {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://bataperu.com.pe/", ConfigurationName="Bata_Util.Bata_ElectronicoSoap")]
    public interface Bata_ElectronicoSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el mensaje ws_descargar_eposRequest tiene encabezados.
        [System.ServiceModel.OperationContractAttribute(Action="http://bataperu.com.pe/ws_descargar_epos", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppBata_WS_Electronico.Bata_Util.ws_descargar_eposResponse ws_descargar_epos(AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://bataperu.com.pe/ws_descargar_epos", ReplyAction="*")]
        System.Threading.Tasks.Task<AppBata_WS_Electronico.Bata_Util.ws_descargar_eposResponse> ws_descargar_eposAsync(AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bataperu.com.pe/")]
    public partial class ValidateAcceso : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string usernameField;
        
        private string passwordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
                this.RaisePropertyChanged("Username");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("Password");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bataperu.com.pe/")]
    public partial class Ba_Files : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoField;
        
        private string descripcionField;
        
        private byte[] filesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
                this.RaisePropertyChanged("codigo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
                this.RaisePropertyChanged("descripcion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=2)]
        public byte[] files {
            get {
                return this.filesField;
            }
            set {
                this.filesField = value;
                this.RaisePropertyChanged("files");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ws_descargar_epos", WrapperNamespace="http://bataperu.com.pe/", IsWrapped=true)]
    public partial class ws_descargar_eposRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://bataperu.com.pe/")]
        public AppBata_WS_Electronico.Bata_Util.ValidateAcceso ValidateAcceso;
        
        public ws_descargar_eposRequest() {
        }
        
        public ws_descargar_eposRequest(AppBata_WS_Electronico.Bata_Util.ValidateAcceso ValidateAcceso) {
            this.ValidateAcceso = ValidateAcceso;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ws_descargar_eposResponse", WrapperNamespace="http://bataperu.com.pe/", IsWrapped=true)]
    public partial class ws_descargar_eposResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://bataperu.com.pe/", Order=0)]
        public AppBata_WS_Electronico.Bata_Util.Ba_Files ws_descargar_eposResult;
        
        public ws_descargar_eposResponse() {
        }
        
        public ws_descargar_eposResponse(AppBata_WS_Electronico.Bata_Util.Ba_Files ws_descargar_eposResult) {
            this.ws_descargar_eposResult = ws_descargar_eposResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Bata_ElectronicoSoapChannel : AppBata_WS_Electronico.Bata_Util.Bata_ElectronicoSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Bata_ElectronicoSoapClient : System.ServiceModel.ClientBase<AppBata_WS_Electronico.Bata_Util.Bata_ElectronicoSoap>, AppBata_WS_Electronico.Bata_Util.Bata_ElectronicoSoap {
        
        public Bata_ElectronicoSoapClient() {
        }
        
        public Bata_ElectronicoSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Bata_ElectronicoSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Bata_ElectronicoSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Bata_ElectronicoSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppBata_WS_Electronico.Bata_Util.ws_descargar_eposResponse AppBata_WS_Electronico.Bata_Util.Bata_ElectronicoSoap.ws_descargar_epos(AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest request) {
            return base.Channel.ws_descargar_epos(request);
        }
        
        public AppBata_WS_Electronico.Bata_Util.Ba_Files ws_descargar_epos(AppBata_WS_Electronico.Bata_Util.ValidateAcceso ValidateAcceso) {
            AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest inValue = new AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest();
            inValue.ValidateAcceso = ValidateAcceso;
            AppBata_WS_Electronico.Bata_Util.ws_descargar_eposResponse retVal = ((AppBata_WS_Electronico.Bata_Util.Bata_ElectronicoSoap)(this)).ws_descargar_epos(inValue);
            return retVal.ws_descargar_eposResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AppBata_WS_Electronico.Bata_Util.ws_descargar_eposResponse> AppBata_WS_Electronico.Bata_Util.Bata_ElectronicoSoap.ws_descargar_eposAsync(AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest request) {
            return base.Channel.ws_descargar_eposAsync(request);
        }
        
        public System.Threading.Tasks.Task<AppBata_WS_Electronico.Bata_Util.ws_descargar_eposResponse> ws_descargar_eposAsync(AppBata_WS_Electronico.Bata_Util.ValidateAcceso ValidateAcceso) {
            AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest inValue = new AppBata_WS_Electronico.Bata_Util.ws_descargar_eposRequest();
            inValue.ValidateAcceso = ValidateAcceso;
            return ((AppBata_WS_Electronico.Bata_Util.Bata_ElectronicoSoap)(this)).ws_descargar_eposAsync(inValue);
        }
    }
}
