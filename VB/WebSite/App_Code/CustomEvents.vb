Imports System
Imports System.Collections
Imports System.ComponentModel

#Region "#customobject"
<Serializable>
Public Class CustomEvent
'INSTANT VB NOTE: The field id was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private id_Conflict As Object
	Private start As DateTime
	Private [end] As DateTime
'INSTANT VB NOTE: The field subject was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private subject_Conflict As String
'INSTANT VB NOTE: The field status was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private status_Conflict As Integer
'INSTANT VB NOTE: The field description was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private description_Conflict As String
'INSTANT VB NOTE: The field label was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private label_Conflict As Long
'INSTANT VB NOTE: The field location was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private location_Conflict As String
'INSTANT VB NOTE: The field allday was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private allday_Conflict As Boolean
'INSTANT VB NOTE: The field eventType was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private eventType_Conflict As Integer
'INSTANT VB NOTE: The field recurrenceInfo was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private recurrenceInfo_Conflict As String
'INSTANT VB NOTE: The field reminderInfo was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private reminderInfo_Conflict As String
'INSTANT VB NOTE: The field ownerId was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private ownerId_Conflict As Object
'INSTANT VB NOTE: The field price was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private price_Conflict As Double
'INSTANT VB NOTE: The field contactInfo was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private contactInfo_Conflict As String

'INSTANT VB NOTE: The field customInfo was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private customInfo_Conflict As String

	Public Sub New()
	End Sub

	Public Property StartTime() As DateTime
		Get
			Return start
		End Get
		Set(ByVal value As DateTime)
			start = value
		End Set
	End Property
	Public Property EndTime() As DateTime
		Get
			Return [end]
		End Get
		Set(ByVal value As DateTime)
			[end] = value
		End Set
	End Property
	Public Property Subject() As String
		Get
			Return subject_Conflict
		End Get
		Set(ByVal value As String)
			subject_Conflict = value
		End Set
	End Property
	Public Property Status() As Integer
		Get
			Return status_Conflict
		End Get
		Set(ByVal value As Integer)
			status_Conflict = value
		End Set
	End Property
	Public Property Description() As String
		Get
			Return description_Conflict
		End Get
		Set(ByVal value As String)
			description_Conflict = value
		End Set
	End Property
	Public Property Label() As Long
		Get
			Return label_Conflict
		End Get
		Set(ByVal value As Long)
			label_Conflict = value
		End Set
	End Property
	Public Property Location() As String
		Get
			Return location_Conflict
		End Get
		Set(ByVal value As String)
			location_Conflict = value
		End Set
	End Property
	Public Property AllDay() As Boolean
		Get
			Return allday_Conflict
		End Get
		Set(ByVal value As Boolean)
			allday_Conflict = value
		End Set
	End Property
	Public Property EventType() As Integer
		Get
			Return eventType_Conflict
		End Get
		Set(ByVal value As Integer)
			eventType_Conflict = value
		End Set
	End Property
	Public Property RecurrenceInfo() As String
		Get
			Return recurrenceInfo_Conflict
		End Get
		Set(ByVal value As String)
			recurrenceInfo_Conflict = value
		End Set
	End Property
	Public Property ReminderInfo() As String
		Get
			Return reminderInfo_Conflict
		End Get
		Set(ByVal value As String)
			reminderInfo_Conflict = value
		End Set
	End Property
	Public Property OwnerId() As Object
		Get
			Return ownerId_Conflict
		End Get
		Set(ByVal value As Object)
			ownerId_Conflict = value
		End Set
	End Property
	Public Property Id() As Object
		Get
			Return id_Conflict
		End Get
		Set(ByVal value As Object)
			id_Conflict = value
		End Set
	End Property
	Public Property Price() As Double
		Get
			Return price_Conflict
		End Get
		Set(ByVal value As Double)
			price_Conflict = value
		End Set
	End Property
	Public Property ContactInfo() As String
		Get
			Return contactInfo_Conflict
		End Get
		Set(ByVal value As String)
			contactInfo_Conflict = value
		End Set
	End Property
	Public Property CustomInfo() As String
		Get
			Return customInfo_Conflict
		End Get
		Set(ByVal value As String)
			customInfo_Conflict = value
		End Set
	End Property
End Class

<Serializable>
Public Class CustomEventList
	Inherits BindingList(Of CustomEvent)

	Public Sub AddRange(ByVal events As CustomEventList)
		For Each customEvent As CustomEvent In events
			Me.Add(customEvent)
		Next customEvent
	End Sub
	Public Function GetEventIndex(ByVal eventId As Object) As Integer
		For i As Integer = 0 To Count - 1
			If Me(i).Id Is eventId Then
				Return i
			End If
		Next i
		Return -1
	End Function
End Class

Public Class CustomEventDataSource
'INSTANT VB NOTE: The field events was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private events_Conflict As CustomEventList
	Public Sub New(ByVal events As CustomEventList)
		If events Is Nothing Then
			DevExpress.XtraScheduler.Native.Exceptions.ThrowArgumentNullException("events")
		End If
		Me.events_Conflict = events
	End Sub
	Public Sub New()
		Me.New(New CustomEventList())
	End Sub
	Public Property Events() As CustomEventList
		Get
			Return events_Conflict
		End Get
		Set(ByVal value As CustomEventList)
			events_Conflict = value
		End Set
	End Property
	Public ReadOnly Property Count() As Integer
		Get
			Return Events.Count
		End Get
	End Property

	#Region "ObjectDataSource methods"
	Public Function InsertMethodHandler(ByVal customEvent As CustomEvent) As Object
		Dim id As Object = customEvent.GetHashCode()
		customEvent.Id = id
		Events.Add(customEvent)
		Return id
	End Function
	Public Sub DeleteMethodHandler(ByVal customEvent As CustomEvent)
		Dim eventIndex As Integer = Events.GetEventIndex(customEvent.Id)
		If eventIndex >= 0 Then
			Events.RemoveAt(eventIndex)
		End If
	End Sub
	Public Sub UpdateMethodHandler(ByVal customEvent As CustomEvent)
		Dim eventIndex As Integer = Events.GetEventIndex(customEvent.Id)
		If eventIndex >= 0 Then
			Events.RemoveAt(eventIndex)
			Events.Insert(eventIndex, customEvent)
		End If
	End Sub
	Public Function SelectMethodHandler() As IEnumerable
		Dim result As New CustomEventList()
		result.AddRange(Events)
		Return result
	End Function
	#End Region

	Public Function ObtainLastInsertedId() As Object
		If Count < 1 Then
			Return Nothing
		End If
		Return Events(Count - 1).Id
	End Function
End Class

#End Region ' #customobject
