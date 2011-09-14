﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OneTransitAPI.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="OneTransitAPI")]
	public partial class DatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTransitAgency(TransitAgency instance);
    partial void UpdateTransitAgency(TransitAgency instance);
    partial void DeleteTransitAgency(TransitAgency instance);
    partial void InsertInformation(Information instance);
    partial void UpdateInformation(Information instance);
    partial void DeleteInformation(Information instance);
    #endregion
		
		public DatabaseDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["OneTransitAPIConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TransitAgency> TransitAgencies
		{
			get
			{
				return this.GetTable<TransitAgency>();
			}
		}
		
		public System.Data.Linq.Table<Information> Informations
		{
			get
			{
				return this.GetTable<Information>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Agencies")]
	public partial class TransitAgency : INotifyPropertyChanging, INotifyPropertyChanged
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
		
		public TransitAgency()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AgencyID", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TimeZone", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GTFSUrl", DbType="VarChar(500)")]
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Information")]
	public partial class Information : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Message;
		
		private System.DateTime _CreatedDate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnMessageChanging(string value);
    partial void OnMessageChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    #endregion
		
		public Information()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this.OnMessageChanging(value);
					this.SendPropertyChanging();
					this._Message = value;
					this.SendPropertyChanged("Message");
					this.OnMessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
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
#pragma warning restore 1591
