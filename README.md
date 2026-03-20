# 📱 Water Meter SDK for .NET MAUI

> SDK quét số đồng hồ nước sử dụng AI cho ứng dụng .NET MAUI.

[![Nền tảng](https://img.shields.io/badge/platform-Android%20%7C%20iOS-green.svg)](https://dotnet.microsoft.com/apps/maui)
[![Android](https://img.shields.io/badge/Android-%3E%3D6.0-brightgreen.svg)](https://www.android.com/)
[![iOS](https://img.shields.io/badge/iOS-%3E%3D12.2-blue.svg)](https://www.apple.com/ios/)
[![.NET](https://img.shields.io/badge/.NET-%3E%3D9.0-purple.svg)](https://dotnet.microsoft.com/)
[![Giấy phép](https://img.shields.io/badge/license-EOV-orange.svg)](LICENSE)

## ✨ Tính năng

- 📷 Xem trước camera thời gian thực với lớp phủ AI
- 🎯 Tự động phát hiện với ngưỡng độ tin cậy
- ⚡ Tự động chụp khi căn chỉnh đúng
- 🔦 Hỗ trợ đèn flash
- 🔍 Điều khiển zoom
- 📐 Phát hiện OBB (hình chữ nhật bao quanh)
- 🔒 Quản lý quyền camera
- 🖼️ Nhận diện từ ảnh base64 hoặc file
- 🔑 Quản lý license key & quota
- 📱 Hỗ trợ đa nền tảng:
  - **Android 6.0+** (API 23+)
  - **iOS 12.2+**
- 🎨 Giao diện tùy chỉnh
- 💾 Lưu ảnh và trả về đường dẫn
- 🖼️ Hỗ trợ Base64 để hiển thị ảnh

## 🛠️ Yêu cầu

| Yêu cầu | Chi tiết |
|---|---|
| **.NET** | 9.0 trở lên |
| **.NET MAUI workload** | `dotnet workload install maui` |
| **Android min SDK** | API 23 (Android 6.0) |
| **iOS min version** | 12.2 |
| **IDE** | Visual Studio 2022 17.12+, VS Code với C# Dev Kit, hoặc JetBrains Rider |
| **macOS** | Bắt buộc nếu build iOS (cần Xcode 15+) |

> **⚠️ Lưu ý:** iOS chỉ chạy trên thiết bị thật, không hỗ trợ Simulator (do native framework chỉ build cho arm64).

## 🚀 Cài đặt

### Cài đặt SDK

#### Cách 1: Project Reference (khuyến nghị cho phát triển)

```xml
<ProjectReference Include="path/to/WaterMeter.Maui/WaterMeter.Maui.csproj" />
```

#### Cách 2: NuGet Package (nếu đã publish)

```bash
dotnet add package WaterMeter.Maui
```

### Kiểm tra cài đặt

App của bạn cần target `net9.0-android` và/hoặc `net9.0-ios`:

```xml
<TargetFrameworks>net9.0-android;net9.0-ios</TargetFrameworks>
```

### Đăng ký SDK trong MauiProgram.cs

```csharp
using WaterMeter.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseWaterMeter()  // 👈 Thêm dòng này
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        return builder.Build();
    }
}
```

### Cấu hình quyền Camera

#### Android

Thêm vào `Platforms/Android/AndroidManifest.xml`:

```xml
<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.INTERNET" />
```

Override `OnActivityResult` trong `MainActivity.cs`:

```csharp
using WaterMeter.Maui.Platforms.Android;

public class MainActivity : MauiAppCompatActivity
{
    protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        if (!WaterMeterActivityResultHandler.HandleActivityResult(requestCode, resultCode, data))
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
```

#### iOS

Thêm vào `Platforms/iOS/Info.plist`:

```xml
<key>NSCameraUsageDescription</key>
<string>Ứng dụng cần quyền Camera để quét chỉ số đồng hồ nước</string>
```

### Xây dựng & chạy

**Android:**
```bash
# Build
dotnet build -f net9.0-android

# Build và cài lên thiết bị
dotnet build -f net9.0-android -t:Install

# Mở app sau khi cài
adb shell monkey -p <your.package.id> -c android.intent.category.LAUNCHER 1
```

**iOS (thiết bị thật):**
```bash
# Build
dotnet build -f net9.0-ios -p:RuntimeIdentifier=ios-arm64

# Build và deploy lên device
dotnet build -f net9.0-ios -t:Run -p:RuntimeIdentifier=ios-arm64
```

## 💻 Sử dụng

### ⚠️ Khởi tạo License (Bắt buộc)

**QUAN TRỌNG**: Bạn phải khởi tạo license trước khi sử dụng các tính năng quét. SDK sẽ không hoạt động nếu license chưa được kích hoạt.

```csharp
using WaterMeter.Maui;

// Cách 1: Chỉ với license key
var result = await WaterMeterSdk.InitializeLicenseAsync("YOUR_LICENSE_KEY");

if (result.Valid)
{
    Console.WriteLine("✓ License activated!");
    // result = {
    //   Valid: true,
    //   Status: LicenseStatus.Valid,  // (xem bảng Status Codes bên dưới)
    //   Message: "License is valid"
    // }
}
else
{
    Console.WriteLine($"✗ License error: {result.Message}");
}

// Cách 2: Với metadata và device user (để theo dõi trên admin)
// Lưu ý: metadataInfo và deviceUser là optional, có thể truyền 1 trong 2, cả 2, hoặc không truyền
var result = await WaterMeterSdk.InitializeLicenseAsync(
    "YOUR_LICENSE_KEY",
    metadataInfo: new Dictionary<string, object>
    {
        { "location", "Chi nhánh Quận 1" },
        { "customerId", "12345" },
        { "department", "Phòng Thu Ngân" }
    },
    deviceUser: "nhanvien@congty.com"
);
```

### Ví dụ cơ bản

```csharp
// Quét số đồng hồ nước
try
{
    var result = await WaterMeterSdk.ScanAsync();

    if (result.Success)
    {
        Console.WriteLine($"✓ Số: {result.Text}");
        Console.WriteLine($"Độ tin cậy: {result.Confidence * 100:F1}%");

        // Hiển thị ảnh đã chụp
        if (result.ImagePath != null)
        {
            Console.WriteLine($"Ảnh đã lưu tại: {result.ImagePath}");
        }
    }
}
catch (Exception ex)
{
    if (ex.Message.Contains("CANCELLED"))
    {
        Console.WriteLine("Người dùng hủy quét");
    }
    else
    {
        Console.WriteLine($"Lỗi: {ex.Message}");
    }
}
```

### Ví dụ nâng cao với tuỳ chọn

```csharp
var result = await WaterMeterSdk.ScanAsync(new ScanOptions
{
    Title = "Quét số đồng hồ nước",    // Tiêu đề màn hình quét
    ShowCloseButton = true,             // Hiện nút đóng (X)
    AutoCloseOnResult = true,           // Tự động đóng khi quét thành công
    AutoCapture = true,                 // Tự động chụp
    MinConfidence = 0.8,                // Ngưỡng tin cậy
    ImageMaxWidth = 1920,               // Resize ảnh về max width (giữ tỷ lệ)
    ImageMaxHeight = 1080               // Resize ảnh về max height (giữ tỷ lệ)
});

// result = {
//   Text: "00012345",                          // Số đồng hồ
//   Confidence: 0.95,                          // Độ tin cậy 0.0-1.0
//   Success: true,                             // true nếu có số
//   ImagePath: "/path/to/image.jpg",           // Đường dẫn ảnh
//   ImageBase64: "data:image/jpeg;base64,...",  // Base64
//   FormattedReading: "00012.345",             // Số đã format
//   IsReliable: true                           // Độ tin cậy cao
// }

// Format with decimal point (e.g., "12345678" → "12345.678")
var formatted = WaterMeterSdk.FormatReading(result.Text);
Console.WriteLine($"Formatted: {formatted} m³");
```

### Tích hợp hoàn chỉnh

```xml
<!-- MainPage.xaml -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="WaterMeterDemo.MainPage"
             Title="📱 Quét Số Đồng Hồ Nước">
    <VerticalStackLayout Padding="20" Spacing="10">
        <Button Text="Quét" Clicked="OnScanClicked" />
        <Button Text="Kiểm tra quyền" Clicked="OnCheckPermissionClicked" />
        <Button Text="Cài đặt" Clicked="OnSettingsClicked" />

        <Frame Padding="15" CornerRadius="8" BackgroundColor="#F0F0F0" IsVisible="{Binding HasResult}">
            <VerticalStackLayout Spacing="8">
                <Label Text="Kết quả:" FontSize="18" FontAttributes="Bold" />
                <Label x:Name="MeterValueLabel" Text="Số: -" FontSize="16" />
                <Label x:Name="ConfidenceLabel" Text="Độ tin cậy: -" FontSize="16" />
                <Image x:Name="CapturedImage" IsVisible="False" />
            </VerticalStackLayout>
        </Frame>
    </VerticalStackLayout>
</ContentPage>
```

```csharp
// MainPage.xaml.cs
using WaterMeter.Maui;

public partial class MainPage : ContentPage
{
    private bool _licenseValid = false;

    public MainPage()
    {
        InitializeComponent();
        InitLicense();
    }

    private async void InitLicense()
    {
        var result = await WaterMeterSdk.InitializeLicenseAsync(
            "YOUR_LICENSE_KEY",
            deviceUser: "nhanvien@congty.com"
        );
        _licenseValid = result.Valid;
        if (!result.Valid)
        {
            await DisplayAlert("Lỗi", $"Không thể kích hoạt license: {result.Message}", "OK");
        }
    }

    private async void OnScanClicked(object sender, EventArgs e)
    {
        if (!_licenseValid) return;

        try
        {
            // Kiểm tra quyền camera
            var permission = await WaterMeterSdk.RequestPermissionAsync();
            if (!permission.Granted)
            {
                await DisplayAlert("Lỗi", "Quyền camera bị từ chối", "OK");
                return;
            }

            // Mở camera scanner
            var result = await WaterMeterSdk.ScanAsync(new ScanOptions
            {
                Title = "Quét số đồng hồ nước",
                ShowCloseButton = true,
                ImageMaxWidth = 1920,
                ImageMaxHeight = 1080
            });

            if (result.Success)
            {
                MeterValueLabel.Text = $"Số: {result.Text}";
                ConfidenceLabel.Text = $"Độ tin cậy: {result.Confidence * 100:F1}%";

                if (result.ImagePath != null)
                {
                    CapturedImage.Source = ImageSource.FromFile(result.ImagePath);
                    CapturedImage.IsVisible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (!ex.Message.Contains("CANCELLED"))
            {
                await DisplayAlert("Lỗi", ex.Message, "OK");
            }
        }
    }

    private async void OnCheckPermissionClicked(object sender, EventArgs e)
    {
        var result = await WaterMeterSdk.CheckPermissionAsync();
        await DisplayAlert("Quyền camera",
            result.Granted ? "Đã cấp ✓" : "Chưa cấp ✗", "OK");
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await WaterMeterSdk.OpenSettingsAsync();
    }
}
```

### Nhận dạng từ ảnh

```csharp
// Từ base64
var result1 = await WaterMeterSdk.RecognizeBase64Async(base64ImageString);

// Từ file path
var result2 = await WaterMeterSdk.RecognizeFileAsync("/path/to/image.jpg");

Console.WriteLine($"Chỉ số: {result1.Text}, Tin cậy: {result1.Confidence}");
```

## 📖 API

### `WaterMeterSdk.InitializeLicenseAsync(key, metadataInfo?, deviceUser?)`

**⚠️ BẮT BUỘC** - Khởi tạo SDK với license key. Phải gọi trước khi sử dụng các tính năng khác.

**Tham số:**
- `key` (string, bắt buộc) - License key từ backend
- `metadataInfo` (Dictionary<string, object>?, tùy chọn) - Metadata gửi lên server để admin theo dõi
- `deviceUser` (string?, tùy chọn) - Email/ID người dùng thiết bị

**Trả về:** `Task<LicenseResult>`

```csharp
// Chỉ với license key
var result = await WaterMeterSdk.InitializeLicenseAsync("YOUR_LICENSE_KEY");

// Với metadata và device user
var result = await WaterMeterSdk.InitializeLicenseAsync(
    "YOUR_LICENSE_KEY",
    metadataInfo: new Dictionary<string, object>
    {
        { "location", "Chi nhánh Quận 1" },
        { "customerId", "12345" }
    },
    deviceUser: "nhanvien@congty.com"
);
```

---

### `WaterMeterSdk.IsLicenseValidAsync()`

Kiểm tra license hiện tại có hợp lệ không.

**Trả về:** `Task<LicenseResult>`

```csharp
var result = await WaterMeterSdk.IsLicenseValidAsync();
if (result.Valid)
{
    Console.WriteLine("License is active");
}
else
{
    Console.WriteLine($"License status: {result.Status}");
}
```

**Status Codes:**

| Mã | Hằng số | Ý nghĩa |
|----|---------|--------|
| 0 | NotInitialized | SDK chưa khởi tạo |
| 1 | Valid | License hợp lệ |
| 2 | Expired | License đã hết hạn |
| 3 | GracePeriod | License trong thời gian gia hạn |
| 4 | Invalid | License key không hợp lệ |
| 5 | Blocked | License bị khóa |
| 6 | QuotaExceeded | Đã vượt quota, cần sync |

---

### `WaterMeterSdk.ScanAsync(options?)`

Mở camera để quét số đồng hồ nước.

**Tham số:**
- `options` (ScanOptions?, tùy chọn) - Cấu hình scanner

**Trả về:** `Task<ScanResult>`

**Kết quả thành công:**

```csharp
new ScanResult
{
    Text = "00123",                              // Số đồng hồ (rỗng nếu thất bại)
    Confidence = 0.95,                           // Độ tin cậy 0.0-1.0
    Success = true,                              // true nếu có số
    ImagePath = "/path/to/image.jpg",            // Đường dẫn ảnh đã chụp
    ImageBase64 = "data:image/jpeg;base64,...",  // Base64 cho hiển thị
    FormattedReading = "00.123",                 // Số đã format
    IsReliable = true                            // Độ tin cậy cao
}
```

**Throws:** Exception với message `CANCELLED` khi người dùng hủy.

---

### `WaterMeterSdk.RecognizeBase64Async(base64Image)`

Nhận diện số đồng hồ từ ảnh base64.

**Tham số:**
- `base64Image` (string) - Ảnh dạng base64 (data URL)

**Trả về:** `Task<ScanResult>`

```csharp
var result = await WaterMeterSdk.RecognizeBase64Async(
    "data:image/jpeg;base64," + imageData
);
Console.WriteLine($"Kết quả: {result.Text}");
```

---

### `WaterMeterSdk.RecognizeFileAsync(filePath)`

Nhận diện số đồng hồ từ file ảnh.

**Tham số:**
- `filePath` (string) - Đường dẫn tới file ảnh

**Trả về:** `Task<ScanResult>`

```csharp
var result = await WaterMeterSdk.RecognizeFileAsync("/path/to/image.jpg");
Console.WriteLine($"Kết quả: {result.Text}");
```

---

### `WaterMeterSdk.InitializeAsync()`

Khởi tạo OCR engine (pre-load model). SDK tự động gọi khi scan lần đầu, nhưng có thể gọi trước để tăng tốc.

**Trả về:** `Task<bool>`

---

### `WaterMeterSdk.CheckPermissionAsync()`

Kiểm tra quyền camera.

**Trả về:** `Task<PermissionResult>` - `{ Granted: bool, Status: string }`

```csharp
var result = await WaterMeterSdk.CheckPermissionAsync();
Console.WriteLine($"Permission granted: {result.Granted}");
```

---

### `WaterMeterSdk.RequestPermissionAsync()`

Yêu cầu quyền camera từ người dùng.

**Trả về:** `Task<PermissionResult>`

```csharp
var result = await WaterMeterSdk.RequestPermissionAsync();
if (result.Granted)
{
    Console.WriteLine("Permission granted!");
    var scanResult = await WaterMeterSdk.ScanAsync();
}
else
{
    Console.WriteLine("Permission denied");
}
```

---

### `WaterMeterSdk.OpenSettingsAsync()`

Mở màn hình cài đặt SDK (auto-capture, confidence, flash, zoom...).

**Trả về:** `Task`

---

### `WaterMeterSdk.GetVersionAsync()`

Lấy version hiện tại của SDK.

**Trả về:** `Task<string>`

---

### `WaterMeterSdk.IsInitializedAsync()`

Kiểm tra SDK đã khởi tạo chưa.

**Trả về:** `Task<bool>`

---

### `WaterMeterSdk.ResetAsync()`

Giải phóng tài nguyên, reset SDK.

**Trả về:** `Task`

---

### `WaterMeterSdk.FormatReading(text, decimalPlaces?)`

Format kết quả nhận dạng với dấu chấm thập phân.

**Tham số:**
- `text` (string) - Chuỗi số cần format
- `decimalPlaces` (int?, tùy chọn) - Số chữ số thập phân (mặc định: 3)

**Trả về:** `string`

```csharp
var formatted = WaterMeterSdk.FormatReading("12345678");                    // "12345.678"
var formatted2 = WaterMeterSdk.FormatReading("12345678", decimalPlaces: 4); // "1234.5678"
```

## ⚙️ Tuỳ chọn cấu hình

### ScanOptions

| Tuỳ chọn | Kiểu | Mặc định | Mô tả |
|----------|------|----------|-------|
| `Title` | string | "Quét số đồng hồ" | Tiêu đề scanner |
| `ShowCloseButton` | bool | true | Hiện nút đóng (X) |
| `AutoCloseOnResult` | bool | true | Tự đóng khi có kết quả |
| `AutoCapture` | bool? | null (dùng settings SDK) | Tự động chụp |
| `MinConfidence` | double? | null (dùng settings SDK) | Ngưỡng tin cậy |
| `ImageMaxWidth` | int? | null (ảnh gốc) | Chiều rộng tối đa ảnh (px) |
| `ImageMaxHeight` | int? | null (ảnh gốc) | Chiều cao tối đa ảnh (px) |

**Lưu ý:** Resize ảnh giữ tỷ lệ. Nếu chỉ định cả width và height, ảnh sẽ fit trong bounds.

## 📱 Đặc điểm theo nền tảng

### Tính năng chung (Android & iOS)

- 🔑 Khởi tạo và quản lý license (`InitializeLicenseAsync`, `IsLicenseValidAsync`)
- 📷 Quét số đồng hồ nước (`ScanAsync`)
- 🔒 Quản lý quyền camera (`CheckPermissionAsync`, `RequestPermissionAsync`)
- 💾 Lưu ảnh và trả về đường dẫn (`ImagePath`)
- 🖼️ Trả về ảnh base64 (`ImageBase64`)
- 🖼️ Nhận diện từ ảnh (`RecognizeBase64Async`, `RecognizeFileAsync`)
- ⚙️ Màn hình cài đặt (`OpenSettingsAsync`)

### Lưu ý riêng theo nền tảng

- Trên **iOS**, ảnh scan được lưu tại `Library/Application Support/NoCloud/scanned_images/`.
- Trên **Android**, ảnh scan được lưu tại thư mục cache của ứng dụng.
- **iOS Simulator không hỗ trợ**: `WaterMeterSDK.framework` chỉ build cho `arm64` (device thật).

## 🔧 Khắc phục sự cố

### SDK không tìm thấy

```bash
# Kiểm tra project reference:
dotnet list reference

# Clean và build lại:
dotnet clean
dotnet restore
dotnet build
```

### Android: Không tìm thấy AAR

```bash
# Đảm bảo tệp AAR ở đúng vị trí:
ls WaterMeter.Maui/NativeLibs/Android/water_meter_sdk.aar

# Clean và build lại:
dotnet clean -f net9.0-android
dotnet build -f net9.0-android
```

### iOS: Framework Not Found

```bash
# Đảm bảo framework ở đúng vị trí:
ls WaterMeter.Maui/NativeLibs/iOS/WaterMeterSDK.framework

# Clean và rebuild:
dotnet clean -f net9.0-ios
dotnet build -f net9.0-ios -p:RuntimeIdentifier=ios-arm64
```

### iOS: Simulator không hoạt động

> `WaterMeterSDK.framework` chỉ hỗ trợ `arm64` (thiết bị thật). Build cho Simulator sẽ gặp linker error. Hãy luôn build và test trên device thật.

### Quyền camera bị từ chối

**Android** - Kiểm tra `Platforms/Android/AndroidManifest.xml` có:
```xml
<uses-permission android:name="android.permission.CAMERA" />
```

**iOS** - Kiểm tra `Platforms/iOS/Info.plist` có:
```xml
<key>NSCameraUsageDescription</key>
<string>Mô tả lý do cần camera</string>
```

### Lỗi build

**Android:**
```bash
dotnet clean
rm -rf bin obj
dotnet restore
dotnet build -f net9.0-android
```

**iOS:**
```bash
dotnet clean
rm -rf bin obj
dotnet restore
dotnet build -f net9.0-ios -p:RuntimeIdentifier=ios-arm64
```

## 📝 Lưu ý quan trọng

- **License phải được khởi tạo trước khi scan**. Camera vẫn mở nhưng OCR không chạy nếu license không hợp lệ.
- **Settings (auto-capture, confidence, flash...)** được quản lý bởi SDK, không cần truyền từ code. Dùng `OpenSettingsAsync()` để user tự cấu hình.
- **Quota** được tính tự động bởi SDK khi scan thành công. Sync quota với server mỗi 2 giờ.
- **Two-step init**: Sau khi license thành công, SDK tự động gọi `InitializeAsync()` để load OCR model khi scan lần đầu. Có thể gọi trước để pre-load.

## 📝 Lịch sử thay đổi

### Phiên bản 1.2.0

- **Nâng cấp .NET 9.0** - .NET 9.0 / MAUI 9.0
- **Hỗ trợ iOS** - Tích hợp đầy đủ cho iOS 12.2+
- **WaterMeterSDK.framework** - Pre-built framework cho iOS
- **Base64 image** - Trả về ảnh base64 (ImageBase64 trong ScanResult)
- **Nhận diện từ ảnh** - API `RecognizeBase64Async` và `RecognizeFileAsync`
- **Màn hình cài đặt** - API `OpenSettingsAsync`
- **Thông tin SDK** - API `GetVersionAsync` và `IsInitializedAsync`
- **License Management** - `InitializeLicenseAsync` với metadata & device user
- **Camera scanner** - Auto-capture
- **Permission handling** - `CheckPermissionAsync`, `RequestPermissionAsync`

### Phiên bản 1.0.0

- Nhận diện số đồng hồ thời gian thực bằng AI
- Điều khiển flash và zoom
- Tự động chụp khi độ tin cậy > 90%
- Hiển thị độ tin cậy
- Quản lý quyền camera
- UI tuỳ chỉnh
- Hỗ trợ Android 6.0+
- Lưu ảnh và trả về đường dẫn

## Cấu trúc dự án

```
maui-water-meter/                        # SDK Library
├── WaterMeter.Maui.sln
├── WaterMeter.Maui/
│   ├── WaterMeter.Maui.csproj          # Multi-target project (Android + iOS)
│   ├── Models.cs                        # Shared models (ScanResult, ScanOptions, etc.)
│   ├── IWaterMeterService.cs            # Platform interface
│   ├── WaterMeterSdk.cs                 # Main static API
│   ├── WaterMeterExtensions.cs          # MAUI DI registration
│   ├── NativeLibs/
│   │   ├── Android/water_meter_sdk.aar  # Android native SDK
│   │   └── iOS/WaterMeterSDK.framework  # iOS native SDK
│   └── Platforms/
│       ├── Android/
│       │   ├── WaterMeterServiceAndroid.cs
│       │   └── WaterMeterActivityResultHandler.cs
│       └── iOS/
│           ├── WaterMeterServiceiOS.cs
│           └── ApiDefinition.cs          # ObjC binding definitions
└── README.md

maui-water-meter-demo/                   # Demo App (separate project)
├── WaterMeterDemo.sln
├── WaterMeterDemo.csproj
├── MauiProgram.cs
├── MainPage.xaml / .cs
└── Platforms/
    ├── Android/MainActivity.cs
    └── iOS/Info.plist
```

## 📄 Giấy phép

[License](./LICENSE)

## 👥 Tác giả

EOV Solutions

---

*Làm bởi EOV Solutions*
