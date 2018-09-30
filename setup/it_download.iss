[Code]
(*
 Inno Tools Downloader DLL
 Copyright (C) Sherlock Software 2006
 Version 0.2.1 Alpha

 Contact:
  The author, Nicholas Sherlock, at nick@sherlocksoftware.org.
  Comments, questions and suggestions welcome.

 Website:
  http://www.sherlocksoftware.org
*)

const
  ITDERR_SUCCESS = 0;
  ITDERR_USERCANCEL = 1;
  ITDERR_ERROR = 3;

  ITD_UPDATE_MOD = 4;

  ITD_Second_Column = 140;
  ITD_Vert_Spacing = 18;
  ITD_Box_Width = 400;

type
  ITD_HINTERNET = longword;

  ITD_TFile = record
    url, filename: string;
    size: cardinal;
    done: boolean;
  end;

  ITD_TFileArray = array of ITD_TFile;
  ITD_TDateTime = double;
  ITD_Pointer = longword;
  ITD_TFileTime = record
    dwLowDateTime: dword;
    dwHighDateTime: dword;
  end;

  ITD_TMsg = record
    hwnd: longword;
    message: longword;
    wParam: longword;
    lParam: longword;
    time: longword;
    pt: TPoint;
  end;

  ITD_TURLScheme = (usInvalid, usHTTP, usFTP);
  ITD_TDelphiUrlComponents = record
    HostName, UrlPath, Username, Password: string;
    port: integer;
    scheme: ITD_TURLScheme;
  end;

  ITD_TUI = record
    lblFile, lblSpeed, lblStatus, lblElapsedTime, lblRemainingTime, lblCurrent, lblTotal,
      valFile, valSpeed, valStatus, valElapsedTime, valRemainingTime, valCurrent, valTotal: tlabel;
    barCurrent, barTotal: TNewProgressBar;
  end;

  ITD_TURLComponents = record
    dwStructSize: DWORD;
    lpszScheme: PCHAR;
    dwSchemeLength: DWORD;
    nScheme: integer;
    lpszHostName: PCHAR;
    dwHostNameLength: DWORD;
    nPort: WORD;
    reserved: WORD; //Padding to align on DWORD boundaries
    lpszUserName: PCHAR;
    dwUserNameLength: DWORD;
    lpszPassword: PCHAR;
    dwPasswordLength: DWORD;
    lpszUrlPath: PCHAR;
    dwUrlPathLength: DWORD;
    lpszExtraInfo: PCHAR;
    dwExtraInfoLength: DWORD;
  end;

function ITD_InternetReadFile(hFile: ITD_HINTERNET; const lpBuffer: string;
  dwNumberOfBytesToRead: DWORD; var lpdwNumberOfBytesRead: DWORD): BOOL;
  external 'InternetReadFile@wininet.dll stdcall';

function ITD_InternetConnect(hInet: ITD_HINTERNET; lpszServerName: PChar;
  nServerPort: word; lpszUsername: PChar; lpszPassword: PChar;
  dwService: DWORD; dwFlags: DWORD; dwContext: DWORD): ITD_HINTERNET;
  external 'InternetConnectA@wininet.dll stdcall';

function ITD_InternetCloseHandle(hInet: ITD_HINTERNET): BOOL;
  external 'InternetCloseHandle@wininet.dll stdcall';

function ITD_InternetOpen(lpszAgent: PChar; dwAccessType: DWORD;
  lpszProxy, lpszProxyBypass: PChar; dwFlags: DWORD): ITD_HINTERNET;
  external 'InternetOpenA@wininet.dll stdcall';

function ITD_InternetOpenUrl(hInet: ITD_HINTERNET; lpszUrl: PChar;
  lpszHeaders: PChar; dwHeadersLength: DWORD; dwFlags: DWORD;
  dwContext: DWORD): ITD_HINTERNET;
  external 'InternetOpenUrlA@wininet.dll stdcall';

function ITD_InternetCrackUrl(lpszUrl: PChar; dwUrlLength, dwFlags: DWORD;
  var lpUrlComponents: ITD_TURLComponents): BOOL;
  external 'InternetCrackUrlA@wininet.dll stdcall';

function ITD_FtpOpenFile(hConnect: ITD_HINTERNET; lpszFileName: PChar;
  dwAccess: DWORD; dwFlags: DWORD; dwContext: DWORD): ITD_HINTERNET;
  external 'FtpOpenFileA@wininet.dll stdcall';

function ITD_FtpGetFileSize(hFile: ITD_HINTERNET;
  var lpdwFileSizeHigh: DWORD): DWORD;
  external 'FtpGetFileSize@wininet.dll stdcall';

function ITD_HttpQueryInfo(hRequest: ITD_HINTERNET; dwInfoLevel: DWORD;
  var Buffer: longword; var lpdwBufferLength: DWORD;
  var lpdwReserved: DWORD): BOOL;
  external 'HttpQueryInfoA@wininet.dll stdcall';

