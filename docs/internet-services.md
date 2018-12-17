# Vocup internet services

## Version 1

Endpoint: https://vectordata.de/vocup/v1/

### Concept
* Check for updates at startup
* Download installer if not existing (check SHA256)
* Start update on window close

### Reference
Example:

````json
{
	"Version": "1.6.0"
	"Integrity": "sha256-sUtCjwBeDlEjqK5j2uJY1wPYGQpyPxPj/72ARyI2y+Y="
	"Mirror": "https://github.com/daniel-lerch/vocup/releases/download/v1.6.0/Vocup_1.6.0.exe"
}
```