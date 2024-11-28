using System;
using System.Collections.Generic;

namespace class ExampleProject
{

    #region Interfaces
    // Enums
    public enum ExampleItemStatus { UNDEF, SELECTED }

    // Interfaces
    public interface IListEntry: IFilterable, ISelectable
    {
        string Id { get; set; }
        ExampleItemStatus ItemStatus { get; set; }
        string ItemName { get; set; }
        public Task<bool> IsEmpty();
        public Task<bool> IsMy();
        public Task<List> Create();
    }

    public interface IExampleUndo<T>
    {
        Task<bool> DoUndo(T record);
        void Add(T record);
    }

    public interface IFilterable { }
    public interface ISelectable { }

    public interface ISomethingElse { } 
    public interface ISomethingElse2 { 
        string SomethingElse { get; set; }
    }
    #endregion

    #region Classes
    // Classess

    public class BaseRecord
    {
        public BaseRecord() { Name = Name + "!"}
        public virtual string Id { get; set; } = "default Id";
        public virtual string Name { get; set; } = "default name";
        public virtual async Task<bool> IsEmpty() => true;
        public virtual async Task<bool> IsMy() => this.Name.startsWith("my");
        public virtual async Task<List> Create() => new List(); 
    }


    public class ExampleListEntry :
        BaseRecord,
        IExampleWorklistRecord,
        IFilterable, ISelectable, IListEntry,
        ISomethingElse, ISomethingElse2
    {
        public ExampleListEntry() : base() { }
        string _id = "";
        override public string Id { get => "my" + _id; set => _id = value; }
        public ExampleItemStatus ItemStatus { get; set; }
        public string ItemName { get; set; }

        public string SomethingElse { get; set; }
    }
    #endregion
}