procedure ITD_GetSystemTimeAsFileTime(var lpSystemTimeAsFileTime: ITD_TFileTime);
  external 'GetSystemTimeAsFileTime@kernel32.dll stdcall';

function ITD_PeekMessage(var lpMsg: ITD_TMsg; hWnd: longword;
  wMsgFilterMin, wMsgFilterMax, wRemoveMsg: longword): integer;
  external 'PeekMessageA@user32.dll stdcall';

function ITD_TranslateMessage(const lpMsg: ITD_TMsg): BOOL;
  external 'TranslateMessage@user32.dll stdcall';

function ITD_DispatchMessage(const lpMsg: ITD_TMsg): Longint;
  external 'DispatchMessageA@user32.dll stdcall';

procedure ITD_PostQuitMessage(nExitCode: Integer);
  external 'PostQuitMessage@user32.dll stdcall';

const
  ITD_INTERNET_OPEN_TYPE_PRECONFIG = 0;

  ITD_INTERNET_FLAG_RELOAD = $80000000;

  ITD_HTTP_QUERY_STATUS_CODE = 19;
  ITD_HTTP_QUERY_FLAG_NUMBER = $20000000;
  ITD_HTTP_QUERY_CONTENT_LENGTH = 5;

  ITD_HTTP_STATUS_FORBIDDEN = 403;
  ITD_HTTP_STATUS_NOT_FOUND = 404;
  ITD_HTTP_STATUS_BAD_METHOD = 405;
  ITD_HTTP_STATUS_SERVER_ERROR = 500;

  ITD_INTERNET_SCHEME_HTTP = 3;
  ITD_INTERNET_SCHEME_HTTPS = 4;
  ITD_INTERNET_SCHEME_FTP = 1;

  ITD_INTERNET_SERVICE_FTP = 1;

  ITD_GENERIC_READ = $80000000;

  ITD_FTP_TRANSFER_TYPE_BINARY = $00000002;

  ITD_INTERNET_FLAG_NO_UI = $00000200;
  ITD_INTERNET_FLAG_IGNORE_CERT_CN_INVALID = $00001000;
  ITD_INTERNET_FLAG_IGNORE_CERT_DATE_INVALID = $00002000;
  ITD_INTERNET_FLAG_IGNORE_REDIRECT_TO_HTTP = $00008000;
  ITD_INTERNET_FLAG_IGNORE_REDIRECT_TO_HTTPS = $00004000;
  ITD_INTERNET_FLAG_RESYNCHRONIZE = $00000800;

  ITD_PM_REMOVE = 1;

  ITD_WM_QUIT = $0012;

var
  ITD_Options: record //Public settings
    caption, description, failmessage, failretryorcontinue: string;
    allowcontinue: boolean;
  end;

  ITD_RetryOnBack: boolean;//Retry when back is clicked

  ITD_Files: ITD_TFileArray; //File list to be downloaded

  ITD_Data: record //Internal data
    Debugmessages, Cancel: boolean;
    StartTime: ITD_tdatetime;
    RequestFlags: LongWord;
    Agent: string;
    DownloadDelay: integer;
  end;

//Returns the number of days elapsed since some epoch. Not compatible with Delphi's Now() as the Epoch differs
function ITD_Now: ITD_TDateTime;
var filetime: ITD_TFileTime;
begin
  ITD_getsystemtimeasfiletime(filetime);
  result := (filetime.dwlowdatetime + filetime.dwhighdatetime * 4294967296) / 8.64E11;
  // 8.64E11 is number of 100-nanosecond intervals in a day
  // 4294967396 is 2^32
end;

procedure ITD_ProcessMessages;
var msg: ITD_TMsg;
  stop: boolean;
begin
  stop := false;
  while (not terminated) and (not stop) and (ITD_PeekMessage(msg, 0, 0, 0, ITD_PM_REMOVE) <> 0) do
    if Msg.Message = ITD_WM_QUIT then begin
      stop := True;
      itd_data.Cancel := true;
      {We intercepted this unfairly, bubble it back up to our host so it can quit for us}
      ITD_PostQuitMessage(0);
    end else begin
      ITD_TranslateMessage(Msg);
      ITD_DispatchMessage(Msg);
    end;
end;

function ITD_FilesizeToStr(size: longword): string;
begin
  if size < 1024 then
    result := format('%d bytes', [size]) else
    if size < 1024 * 1024 then
      result := format('%dKB', [size div 1024]) else
      if size < 1024 * 1024 * 1024 then begin
        result := format('%.2fMB', [double(size) / (1024 * 1024)]);
        //For some reason, the cast to double is needed, otherwise
        //ROPS will think that the result is an integer and throw an
        //exception. Weird.
      end else
        result := format('%.2fGB', [double(size) / (1024 * 1024 * 1024)]);
