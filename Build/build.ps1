Set-ItemProperty -Path HKLM:\Software\Microsoft\Fusion -Name ForceLog         -Value 1               -Type DWord
Set-ItemProperty -Path HKLM:\Software\Microsoft\Fusion -Name LogFailures      -Value 1               -Type DWord
Set-ItemProperty -Path HKLM:\Software\Microsoft\Fusion -Name LogResourceBinds -Value 1               -Type DWord
Set-ItemProperty -Path HKLM:\Software\Microsoft\Fusion -Name LogPath          -Value 'C:\FusionLog\' -Type String

[Reflection.Assembly]::LoadWithPartialName("System.Xml.Linq") | Out-Null

$files = Get-ChildItem -Path "C:\Program Files (x86)\Microsoft Visual Studio\2017\" -Filter vstest.console.exe.config -Recurse -ErrorAction SilentlyContinue -Force
foreach ($file in $files) {
    $vstestConfigXDocument = [System.Xml.Linq.XDocument]::Load($file.FullName)
    if(!$vstestConfigXDocument.Root.Element("startup"))
    {
        $startupXElement = [System.Xml.Linq.XElement]::Parse('<startup useLegacyV2RuntimeActivationPolicy="true"> 
                <supportedRuntime version="v4.0"/>
            </startup>')

        $file.FullName
        $configSectionsXElement = $vstestConfigXDocument.Root.Element("configSections")
       
        if($configSectionsXElement)
        {
            $configSectionsXElement.AddAfterSelf($startupXElement)
        }
        else
        {
            $vstestConfigXDocument.Root.Add($startupXElement)
        }

        $vstestConfigXDocument.Save($file.FullName)
    }
}