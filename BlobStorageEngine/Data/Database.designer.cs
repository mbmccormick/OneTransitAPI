﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlobStorageEngine.Data
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
    partial void InsertGTFS_Agency(GTFS_Agency instance);
    partial void UpdateGTFS_Agency(GTFS_Agency instance);
    partial void DeleteGTFS_Agency(GTFS_Agency instance);
    partial void InsertGTFS_Trip(GTFS_Trip instance);
    partial void UpdateGTFS_Trip(GTFS_Trip instance);
    partial void DeleteGTFS_Trip(GTFS_Trip instance);
    partial void InsertGTFS_Calendar(GTFS_Calendar instance);
    partial void UpdateGTFS_Calendar(GTFS_Calendar instance);
    partial void DeleteGTFS_Calendar(GTFS_Calendar instance);
    partial void InsertGTFS_Route(GTFS_Route instance);
    partial void UpdateGTFS_Route(GTFS_Route instance);
    partial void DeleteGTFS_Route(GTFS_Route instance);
    partial void InsertGTFS_StopTime(GTFS_StopTime instance);
    partial void UpdateGTFS_StopTime(GTFS_StopTime instance);
    partial void DeleteGTFS_StopTime(GTFS_StopTime instance);
    partial void InsertGTFS_Stop(GTFS_Stop instance);
    partial void UpdateGTFS_Stop(GTFS_Stop instance);
    partial void DeleteGTFS_Stop(GTFS_Stop instance);
    #endregion
		
		public DatabaseDataContext() : 
				base(global::BlobStorageEngine.Properties.Settings.Default.OneTransitAPIConnectionString1, mappingSource)
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
		
		public System.Data.Linq.Table<GTFS_Agency> GTFS_Agencies
		{
			get
			{
				return this.GetTable<GTFS_Agency>();
			}
		}
		
		public System.Data.Linq.Table<GTFS_Trip> GTFS_Trips
		{
			get
			{
				return this.GetTable<GTFS_Trip>();
			}
		}
		
		public System.Data.Linq.Table<GTFS_Calendar> GTFS_Calendars
		{
			get
			{
				return this.GetTable<GTFS_Calendar>();
			}
		}
		
		public System.Data.Linq.Table<GTFS_Route> GTFS_Routes
		{
			get
			{
				return this.GetTable<GTFS_Route>();
			}
		}
		
		public System.Data.Linq.Table<GTFS_StopTime> GTFS_StopTimes
		{
			get
			{
				return this.GetTable<GTFS_StopTime>();
			}
		}
		
		public System.Data.Linq.Table<GTFS_Stop> GTFS_Stops
		{
			get
			{
				return this.GetTable<GTFS_Stop>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertCalendars")]
		public int InsertCalendars([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SerializedData", DbType="Xml")] System.Xml.Linq.XElement serializedData, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NewPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> newPartitionKey, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="OldPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> oldPartitionKey)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serializedData, newPartitionKey, oldPartitionKey);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertTrips")]
		public int InsertTrips([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SerializedData", DbType="Xml")] System.Xml.Linq.XElement serializedData, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NewPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> newPartitionKey, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="OldPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> oldPartitionKey)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serializedData, newPartitionKey, oldPartitionKey);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertRoutes")]
		public int InsertRoutes([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SerializedData", DbType="Xml")] System.Xml.Linq.XElement serializedData, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NewPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> newPartitionKey, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="OldPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> oldPartitionKey)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serializedData, newPartitionKey, oldPartitionKey);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertStops")]
		public int InsertStops([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SerializedData", DbType="Xml")] System.Xml.Linq.XElement serializedData, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NewPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> newPartitionKey, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="OldPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> oldPartitionKey)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serializedData, newPartitionKey, oldPartitionKey);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertStopTimes")]
		public int InsertStopTimes([global::System.Data.Linq.Mapping.ParameterAttribute(Name="SerializedData", DbType="Xml")] System.Xml.Linq.XElement serializedData, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="NewPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> newPartitionKey, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="OldPartitionKey", DbType="UniqueIdentifier")] System.Nullable<System.Guid> oldPartitionKey)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serializedData, newPartitionKey, oldPartitionKey);
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GTFS_Agency")]
	public partial class GTFS_Agency : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _RowKey;
		
		private System.Guid _PartitionKey;
		
		private string _Name;
		
		private string _URL;
		
		private string _TimeZone;
		
		private string _State;
		
		private string _ID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(System.Guid value);
    partial void OnRowKeyChanged();
    partial void OnPartitionKeyChanging(System.Guid value);
    partial void OnPartitionKeyChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnURLChanging(string value);
    partial void OnURLChanged();
    partial void OnTimeZoneChanging(string value);
    partial void OnTimeZoneChanged();
    partial void OnStateChanging(string value);
    partial void OnStateChanged();
    partial void OnIDChanging(string value);
    partial void OnIDChanged();
    #endregion
		
		public GTFS_Agency()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowKey", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid RowKey
		{
			get
			{
				return this._RowKey;
			}
			set
			{
				if ((this._RowKey != value))
				{
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._RowKey = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartitionKey", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid PartitionKey
		{
			get
			{
				return this._PartitionKey;
			}
			set
			{
				if ((this._PartitionKey != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._PartitionKey = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_URL", DbType="VarChar(500)")]
		public string URL
		{
			get
			{
				return this._URL;
			}
			set
			{
				if ((this._URL != value))
				{
					this.OnURLChanging(value);
					this.SendPropertyChanging();
					this._URL = value;
					this.SendPropertyChanged("URL");
					this.OnURLChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="VarChar(50)")]
		public string ID
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GTFS_Trips")]
	public partial class GTFS_Trip : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _RowKey;
		
		private System.Guid _PartitionKey;
		
		private string _ID;
		
		private string _RouteID;
		
		private string _ServiceID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(System.Guid value);
    partial void OnRowKeyChanged();
    partial void OnPartitionKeyChanging(System.Guid value);
    partial void OnPartitionKeyChanged();
    partial void OnIDChanging(string value);
    partial void OnIDChanged();
    partial void OnRouteIDChanging(string value);
    partial void OnRouteIDChanged();
    partial void OnServiceIDChanging(string value);
    partial void OnServiceIDChanged();
    #endregion
		
		public GTFS_Trip()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowKey", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid RowKey
		{
			get
			{
				return this._RowKey;
			}
			set
			{
				if ((this._RowKey != value))
				{
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._RowKey = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartitionKey", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid PartitionKey
		{
			get
			{
				return this._PartitionKey;
			}
			set
			{
				if ((this._PartitionKey != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._PartitionKey = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ID
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RouteID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string RouteID
		{
			get
			{
				return this._RouteID;
			}
			set
			{
				if ((this._RouteID != value))
				{
					this.OnRouteIDChanging(value);
					this.SendPropertyChanging();
					this._RouteID = value;
					this.SendPropertyChanged("RouteID");
					this.OnRouteIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ServiceID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ServiceID
		{
			get
			{
				return this._ServiceID;
			}
			set
			{
				if ((this._ServiceID != value))
				{
					this.OnServiceIDChanging(value);
					this.SendPropertyChanging();
					this._ServiceID = value;
					this.SendPropertyChanged("ServiceID");
					this.OnServiceIDChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GTFS_Calendar")]
	public partial class GTFS_Calendar : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _RowKey;
		
		private System.Guid _PartitionKey;
		
		private string _ServiceID;
		
		private bool _Monday;
		
		private bool _Tuesday;
		
		private bool _Wednesday;
		
		private bool _Thursday;
		
		private bool _Friday;
		
		private bool _Saturday;
		
		private bool _Sunday;
		
		private System.DateTime _StartDate;
		
		private System.DateTime _EndDate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(System.Guid value);
    partial void OnRowKeyChanged();
    partial void OnPartitionKeyChanging(System.Guid value);
    partial void OnPartitionKeyChanged();
    partial void OnServiceIDChanging(string value);
    partial void OnServiceIDChanged();
    partial void OnMondayChanging(bool value);
    partial void OnMondayChanged();
    partial void OnTuesdayChanging(bool value);
    partial void OnTuesdayChanged();
    partial void OnWednesdayChanging(bool value);
    partial void OnWednesdayChanged();
    partial void OnThursdayChanging(bool value);
    partial void OnThursdayChanged();
    partial void OnFridayChanging(bool value);
    partial void OnFridayChanged();
    partial void OnSaturdayChanging(bool value);
    partial void OnSaturdayChanged();
    partial void OnSundayChanging(bool value);
    partial void OnSundayChanged();
    partial void OnStartDateChanging(System.DateTime value);
    partial void OnStartDateChanged();
    partial void OnEndDateChanging(System.DateTime value);
    partial void OnEndDateChanged();
    #endregion
		
		public GTFS_Calendar()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowKey", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid RowKey
		{
			get
			{
				return this._RowKey;
			}
			set
			{
				if ((this._RowKey != value))
				{
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._RowKey = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartitionKey", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid PartitionKey
		{
			get
			{
				return this._PartitionKey;
			}
			set
			{
				if ((this._PartitionKey != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._PartitionKey = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ServiceID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ServiceID
		{
			get
			{
				return this._ServiceID;
			}
			set
			{
				if ((this._ServiceID != value))
				{
					this.OnServiceIDChanging(value);
					this.SendPropertyChanging();
					this._ServiceID = value;
					this.SendPropertyChanged("ServiceID");
					this.OnServiceIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Monday", DbType="Bit NOT NULL")]
		public bool Monday
		{
			get
			{
				return this._Monday;
			}
			set
			{
				if ((this._Monday != value))
				{
					this.OnMondayChanging(value);
					this.SendPropertyChanging();
					this._Monday = value;
					this.SendPropertyChanged("Monday");
					this.OnMondayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tuesday", DbType="Bit NOT NULL")]
		public bool Tuesday
		{
			get
			{
				return this._Tuesday;
			}
			set
			{
				if ((this._Tuesday != value))
				{
					this.OnTuesdayChanging(value);
					this.SendPropertyChanging();
					this._Tuesday = value;
					this.SendPropertyChanged("Tuesday");
					this.OnTuesdayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Wednesday", DbType="Bit NOT NULL")]
		public bool Wednesday
		{
			get
			{
				return this._Wednesday;
			}
			set
			{
				if ((this._Wednesday != value))
				{
					this.OnWednesdayChanging(value);
					this.SendPropertyChanging();
					this._Wednesday = value;
					this.SendPropertyChanged("Wednesday");
					this.OnWednesdayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Thursday", DbType="Bit NOT NULL")]
		public bool Thursday
		{
			get
			{
				return this._Thursday;
			}
			set
			{
				if ((this._Thursday != value))
				{
					this.OnThursdayChanging(value);
					this.SendPropertyChanging();
					this._Thursday = value;
					this.SendPropertyChanged("Thursday");
					this.OnThursdayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Friday", DbType="Bit NOT NULL")]
		public bool Friday
		{
			get
			{
				return this._Friday;
			}
			set
			{
				if ((this._Friday != value))
				{
					this.OnFridayChanging(value);
					this.SendPropertyChanging();
					this._Friday = value;
					this.SendPropertyChanged("Friday");
					this.OnFridayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Saturday", DbType="Bit NOT NULL")]
		public bool Saturday
		{
			get
			{
				return this._Saturday;
			}
			set
			{
				if ((this._Saturday != value))
				{
					this.OnSaturdayChanging(value);
					this.SendPropertyChanging();
					this._Saturday = value;
					this.SendPropertyChanged("Saturday");
					this.OnSaturdayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sunday", DbType="Bit NOT NULL")]
		public bool Sunday
		{
			get
			{
				return this._Sunday;
			}
			set
			{
				if ((this._Sunday != value))
				{
					this.OnSundayChanging(value);
					this.SendPropertyChanging();
					this._Sunday = value;
					this.SendPropertyChanged("Sunday");
					this.OnSundayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartDate", DbType="Date NOT NULL")]
		public System.DateTime StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				if ((this._StartDate != value))
				{
					this.OnStartDateChanging(value);
					this.SendPropertyChanging();
					this._StartDate = value;
					this.SendPropertyChanged("StartDate");
					this.OnStartDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndDate", DbType="Date NOT NULL")]
		public System.DateTime EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				if ((this._EndDate != value))
				{
					this.OnEndDateChanging(value);
					this.SendPropertyChanging();
					this._EndDate = value;
					this.SendPropertyChanged("EndDate");
					this.OnEndDateChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GTFS_Routes")]
	public partial class GTFS_Route : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _RowKey;
		
		private System.Guid _PartitionKey;
		
		private string _ID;
		
		private string _LongName;
		
		private string _ShortName;
		
		private int _Type;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(System.Guid value);
    partial void OnRowKeyChanged();
    partial void OnPartitionKeyChanging(System.Guid value);
    partial void OnPartitionKeyChanged();
    partial void OnIDChanging(string value);
    partial void OnIDChanged();
    partial void OnLongNameChanging(string value);
    partial void OnLongNameChanged();
    partial void OnShortNameChanging(string value);
    partial void OnShortNameChanged();
    partial void OnTypeChanging(int value);
    partial void OnTypeChanged();
    #endregion
		
		public GTFS_Route()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowKey", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid RowKey
		{
			get
			{
				return this._RowKey;
			}
			set
			{
				if ((this._RowKey != value))
				{
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._RowKey = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartitionKey", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid PartitionKey
		{
			get
			{
				return this._PartitionKey;
			}
			set
			{
				if ((this._PartitionKey != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._PartitionKey = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ID
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LongName", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string LongName
		{
			get
			{
				return this._LongName;
			}
			set
			{
				if ((this._LongName != value))
				{
					this.OnLongNameChanging(value);
					this.SendPropertyChanging();
					this._LongName = value;
					this.SendPropertyChanged("LongName");
					this.OnLongNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShortName", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string ShortName
		{
			get
			{
				return this._ShortName;
			}
			set
			{
				if ((this._ShortName != value))
				{
					this.OnShortNameChanging(value);
					this.SendPropertyChanging();
					this._ShortName = value;
					this.SendPropertyChanged("ShortName");
					this.OnShortNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Int NOT NULL")]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GTFS_StopTimes")]
	public partial class GTFS_StopTime : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _RowKey;
		
		private System.Guid _PartitionKey;
		
		private string _StopID;
		
		private string _TripID;
		
		private System.DateTime _ArrivalTime;
		
		private System.DateTime _DepartureTime;
		
		private int _StopSequence;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(System.Guid value);
    partial void OnRowKeyChanged();
    partial void OnPartitionKeyChanging(System.Guid value);
    partial void OnPartitionKeyChanged();
    partial void OnStopIDChanging(string value);
    partial void OnStopIDChanged();
    partial void OnTripIDChanging(string value);
    partial void OnTripIDChanged();
    partial void OnArrivalTimeChanging(System.DateTime value);
    partial void OnArrivalTimeChanged();
    partial void OnDepartureTimeChanging(System.DateTime value);
    partial void OnDepartureTimeChanged();
    partial void OnStopSequenceChanging(int value);
    partial void OnStopSequenceChanged();
    #endregion
		
		public GTFS_StopTime()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowKey", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid RowKey
		{
			get
			{
				return this._RowKey;
			}
			set
			{
				if ((this._RowKey != value))
				{
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._RowKey = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartitionKey", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid PartitionKey
		{
			get
			{
				return this._PartitionKey;
			}
			set
			{
				if ((this._PartitionKey != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._PartitionKey = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StopID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string StopID
		{
			get
			{
				return this._StopID;
			}
			set
			{
				if ((this._StopID != value))
				{
					this.OnStopIDChanging(value);
					this.SendPropertyChanging();
					this._StopID = value;
					this.SendPropertyChanged("StopID");
					this.OnStopIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TripID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string TripID
		{
			get
			{
				return this._TripID;
			}
			set
			{
				if ((this._TripID != value))
				{
					this.OnTripIDChanging(value);
					this.SendPropertyChanging();
					this._TripID = value;
					this.SendPropertyChanged("TripID");
					this.OnTripIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArrivalTime", DbType="DateTime NOT NULL")]
		public System.DateTime ArrivalTime
		{
			get
			{
				return this._ArrivalTime;
			}
			set
			{
				if ((this._ArrivalTime != value))
				{
					this.OnArrivalTimeChanging(value);
					this.SendPropertyChanging();
					this._ArrivalTime = value;
					this.SendPropertyChanged("ArrivalTime");
					this.OnArrivalTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartureTime", DbType="DateTime NOT NULL")]
		public System.DateTime DepartureTime
		{
			get
			{
				return this._DepartureTime;
			}
			set
			{
				if ((this._DepartureTime != value))
				{
					this.OnDepartureTimeChanging(value);
					this.SendPropertyChanging();
					this._DepartureTime = value;
					this.SendPropertyChanged("DepartureTime");
					this.OnDepartureTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StopSequence", DbType="Int NOT NULL")]
		public int StopSequence
		{
			get
			{
				return this._StopSequence;
			}
			set
			{
				if ((this._StopSequence != value))
				{
					this.OnStopSequenceChanging(value);
					this.SendPropertyChanging();
					this._StopSequence = value;
					this.SendPropertyChanged("StopSequence");
					this.OnStopSequenceChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GTFS_Stops")]
	public partial class GTFS_Stop : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _RowKey;
		
		private System.Guid _PartitionKey;
		
		private string _ID;
		
		private string _Name;
		
		private string _Code;
		
		private decimal _Latitude;
		
		private decimal _Longitude;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(System.Guid value);
    partial void OnRowKeyChanged();
    partial void OnPartitionKeyChanging(System.Guid value);
    partial void OnPartitionKeyChanged();
    partial void OnIDChanging(string value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCodeChanging(string value);
    partial void OnCodeChanged();
    partial void OnLatitudeChanging(decimal value);
    partial void OnLatitudeChanged();
    partial void OnLongitudeChanging(decimal value);
    partial void OnLongitudeChanged();
    #endregion
		
		public GTFS_Stop()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowKey", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid RowKey
		{
			get
			{
				return this._RowKey;
			}
			set
			{
				if ((this._RowKey != value))
				{
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._RowKey = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartitionKey", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid PartitionKey
		{
			get
			{
				return this._PartitionKey;
			}
			set
			{
				if ((this._PartitionKey != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._PartitionKey = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ID
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="VarChar(50)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this.OnCodeChanging(value);
					this.SendPropertyChanging();
					this._Code = value;
					this.SendPropertyChanged("Code");
					this.OnCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Latitude", DbType="Decimal(18,15) NOT NULL")]
		public decimal Latitude
		{
			get
			{
				return this._Latitude;
			}
			set
			{
				if ((this._Latitude != value))
				{
					this.OnLatitudeChanging(value);
					this.SendPropertyChanging();
					this._Latitude = value;
					this.SendPropertyChanged("Latitude");
					this.OnLatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Longitude", DbType="Decimal(18,15) NOT NULL")]
		public decimal Longitude
		{
			get
			{
				return this._Longitude;
			}
			set
			{
				if ((this._Longitude != value))
				{
					this.OnLongitudeChanging(value);
					this.SendPropertyChanging();
					this._Longitude = value;
					this.SendPropertyChanged("Longitude");
					this.OnLongitudeChanged();
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