end;

function ITD_postfix(i: integer; const singular, plural: string): string;
begin
  result := inttostr(i);
  if i = 1 then
    result := result + ' ' + singular else
    result := result + ' ' + plural;
end;

function ITD_ShortTimeToStr(time: ITD_tdatetime): string;
var l: cardinal;
  Sekunden, Minuten, Stunden, days, weeks: integer;
begin
  try
    {Likely to overflow if we are calc'ing time remaining and speed~0.
     Exception handler catches this case for us}
    l := round(time * secsperday);

    Sekunden := l mod 60;
    l := l div 60;

    Minuten := l mod 60;
    l := l div 60;

    Stunden := l mod 24;
    l := l div 24;

    days := l mod 7;
    l := l div 7;

    weeks := l;

    if weeks > 0 then begin
      result := ITD_Postfix(weeks, 'Woche', 'Wochen');
      if days > 0 then result := result + ', ' + ITD_Postfix(days, 'Tag', 'Tage');
    end else
      if days > 0 then begin
        result := ITD_Postfix(days, 'Tag', 'Tage');
        if Stunden > 0 then result := result + ', ' + ITD_Postfix(Stunden, 'Stunde', 'Stunden');
      end else
        if Stunden > 0 then begin
          result := ITD_Postfix(Stunden, 'Stunde', 'Stunden');
          if Minuten > 0 then result := result + ', ' + ITD_Postfix(Minuten, 'Minute', 'Minuten');
        end else
          if Minuten > 0 then begin
            result := ITD_Postfix(Minuten, 'Minute', 'Minuten');
            if Sekunden > 0 then result := result + ', ' + ITD_Postfix(Sekunden, 'Sekunde', 'Sekunden');
          end else
            result := ITD_Postfix(Sekunden, 'Sekunde', 'Sekunden');
  except
    result := 'Unbekannt'; //due to overflow
  end;
end;

function ITD_ShouldCancel: Boolean;
begin
  result := terminated or itd_data.Cancel;
end;

procedure ITD_Cancel;
begin
  itd_data.Cancel := true;
end;

procedure ITD_AddFile(const url, filename: string);
var f: ITD_TFile;
begin
  f.url := url;
  f.filename := filename;
  f.size := 0;
  setarraylength(itd_files, getarraylength(itd_files) + 1);
  itd_files[getarraylength(itd_files) - 1] := f;
end;

procedure ITD_AddFileSize(const url, filename: string; size:integer);
var f: ITD_TFile;
begin
  f.url := url;
  f.filename := filename;
  f.size := size;
  setarraylength(itd_files, getarraylength(itd_files) + 1);
  itd_files[getarraylength(itd_files) - 1] := f;
end;

procedure ITD_ClearFiles;
begin
  setarraylength(itd_files, 0);
end;

procedure ITD_UpdateUI(itd_UI: ITD_TUI; f: ITD_TFile; sizeunknown: boolean; filetime, starttime: ITD_TDateTime; byteswritten, totalbyteswritten, total: cardinal);
begin
  if itd_now - filetime > 0 then
    itd_ui.valSpeed.caption := ITD_FilesizeToStr(round(byteswritten / ((itd_now - filetime) * secsperday))) + '/s';

  itd_ui.valElapsedTime.caption := itd_shorttimetostr(itd_now - starttime);

  if (totalbyteswritten > 0) and (not sizeunknown) then
    itd_ui.valRemainingTime.caption := itd_shorttimetostr((total / totalbyteswritten) * (itd_now - starttime) - (itd_now - starttime)) else
    itd_ui.valRemainingTime.caption := 'Unbekannt';

  if byteswritten > f.size then
    //essentially, we don't know the proper size of the file, so let's not let on to the user.. :)
    itd_ui.valcurrent.caption := ITD_FilesizeToStr(byteswritten) + ' of unknown' else begin
    itd_ui.valcurrent.caption := ITD_FilesizeToStr(byteswritten) + ' of ' + ITD_FilesizeToStr(f.size);
    itd_ui.barcurrent.position := byteswritten;
  end;
  itd_ui.valcurrent.left := ITD_Box_Width - itd_ui.valCurrent.width;

  if (totalbyteswritten > total) or sizeunknown then
    itd_ui.valtotal.caption := ITD_FilesizeToStr(totalbyteswritten) + ' von Unbekannt' else begin
    itd_ui.valtotal.caption := ITD_FilesizeToStr(totalbyteswritten) + ' von ' + ITD_FilesizeToStr(total);
    itd_ui.barTotal.position := totalbyteswritten;
  end;
  itd_ui.valTotal.left := ITD_Box_Width - itd_ui.valTotal.width;
end;

