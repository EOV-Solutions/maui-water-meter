# Changelog

## Unreleased

### Added
- `ScanOptions.ImageSaveDir`: directory to save the captured image (absolute path or `file://` URL, e.g. `FileSystem.AppDataDirectory`). Created if missing; falls back to the SDK default directory if not writable, so always use the returned `ScanResult.ImagePath`
- `ScanOptions.ImageFileName`: file name of the captured image (e.g. `KH001_202610.jpg`). `.jpg` is appended if missing; an existing file with the same name is overwritten

## 1.2.0 (2025-12-15)

### Added
- Initial .NET MAUI release
- Camera scanner with real-time OCR
- Image recognition from base64 and file path
- License management with metadata support
- SDK settings screen
- Camera permission handling
- Full API parity with Flutter and Cordova plugins
- Android support (API 23+)
- iOS support (12.0+)
- Demo application
