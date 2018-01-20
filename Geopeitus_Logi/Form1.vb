Public Class Main

    Private Property pageready As Boolean = False

    Public hanneseStat As String = "http://www.geopeitus.ee/?p=420&u=hpalang1&num=10000"
	Public kauriStat As String = "http://www.geopeitus.ee/index.php?p=420&u=kaurp&num=10000"
	Public geoMain As String = "http://www.geopeitus.ee"
    Public logBase As String = "http://www.geopeitus.ee/logs/new/c/"
    Private username As String = "kaurp"
    Private password As String = "cog6heat"

	Dim caches As New Dictionary(Of String, List(Of String))

	Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'WB_HanneseStat.Navigate(hanneseStat)
		'WaitForPageLoadStat()
		'logIn(WB_HanneseStat)
	End Sub
	Private Sub Btn_Fire_Click(sender As Object, e As EventArgs) Handles Btn_Fire.Click
		WB_MainAction.Navigate(geoMain)
		WaitForPageLoad()

		logIn()
		WaitForPageLoad()

		WB_MainAction.Navigate(hanneseStat)
		WaitForPageLoad()

		generateToLog()

		WB_MainAction.Navigate(kauriStat)
		WaitForPageLoad()

		cleanToLog()

		Dim pair As KeyValuePair(Of String, List(Of String))
		For Each pair In caches
			logEntry(pair.Value(0), pair.Key, pair.Value(3))
		Next

		LogBox.AppendText("-------------------------------------------------------------------------------------------------------------------" & Environment.NewLine)
		LogBox.AppendText("Fucking everything should be logged" & Environment.NewLine)
	End Sub

	Private Sub logIn()
		Dim allInputs As HtmlElementCollection = WB_MainAction.Document.GetElementsByTagName("input")

		For Each input As HtmlElement In allInputs
			Dim name As String = input.GetAttribute("name")

			If name = "LoginForm[username]" Then
				input.InnerText = username
			End If
			If name = "LoginForm[password]" Then
				input.InnerText = password
			End If
			If name = "submit" Then
				input.InvokeMember("click")
			End If
		Next
	End Sub
	Private Sub generateToLog()
		Dim tables As HtmlElementCollection = WB_MainAction.Document.GetElementsByTagName("table")

		Debug.WriteLine("Number of tables: " & tables.Count)
		Dim table As HtmlElement = tables.Item(0)
		Dim logs As HtmlElementCollection = table.GetElementsByTagName("tr")
		For x As Integer = 0 To logs.Count - 1
			Dim log As HtmlElementCollection = logs.Item(x).Children

			If log.Item(3).InnerText.ToLower.Contains("leidis") Then
				Dim group As String = log.Item(4).InnerText.ToLower
				If group.Contains("kaur") Or group.Contains("kogu jõuk") Then
					Dim foundDate As String = log.Item(1).InnerText
					Dim id As String = extractID(log.Item(2).Children.Item(0).GetAttribute("href"))
					Dim name As String = log.Item(2).InnerText
					addToLogList(correctDate(foundDate), id, name, group)
					LogBox.AppendText("Added: " & name & tab(1) & " ID: " & id & Environment.NewLine)
				End If
			End If
		Next
		LogBox.AppendText("-------------------------------------------------------------------------------------------------------------------" & Environment.NewLine)
	End Sub

	Private Sub cleanToLog()
		Dim tables As HtmlElementCollection = WB_MainAction.Document.GetElementsByTagName("table")

		Debug.WriteLine("Number of tables: " & tables.Count)
		Dim table As HtmlElement = tables.Item(0)
		Dim logs As HtmlElementCollection = table.GetElementsByTagName("tr")
		For x As Integer = 0 To logs.Count - 1
			Dim log As HtmlElementCollection = logs.Item(x).Children

			If log.Item(3).InnerText.ToLower.Contains("leidis") Then
				Dim id As String = extractID(log.Item(2).Children.Item(0).GetAttribute("href"))
				If (caches.Contains(extractID(id))) Then
					caches.Remove(extractID(id))
				End If
			End If
		Next
		LogBox.AppendText("-------------------------------------------------------------------------------------------------------------------" & Environment.NewLine)
	End Sub

	Private Sub logEntry(foundDate As String, id As String, group As String)
		Delay(3)
		WB_MainAction.Navigate(logBase & id)
		WaitForPageLoad()

		Dim found As HtmlElement = WB_MainAction.Document.GetElementsByTagName("option").Cast(Of HtmlElement).First(
			Function(el) el.GetAttribute("value") = "1")

		found.SetAttribute("selected", "true")

		WB_MainAction.Document.GetElementById("Event_date").InnerText = foundDate
		WB_MainAction.Document.GetElementById("Event_user_name").InnerText = group
		WB_MainAction.Document.GetElementById("wmd-input").InnerText = "Leitud"

		For Each input As HtmlElement In WB_MainAction.Document.GetElementsByTagName("input")
			Dim name As String = input.GetAttribute("name")

			If name = "yt0" Then
				input.InvokeMember("click")
				LogBox.AppendText("Logging cache with ID: " & id & Environment.NewLine)
				WaitForPageLoad()
			End If
		Next
	End Sub
#Region "Page Loading Functions"
	Private Sub WaitForPageLoad()
		AddHandler WB_MainAction.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
		While Not pageready
			Application.DoEvents()
		End While
		pageready = False
	End Sub
	Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
		If WB_MainAction.ReadyState = WebBrowserReadyState.Complete Then
			pageready = True
			RemoveHandler WB_MainAction.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
		End If
	End Sub
#End Region
	Private Sub wbMain_Navigating(ByVal sender As Object, ByVal e As WebBrowserNavigatingEventArgs) Handles WB_MainAction.Navigating
		Debug.WriteLine("Main navigating")
	End Sub
	Private Sub addToLogList(foundDate As String, id As String, name As String, group As String)
		caches.Add(id, New List(Of String)(New String() {correctDate(foundDate), name, group}))
		LogBox.AppendText("Cache: " & name & tab(2) & " Found: " & correctDate(foundDate) & tab(2) & " Group: " & group & Environment.NewLine)
	End Sub
	Private Function correctDate(foundDate As String)
		Dim returnDate() As String = foundDate.Split(".")
		Return returnDate(2) & "-" & returnDate(1) & "-" & returnDate(0)
	End Function
	Private Function extractID(id As String)
		Dim sDelimStart As String = "&c=" 'First delimiting word
		Dim sDelimEnd As String = "#" 'Second delimiting word
		Dim nIndexStart As Integer = id.IndexOf(sDelimStart) 'Find the first occurrence of f1
		Dim nIndexEnd As Integer = id.IndexOf(sDelimEnd) 'Find the first occurrence of f2

		If nIndexStart > -1 AndAlso nIndexEnd > -1 Then '-1 means the word was not found.
			Dim res As String = Strings.Mid(id, nIndexStart + sDelimStart.Length + 1, nIndexEnd - nIndexStart - sDelimStart.Length) 'Crop the text between
			Return res
		End If
		Return "empty"
	End Function
	Private Function tab(count As Integer)
        Dim tabs As String = String.Empty
        For x As Integer = 1 To count
            tabs = tabs & ControlChars.Tab
        Next
        Return tabs
    End Function
End Class