function ITD_CheckErrorCode(hRequest: ITD_HINTERNET; out code: cardinal): boolean;
var buffer, buflength, index: cardinal;
begin
  index := 0;
  buffer := 0;
  buflength := sizeof(buffer);
  //We want to return true on error
  result := false;
  if ITD_HttpQueryInfo(hRequest, ITD_HTTP_QUERY_STATUS_CODE or ITD_HTTP_QUERY_FLAG_NUMBER, buffer, buflength, index) then begin //haven't decided that it's an error yet
    code := buffer;
    case code of
      ITD_HTTP_STATUS_FORBIDDEN, ITD_HTTP_STATUS_NOT_FOUND,
        ITD_HTTP_STATUS_BAD_METHOD, ITD_HTTP_STATUS_SERVER_ERROR: result := True; //error!
    else result := false; //no error
    end;
  end else
    code := 0;
end;

function ITD_CrackUrl(const url: string): ITD_TDelphiURLComponents;
var apiurl: ITD_TURLComponents;
begin
  apiurl.dwStructSize := SizeOf(apiurl);
  apiurl.dwHostNameLength:=1;
  apiurl.dwUserNameLength:=1;
  apiurl.dwPasswordLength:=1;
  apiurl.dwUrlPathLength:=1;

  if ITD_InternetCrackUrl(pchar(url), length(url), 0, apiurl) then begin
    result.HostName := copy(apiurl.lpszHostName, 1, apiurl.dwHostNameLength);
    Result.UrlPath := copy(apiurl.lpszUrlPath, 1, apiurl.dwUrlPathLength);
    result.Username := copy(apiurl.lpszUserName, 1, apiurl.dwUserNameLength);
    result.Password := copy(apiurl.lpszPassword, 1, apiurl.dwPasswordLength);
    result.Port := apiurl.nPort;
    case apiurl.nScheme of
      ITD_INTERNET_SCHEME_HTTP, ITD_INTERNET_SCHEME_HTTPS: result.scheme := usHTTP;
      ITD_INTERNET_SCHEME_FTP: result.scheme := usFTP;
    else result.scheme := usInvalid;
    end;
  end;
end;

function ITD_GetFTPSize(const url: string; const agent: string): longword;
var hInet, hConnect, hFile: ITD_HINTERNET;
  reserved: cardinal;
  crackedurl: ITD_TDelphiUrlComponents;
  highsize: longword;
begin
  result := 0;

  try
    reserved := 0;

    crackedurl := ITD_crackurl(url);

    hInet := ITD_InternetOpen(pchar(agent), ITD_INTERNET_OPEN_TYPE_PRECONFIG, '', '', 0);
    if hinet = 0 then exit;
    try
      hConnect := ITD_InternetConnect(hInet, pchar(crackedurl.hostname), crackedurl.port, pchar(crackedurl.username), pchar(crackedurl.password), ITD_INTERNET_SERVICE_FTP, 0, reserved);
      if hconnect = 0 then
        exit;
      try
        hfile := ITD_FtpOpenFile(hConnect, pchar(crackedurl.UrlPath), ITD_generic_read, ITD_FTP_TRANSFER_TYPE_BINARY or ITD_INTERNET_FLAG_RESYNCHRONIZE, reserved);
        if hFile = 0 then
          exit;
        try
          result := ITD_ftpgetfilesize(hFile, highsize); //Throw away the high part. Not good if we want files >4gig
        finally
          ITD_InternetCloseHandle(hfile);
        end;
      finally
        ITD_InternetCloseHandle(hConnect);
      end;
    finally
      ITD_InternetCloseHandle(hInet);
    end;
  except
    result := 0;
  end;
end;

function ITD_GetHTTPSize(const url: string; const agent: string; requestflags: longword): longword;
var hInet, hUrl: ITD_HINTERNET;
  bufferlength, flagnum, bytesavail: cardinal;
begin
  result := 0;
  try
    hInet := ITD_InternetOpen(pchar(agent), ITD_INTERNET_OPEN_TYPE_PRECONFIG, '', '', 0);
    if hinet = 0 then exit;
    try
      hUrl := ITD_InternetOpenUrl(hInet, PChar(url), '', 0, requestflags, 0);
      if hUrl = 0 then
        exit;
      try
        bufferlength := sizeof(bytesavail);
        flagnum := 0;
        if ITD_HttpQueryInfo(hUrl, ITD_HTTP_QUERY_CONTENT_LENGTH or ITD_HTTP_QUERY_FLAG_NUMBER, bytesavail, bufferlength, flagnum) then
          result := bytesavail else
          exit;
      finally
        ITD_InternetCloseHandle(hUrl);
      end;
    finally
      ITD_InternetCloseHandle(hInet);
    end;
  except
    result := 0;
  end;
end;

