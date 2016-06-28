using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NormalizedSystems.Net.Definitions
{
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RuleElement))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConditionElement))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActionElement))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EventElement))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DataElement))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(FieldElement))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class Element : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nameField;

        private uint versionField;

        public Element()
        {
            this.versionField = ((uint)(1));
        }

        [Category("Element")]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        [Category("Element")]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(typeof(uint), "1")]
        public uint Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
                this.RaisePropertyChanged("Version");
            }
        }

        public string FullName { get { return this.Name + (this.Version > 1 ? "Version" + this.Version : ""); } }

        public string PreviousVersion { get { return (this.Version >= 2 ? this.Name : "") + (this.Version > 2 ? "Version" + (this.Version - 1) : ""); } }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class FieldElement : Element
    {

        private PrimitiveTypes typeField;

        [Category("Field Element")]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PrimitiveTypes Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("Type");
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class DataElement : Element
    {

        private DeepObservableCollection<Element> fieldsField;

        [Category("Data Element")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Field", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> Fields
        {
            get
            {
                return this.fieldsField;
            }
            set
            {
                this.fieldsField = value;
                this.fieldsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("Fields"); };
                this.RaisePropertyChanged("Fields");
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class EventElement : Element
    {

        private DeepObservableCollection<Element> contentDataField;

        [Category("Event Element")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Data", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> ContentData
        {
            get
            {
                return this.contentDataField;
            }
            set
            {
                this.contentDataField = value;
                this.contentDataField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("ContentData"); };
                this.RaisePropertyChanged("ContentData");
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class ActionElement : Element
    {

        private DeepObservableCollection<Element> inputDataField;

        private DeepObservableCollection<Element> outputEventsField;

        [Category("Action Element")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Data", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> InputData
        {
            get
            {
                return this.inputDataField;
            }
            set
            {
                this.inputDataField = value;
                this.inputDataField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("InputData"); };
                this.RaisePropertyChanged("InputData");
            }
        }

        [Category("Action Element")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Event", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> OutputEvents
        {
            get
            {
                return this.outputEventsField;
            }
            set
            {
                this.outputEventsField = value;
                this.outputEventsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("OutputEvents"); };
                this.RaisePropertyChanged("OutputEvents");
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class ConditionElement : Element
    {

        private DeepObservableCollection<Element> eventsField;

        [Category("Condition Element")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Event", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> Events
        {
            get
            {
                return this.eventsField;
            }
            set
            {
                this.eventsField = value;
                this.eventsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("Events"); };
                this.RaisePropertyChanged("Events");
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class RuleElement : Element
    {
        private DeepObservableCollection<Element> eventsField;

        private DeepObservableCollection<Element> conditionsField;

        private DeepObservableCollection<Element> actionsField;

        private string logicField = "AND";

        [Category("Rule Element")] 
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Event", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> Events
        {
            get
            {
                return this.eventsField;
            }
            set
            {
                this.eventsField = value;
                this.eventsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("Events"); };
                this.RaisePropertyChanged("Events");
            }
        }

        [Category("Rule Element")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Condition", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> Conditions
        {
            get
            {
                return this.conditionsField;
            }
            set
            {
                this.conditionsField = value;
                this.conditionsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("Conditions"); };
                this.RaisePropertyChanged("Conditions");
            }
        }

        [Category("Rule Element")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Action", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<Element> Actions
        {
            get
            {
                return this.actionsField;
            }
            set
            {
                this.actionsField = value;
                this.actionsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("Actions"); };
                this.RaisePropertyChanged("Actions");
            }
        }

        [Category("Rule Element")]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("AND")]
        public string Logic
        {
            get
            {
                return this.logicField;
            }
            set
            {
                this.logicField = value;
                this.RaisePropertyChanged("Logic");
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Application : object, System.ComponentModel.INotifyPropertyChanged
    {
        private DeepObservableCollection<FieldElement> fieldElementsField;
        private DeepObservableCollection<DataElement> dataElementsField;
        private DeepObservableCollection<EventElement> eventElementsField;
        private DeepObservableCollection<ActionElement> actionElementsField;
        private DeepObservableCollection<ConditionElement> conditionElementsField;
        private DeepObservableCollection<RuleElement> ruleElementsField;

        private string nameField;

        [Category("Application")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<FieldElement> FieldElements
        {
            get
            {
                return this.fieldElementsField;
            }
            set
            {
                this.fieldElementsField = value;
                this.fieldElementsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("FieldElements"); };
                this.RaisePropertyChanged("FieldElements");
            }
        }

        [Category("Application")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<DataElement> DataElements
        {
            get
            {
                return this.dataElementsField;
            }
            set
            {
                this.dataElementsField = value;
                this.dataElementsField.CollectionChanged +=
                    (sender, e) =>
                    {
                        this.RaisePropertyChanged("DataElements");
                    };
                this.RaisePropertyChanged("DataElements");
            }
        }

        [Category("Application")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<EventElement> EventElements
        {
            get
            {
                return this.eventElementsField;
            }
            set
            {
                this.eventElementsField = value;
                this.eventElementsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("EventElements"); };
                this.RaisePropertyChanged("EventElements");
            }
        }

        [Category("Application")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<ActionElement> ActionElements
        {
            get
            {
                return this.actionElementsField;
            }
            set
            {
                this.actionElementsField = value;
                this.actionElementsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("ActionElements"); };
                this.RaisePropertyChanged("ActionElements");
            }
        }

        [Category("Application")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<ConditionElement> ConditionElements
        {
            get
            {
                return this.conditionElementsField;
            }
            set
            {
                this.conditionElementsField = value;
                this.conditionElementsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("ConditionElements"); };
                this.RaisePropertyChanged("ConditionElements");
            }
        }

        [Category("Application")]
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeepObservableCollection<RuleElement> RuleElements
        {
            get
            {
                return this.ruleElementsField;
            }
            set
            {
                this.ruleElementsField = value;
                this.ruleElementsField.CollectionChanged += (sender, e) => { this.RaisePropertyChanged("RuleElements"); };
                this.RaisePropertyChanged("RuleElements");
            }
        }

        [Category("Application")]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}


