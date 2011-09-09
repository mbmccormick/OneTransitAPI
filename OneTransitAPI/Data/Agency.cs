using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.ComponentModel;
using OneTransitAPI.Common;

namespace OneTransitAPI.Data
{
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Agencies")]
    public partial class Agency : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private string _AgencyID;

        private string _Name;

        private string _State;

        private string _TimeZone;

        private string _GTFSUrl;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnAgencyIDChanging(string value);
        partial void OnAgencyIDChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnStateChanging(string value);
        partial void OnStateChanged();
        partial void OnTimeZoneChanging(string value);
        partial void OnTimeZoneChanged();
        partial void OnGTFSUrlChanging(string value);
        partial void OnGTFSUrlChanged();
        #endregion

        public Agency()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AgencyID", DbType = "VarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        public string AgencyID
        {
            get
            {
                return this._AgencyID;
            }
            set
            {
                if ((this._AgencyID != value))
                {
                    this.OnAgencyIDChanging(value);
                    this.SendPropertyChanging();
                    this._AgencyID = value;
                    this.SendPropertyChanged("AgencyID");
                    this.OnAgencyIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(100)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_State", DbType = "VarChar(50)")]
        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                if ((this._State != value))
                {
                    this.OnStateChanging(value);
                    this.SendPropertyChanging();
                    this._State = value;
                    this.SendPropertyChanged("State");
                    this.OnStateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeZone", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string TimeZone
        {
            get
            {
                return this._TimeZone;
            }
            set
            {
                if ((this._TimeZone != value))
                {
                    this.OnTimeZoneChanging(value);
                    this.SendPropertyChanging();
                    this._TimeZone = value;
                    this.SendPropertyChanged("TimeZone");
                    this.OnTimeZoneChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GTFSUrl", DbType = "VarChar(500)")]
        public string GTFSUrl
        {
            get
            {
                return this._GTFSUrl;
            }
            set
            {
                if ((this._GTFSUrl != value))
                {
                    this.OnGTFSUrlChanging(value);
                    this.SendPropertyChanging();
                    this._GTFSUrl = value;
                    this.SendPropertyChanged("GTFSUrl");
                    this.OnGTFSUrlChanged();
                }
            }
        }

        public TimeZoneInfo FriendlyTimeZone
        {
            get
            {
                return Utilities.OlsonTimeZoneToTimeZoneInfo(this.TimeZone);
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}