procedure ITD_QuerySize(var file: ITD_TFile; const agent: string; requestflags: longword);
var crackedurl: ITD_TDelphiUrlComponents;
begin
  file.size := 0;

  crackedurl := itd_crackurl(file.url);

  case crackedurl.scheme of
    usHTTP: file.size := ITD_gethttpsize(file.url, agent, requestflags);
    usFTP: file.size := ITD_getftpsize(file.url, agent);
  end;
end;

function ITD_WrapInternetReadFile(hFile: ITD_HINTERNET; lpBuffer: pchar;
  dwNumberOfBytesToRead: DWORD; var lpdwNumberOfBytesRead: DWORD; var success: boolean): Boolean;
begin
  success := ITD_InternetReadFile(hfile, lpbuffer, dwNumberOfBytesToRead, lpdwNumberOfBytesRead);
  result := success;
end;

function ITD_Internal_DownloadList(itd_ui: ITD_TUI; var files: ITD_TFileArray): integer;
var shouldupdate, fileindex: integer;
  useUI:boolean;
  hInet, hFile: ITD_HINTERNET;
  buffer: string;
  total, bytesread, byteswritten, totalbyteswritten: cardinal;
  filestream: TFileStream;
  starttime, filetime, lastupdate: ITD_tdatetime;
  success, sizeunknown: boolean;
  errorcode: cardinal;
begin
  result := ITDERR_ERROR;
  itd_data.Cancel := false;
  setlength(buffer, 8 * 1024); //8 kilobyte buffer
  starttime := itd_now;
  useUI:=itd_ui.valStatus<>nil;

  itd_processmessages;

  if useUI then itd_ui.valstatus.caption := 'Suche nach Datei...';

  itd_processmessages;

  total := 0;
  sizeunknown := false;

  for fileindex := 0 to getarraylength(itd_files) - 1 do begin
	if itd_files[fileindex].size=0 then //Haven't found the size yet...
      itd_querysize(itd_files[fileindex], itd_data.Agent, itd_data.RequestFlags);
    total := total + itd_files[fileindex].size;
    if itd_files[fileindex].size = 0 then begin
      sizeunknown := true;
      if itd_data.DebugMessages then
        msgbox('Can''t get size for ' + itd_files[fileindex].url, mbinformation, mb_ok);
    end;

    itd_processmessages;

    if itd_shouldcancel then
      raiseexception(inttostr(ITDERR_USERCANCEL));
  end;

  if useUI then begin
   itd_ui.barTotal.Max := total;
   itd_ui.barTotal.position := 0;
  end;

  shouldupdate := 0;
  lastupdate := 0;
  totalbyteswritten := 0;

  hInet := ITD_InternetOpen(pchar(itd_data.Agent), ITD_INTERNET_OPEN_TYPE_PRECONFIG, '', '', 0);
  if hInet = 0 then
    raiseexception(inttostr(ITDERR_ERROR));
  try
    for fileindex := 0 to GetArrayLength(ITD_Files) - 1 do
     if not ITD_Files[fileindex].done then begin //Loop over all files to be downloaded

      if useUI then begin
       itd_ui.valstatus.caption := 'Starte Download...';
       itd_ui.valfile.caption := extractfilename(itd_files[fileindex].filename);
       itd_ui.barcurrent.position := 0;
       itd_ui.barcurrent.max := itd_files[fileindex].size;
      end;

      hFile := ITD_InternetOpenUrl(hInet, PChar(itd_files[fileindex].url), '', 0, itd_data.RequestFlags, 0);
      if hFile = 0 then
        raiseexception(inttostr(ITDERR_ERROR));
      try
        if itd_checkerrorcode(hFile, errorcode) then begin //there was an error
          if itd_data.DebugMessages then
            msgbox('HTTP error trying to download the file at (' + itd_files[fileindex].url + ') to (' + itd_files[fileindex].filename + '), code ' + inttostr(errorcode), MBError, MB_OK);
          raiseexception(inttostr(ITDERR_ERROR));
        end;

        if useUI then
         itd_ui.valstatus.caption := 'Download...';

        filetime := itd_now;

        try
          filestream := TFileStream.Create(itd_files[fileindex].filename, fmCreate or fmShareDenyWrite);
          try
            byteswritten := 0;
            while ITD_WrapInternetReadFile(hFile, pchar(buffer), Length(buffer), bytesread, success) and (bytesread > 0) do begin
              filestream.write(buffer, bytesread);
              byteswritten := byteswritten + bytesread;
              totalbyteswritten := totalbyteswritten + bytesread;
              shouldupdate := shouldupdate + 1;

              if ((shouldupdate >= itd_update_mod) or ((itd_now - lastupdate) > 2 / secsperday)) then begin
                lastupdate := itd_now; //Updated based on time passed
                shouldupdate := 0; //Note that this avoids potential problems if the clock changes
                if useUI then
                 ITD_updateUI(itd_ui, itd_files[fileindex], sizeunknown, filetime, starttime, byteswritten, totalbyteswritten, total);
                itd_processmessages;
                if itd_shouldcancel then
                  raiseexception(inttostr(ITDERR_USERCANCEL));
              end;
              if itd_data.DownloadDelay > 0 then sleep(itd_data.DownloadDelay);
            end;
            //update the UI one last time, just before we leave
            if useUI then
             ITD_updateUI(itd_ui, itd_files[fileindex], sizeunknown, filetime, starttime, byteswritten, totalbyteswritten, total);
            if not success then
              raiseexception(inttostr(ITDERR_ERROR));
          finally
            filestream.Free;
          end;
        except //There was a problem downloading the current file
          if fileexists(itd_files[fileindex].filename) then //our file is useless, now
            deletefile(pchar(itd_files[fileindex].filename));
          raiseexception(getexceptionmessage); //Keep going on our exception handler chain
        end;
      finally
        ITD_InternetCloseHandle(hFile);
      end;

      files[fileindex].done := true; //we have finished this file!
    end; //End for loop
    result := ITDERR_SUCCESS; //done without incident!

	if useUI then begin
     itd_ui.lblstatus.caption := 'Download vollständig!';
     itd_ui.valFile.caption := '';
     itd_ui.valCurrent.caption := '';
     itd_ui.valSpeed.caption := '';
     itd_ui.valRemainingTime.caption := '';
    end;

  finally
    ITD_InternetCloseHandle(hInet);
  end;
