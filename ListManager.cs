using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ExampleProject
{
    #region Interfaces

    // Interfaces

    public interface ISupportedFile<T> { }
    public interface IFilterProvider<T>
    {
        ICollection<T> FiltersApply();
        void FiltersClear();
    }
    public interface IUndo<T>
    {
        Task<bool> UndoPop(T record);
        void UndoPush(T record);
    }

    /// <summary>
    /// Provides interface for methods/props that are not linked to generic type
    /// </summary>
    public interface IListManagerSpecific
    {
        Task Clear();
        bool IsCurrentRecordSelected();

        BindingSource GetBindingSource();
    }

    /// <summary>
    /// Extends the 'specific' interface (ie without generic types) and adds more
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IListManager<T> : IListManagerSpecific, IUndo<T>, IFilterProvider<T>
    {
        BindingList<T> Records { get; set; }
        ISupportedFile<T> File { get; set; }
        void SelectRecord(T recordToSelect);
        T CurrentRecord { get; set; }
    }

    #endregion

    #region Classes
    // Classes

    /// <summary>
    ///  This is a base/default implementation of IListManager
    /// </summary>
    /// <typeparam name="TRec"></typeparam>
    public abstract partial class ListManagerObject<TRec> :
                                   IListManager<TRec>,
                                   IFilterProvider<TRec>
                       where TRec :
                                   IListEntry,
                                   ISelectable,
                                   new()
    {
        public abstract BindingList<TRec> Records { get; set; }
        public virtual ISupportedFile<TRec> File { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual TRec CurrentRecord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual async Task Clear()
        {
            Records?.Clear();
            await Task.CompletedTask;
        }

        public virtual ICollection<TRec> FiltersApply()
        {
            return Records?.Where(r => r.IsNamed("filterMe")).ToList();
        }

        public virtual void FiltersClear() { }

        public virtual BindingSource GetBindingSource() => new BindingSource(Records, null);

        public virtual bool IsCurrentRecordSelected() => true;

        public virtual void SelectRecord(TRec recordToSelect) => recordToSelect.ItemStatus = ExampleItemStatus.SELECTED;

        public virtual async Task<bool> UndoPop(TRec record)
        {
            await Task.CompletedTask;
            return true;
        }

        public virtual void UndoPush(TRec record) { }
    }





    /// <summary>
    /// Extends the base implementation; from memory this was required in order to be able to
    /// allow casting children as IListManager<IListEntry>
    /// </summary>
    /// <typeparam name="TRec"></typeparam>
    public abstract class ListManagerWFO<TRec> : ListManagerObject<TRec>, IListManager<IListEntry>
       where TRec :
                IListEntry,
                ISelectable,
                new()
    {

        BindingList<IListEntry> IListManager<IListEntry>.Records
        {
            get => Records as BindingList<IListEntry>;
            set => Records = value as BindingList<TRec>;
        }
        
        // was:
        // BindingList<IListEntry> IListManager<IListEntry>.Records
        // {
        //    get => Unsafe.As<BindingList<IListEntry>>(Records);
        //    set => Records = Unsafe.As<BindingList<TRec>>(value);
        // }

        ICollection<IListEntry> IFilterProvider<IListEntry>.FiltersApply()
            => (ICollection<IListEntry>)FiltersApply();

        //ICollection<IListEntry> IFilterProvider<IListEntry>.FiltersApply()
        //    => Unsafe.As<ICollection<IListEntry>>(FiltersApply());

        ISupportedFile<IListEntry> IListManager<IListEntry>.File { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IListEntry IListManager<IListEntry>.CurrentRecord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        void IFilterProvider<IListEntry>.FiltersClear() => FiltersClear();

        void IListManager<IListEntry>.SelectRecord(IListEntry recordToSelect)
            => SelectRecord((TRec)(recordToSelect));

        async Task<bool> IUndo<IListEntry>.UndoPop(IListEntry record) => await UndoPop((TRec)record);

        void IUndo<IListEntry>.UndoPush(IListEntry record) => UndoPush((TRec)record);
    }


    /// <summary>
    /// This is the final implementation of the list manager
    /// </summary>
    public class ExampleListManager :
         ListManagerWFO<ExampleListEntry>,
         //IListManager<IListEntry>,
         IUndo<ExampleListEntry>,
         IFilterProvider<ExampleListEntry>
    {
        override public BindingList<ExampleListEntry> Records { get; set; } = new BindingList<ExampleListEntry>()
            {
                new ExampleListEntry { ItemName = "Name1" },
                new ExampleListEntry { ItemName = "Name2" },
                new ExampleListEntry { ItemName = "Name3" },
            };
    }

    #endregion







}

