# RSACrackstation

<img alt="Lines of code" src="https://img.shields.io/tokei/lines/github/Zymat-dk/RSACrackstation?color=FF8A00">

The source code behind the service running at [RSACrackstation](https://rsacrackstation.com). Feel free to poke around and look at the code.


Control all at once 
```bash
updateserver
```

To control the main service
```bash
sudo systemctl stop rsacrackstation.service
git pull
dotnet publish --configuration Release
sudo systemctl start rsacrackstation.service
```

To control the python server
```bash
sudo systemctl start primeserver.service
sudo systemctl stop primeserver.service
sudo systemctl status primeserver.service
```