end;

function ITD_DownloadFiles: boolean;
var ui:ITD_TUI;
begin
  try
    result := ITD_Internal_DownloadList(ui, ITD_Files)=ITDERR_SUCCESS;
  except
    result := false; //Unknown error message
  end;
end;

procedure ITD_NowDoDownload(page: TWizardPage);
var err: integer;
  ui: ITD_TUI;
begin
  wizardform.backbutton.enabled := false;
  wizardform.nextbutton.enabled := false;

  {Find the UI components for the rest of the code to work with...}
  ui.lblFile := TLabel(page.findcomponent('lblFile'));
  ui.lblSpeed := TLabel(page.findcomponent('lblSpeed'));
  ui.lblStatus := TLabel(page.findcomponent('lblStatus'));
  ui.lblElapsedTime := TLabel(page.findcomponent('lblElapsedTime'));
  ui.lblRemainingTime := TLabel(page.findcomponent('lblRemainingTime'));
  ui.lblCurrent := TLabel(page.findcomponent('lblCurrent'));
  ui.lblTotal := TLabel(page.findcomponent('lblTotal'));

  ui.valFile := TLabel(page.findcomponent('valFile'));
  ui.valSpeed := TLabel(page.findcomponent('valSpeed'));
  ui.valStatus := TLabel(page.findcomponent('valStatus'));
  ui.valElapsedTime := TLabel(page.findcomponent('valElapsedTime'));
  ui.valRemainingTime := TLabel(page.findcomponent('valRemainingTime'));
  ui.valCurrent := TLabel(page.findcomponent('valCurrent'));
  ui.valTotal := TLabel(page.findcomponent('valTotal'));

  ui.barCurrent := TNewProgressBar(page.findcomponent('barCurrent'));
  ui.barTotal := TNewProgressBar(page.findcomponent('barTotal'));

  try
   try
    err := itd_internal_downloadlist(ui,ITD_Files);
   except
    err := strtoint(getexceptionmessage);
   end;
  except
    err := ITDERR_ERROR; //Unknown error.
  end;

  case err of
    ITDERR_SUCCESS: begin
        wizardform.nextbutton.enabled := true;
        wizardform.nextbutton.onclick(nil);
      end;
    ITDERR_USERCANCEL: ; //Don't show a message, this happens on setup close and cancel click
  else begin
    //Some unexpected error
      wizardform.backbutton.caption := 'Wiederholen';
      wizardform.backbutton.enabled := true;
      wizardform.backbutton.show();
      itd_retryonback := true;

      if itd_options.allowcontinue then begin //Download failed, we can retry, continue, or exit
        wizardform.nextbutton.enabled := true;
        if length(itd_options.failretryorcontinue) = 0 then
          MsgBox('Die Datei konnte nicht heruntergeladen werden. Klicken Sie auf Wiederholen um den Download erneut zu starten, oder klicken Sie auf weiter um das Setup fortzusetzen.', mbError, MB_OK)
        else
          MsgBox(itd_options.failretryorcontinue, mbError, MB_OK);
      end else begin //Download failed, we must retry or exit setup
        if length(itd_options.failmessage) = 0 then
          MsgBox('Die Datei konnte nicht heruntergeladen werden. Klicken Sie auf Wiederholen um den Download erneut zu starten, oder klicken Sie auf Abbrechen um das Setup zu beenden.', mbError, MB_OK)
        else
          MsgBox(itd_options.failmessage, mbError, MB_OK);
      end;
    end;
  end;
end;

procedure ITD_HandleShowPage(sender: TWizardPage);
begin
  wizardform.nextbutton.enabled := false;
  wizardform.backbutton.hide();
  ITD_NowDoDownload(sender);
end;

function ITD_HandleBackClick(sender: TWizardpage): boolean;
begin
  result := false;
  if ITD_RetryOnBack then begin
    ITD_RetryOnBack := false;
    ITD_NowDoDownload(sender);
  end;
end;

function ITD_DownloadFile(const url, filename: string): boolean;
var f: ITD_TFile;
  list: ITD_TFileArray;
  ui: ITD_TUI;
begin
  f.url := url;
  f.filename := filename;
  f.size := 0;
  setarraylength(list, 1);
  list[0] := f;

  try
    result := (itd_internal_downloadlist(ui, list)=ITDERR_Success);
  except
    result := false;
  end;
end;

function ITD_HandleSkipPage(Sender: TWizardPage): boolean;
begin
  result := GetArrayLength(itd_files) = 0;
end;

procedure ITD_Init;
begin
  itd_data.RequestFlags := ITD_INTERNET_FLAG_NO_UI or ITD_INTERNET_FLAG_IGNORE_CERT_CN_INVALID or
    ITD_INTERNET_FLAG_IGNORE_CERT_DATE_INVALID or ITD_INTERNET_FLAG_IGNORE_REDIRECT_TO_HTTP or
    ITD_INTERNET_FLAG_IGNORE_REDIRECT_TO_HTTPS or ITD_INTERNET_FLAG_RESYNCHRONIZE;

  itd_data.Agent := 'InnoTools_Downloader';

  itd_data.DownloadDelay := 0;

  itd_options.caption := 'Es werden erforderliche Dateien vom Internet heruntergeladen';
  itd_options.description := 'Bitte warten Sie, während das Setup die Dateien herunterlädt...';
end;

procedure ITD_DownloadAfter(afterID: integer);
var itd_downloadpage: TWizardPage;
  ui: ITD_TUI;
begin
  itd_downloadpage := CreateCustomPage(afterID, itd_options.caption, itd_options.description);

  itd_downloadpage.onactivate := @itd_handleshowpage;
  itd_downloadpage.onshouldskippage := @itd_handleskippage;
  itd_downloadpage.onbackbuttonclick := @itd_handlebackclick;

  ui.lblFile := TLabel.create(itd_downloadpage);
  ui.lblFile.name := 'lblFile';
  ui.lblFile.caption := 'Datei:';
  ui.lblFile.parent := itd_downloadpage.surface;

  ui.valfile := TLabel.create(itd_downloadpage);
  ui.valfile.name := 'valFile';
  ui.valfile.caption := '';
  ui.valfile.left := ITD_Second_Column;
  ui.valFile.parent := itd_downloadpage.surface;

  ui.lblspeed := TLabel.create(itd_downloadpage);
  ui.lblSpeed.name := 'lblSpeed';
  ui.lblSpeed.caption := 'Geschwindigkeit:';
  ui.lblSpeed.top := ITD_Vert_Spacing * 1;
  ui.lblSpeed.parent := itd_downloadpage.surface;

  ui.valSpeed := TLabel.create(itd_downloadpage);
  ui.valSpeed.name := 'valSpeed';
  ui.valspeed.caption := '';
  ui.valspeed.left := ITD_Second_Column;
  ui.valSpeed.top := ITD_Vert_Spacing * 1;
  ui.valSpeed.parent := itd_downloadpage.surface;

  ui.lblstatus := TLabel.create(itd_downloadpage);
  ui.lblStatus.name := 'lblStatus';
  ui.lblstatus.caption := 'Status:';
  ui.lblstatus.top := ITD_Vert_Spacing * 2;
  ui.lblStatus.parent := itd_downloadpage.surface;

  ui.valstatus := TLabel.create(itd_downloadpage);
  ui.valstatus.name := 'valStatus';
  ui.valStatus.caption := '';
  ui.valstatus.left := ITD_Second_Column;
  ui.valStatus.top := ITD_Vert_Spacing * 2;
  ui.valStatus.parent := itd_downloadpage.surface;

  ui.lblelapsedtime := TLabel.create(itd_downloadpage);
  ui.lblelapsedtime.name := 'lblElapsedTime';
  ui.lblelapsedtime.caption := 'Verstrichene Zeit:';
  ui.lblelapsedtime.top := ITD_Vert_Spacing * 3;
  ui.lblElapsedTime.parent := itd_downloadpage.surface;

  ui.valElapsedTime := TLabel.create(itd_downloadpage);
  ui.valElapsedTime.name := 'valElapsedTime';
  ui.valElapsedTime.caption := '';
  ui.valElapsedTime.left := ITD_Second_Column;
  ui.valelapsedTime.top := ITD_Vert_Spacing * 3;
  ui.valElapsedTime.parent := itd_downloadpage.surface;

  ui.lblRemainingTime := TLabel.create(itd_downloadpage);
  ui.lblRemainingTime.name := 'lblRemainingTime';
  ui.lblRemainingtime.caption := 'Verbleibende Zeit:';
  ui.lblremainingtime.top := ITD_Vert_Spacing * 4;
  ui.lblRemainingTime.parent := itd_downloadpage.surface;

  ui.valRemainingtime := TLabel.create(itd_downloadpage);
  ui.valRemainingTime.name := 'valRemainingTime';
  ui.valRemainingtime.caption := '';
  ui.valremainingtime.left := ITD_Second_Column;
  ui.valRemainingTime.Top := ITD_Vert_Spacing * 4;
  ui.valRemainingTime.parent := itd_downloadpage.surface;

  {progess bars etc:}

  ui.lblCurrent := TLabel.create(itd_downloadpage);
  ui.lblCurrent.name := 'lblCurrent';
  ui.lblCurrent.caption := 'Aktuelle Datei:';
  ui.lblCurrent.top := ITD_Vert_Spacing * 6;
  ui.lblCurrent.parent := itd_downloadpage.surface;

  ui.valCurrent := TLabel.create(itd_downloadpage);
  ui.valcurrent.name := 'valCurrent';
  ui.valCurrent.caption := '';
  ui.valCurrent.Top := ITD_Vert_Spacing * 6;
  ui.valCurrent.left := ITD_Box_Width - ui.valcurrent.width;
  //ui.valCurrent.align := taRightJustify;
  ui.valCurrent.parent := itd_downloadpage.surface;

  ui.barCurrent := TNewProgressBar.create(itd_downloadpage);
  ui.barCurrent.name := 'barCurrent';
  ui.barcurrent.width := ITD_Box_Width;
  ui.barCurrent.Top := ITD_Vert_Spacing * 7;
  ui.barCurrent.parent := itd_downloadpage.surface;

  ui.lblTotal := TLabel.create(itd_downloadpage);
  ui.lblTotal.name := 'lblTotal';
  ui.lblTotal.caption := 'Gesamthaft:';
  ui.lblTotal.top := ITD_Vert_Spacing * 9;
  ui.lblTotal.parent := itd_downloadpage.surface;

  ui.valTotal := TLabel.create(itd_downloadpage);
  ui.valTotal.name := 'valTotal';
  ui.valTotal.caption := '';
  ui.valTotal.top := ITD_Vert_Spacing * 9;
  ui.valTotal.left := ITD_Box_Width - ui.valtotal.width;
  ui.valTotal.parent := itd_downloadpage.surface;

  ui.barTotal := TNewProgressBar.create(itd_downloadpage);
  ui.barTotal.name := 'barTotal';
  ui.barTotal.width := ITD_Box_Width;
  ui.barTotal.Top := ITD_Vert_Spacing * 10;
  ui.barTotal.parent := itd_downloadpage.surface;
end;


procedure ITD_SetOption(const option, value: string);
begin
  if comparetext(option, 'UI_Caption') = 0 then begin
    itd_options.caption := value;
  end else
    if comparetext(option, 'UI_Description') = 0 then begin
      itd_options.description := value;
    end else
      if comparetext(option, 'UI_FailMessage') = 0 then
        itd_options.failmessage := value else
        if comparetext(option, 'UI_FailOrContinueMessage') = 0 then
          itd_options.failretryorcontinue := value else
          if comparetext(option, 'UI_AllowContinue') = 0 then
            itd_options.allowcontinue := (value = '1') else

            if CompareText(option, 'Debug_DownloadDelay') = 0 then begin
              itd_data.DownloadDelay := strtoint(value);
            end else
              if CompareText(option, 'ITD_NoCache') = 0 then begin
                if value = '1' then
                  itd_data.RequestFlags := itd_data.RequestFlags or ITD_INTERNET_FLAG_RELOAD else
                  itd_data.RequestFlags := itd_data.RequestFlags and not ITD_INTERNET_FLAG_RELOAD;
              end else
                if CompareText(option, 'Debug_Messages') = 0 then
                  itd_data.DebugMessages := (value = '1');

end;

function ITD_GetOption(const option: string): string;
begin
  if CompareText(option, 'ITD_Version') = 0 then
    result := '0.2 Alpha' else
    if CompareText(option, 'Debug_DownloadDelay') = 0 then
      result := inttostr(itd_data.DownloadDelay) else
      if CompareText(option, 'ITD_NoCache') = 0 then begin
        if itd_data.RequestFlags and ITD_INTERNET_FLAG_RELOAD <> 0 then
          result := '1' else result := '0';
      end else
        if CompareText(option, 'Debug_Messages') = 0 then begin
          if itd_data.DebugMessages then result := '1' else result := '0'
        end else
          result := '';
end;
