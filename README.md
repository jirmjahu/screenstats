# ScreenStats

A lightweight and customizable desktop overlay for Windows that displays system statistics in real-time

## Configuration

Edit `config.ini` in your config directory:

- **Installer**: `C:\ProgramData\ScreenStats\config\config.ini`
- **Portable**: Same directory as the executable

You can also open the configuration by right-clicking the system tray icon.

## Requirements

- Windows 10 Version 2004 or newer
- .NET 10 or higher

## Config

### Layout Options

| Key           | Default    | Description                          |
|---------------|------------|--------------------------------------|
| `orientation` | `Vertical` | `Vertical` or `Horizontal`           |
| `left`        | `25`       | Distance from the left screen edge   |
| `bottom`      | `25`       | Distance from the bottom screen edge |
| `width`       | `500`      | Width of the overlay                 |
| `spacing`     | `12`       | Space between widgets                |

### Background Options

| Key            | Default     | Description                       |
|----------------|-------------|-----------------------------------|
| `enabled`      | `true`      | Show or hide the background panel |
| `color`        | `#70000000` | Background hex color              |
| `padding`      | `15`        | Padding inside the background     |
| `cornerRadius` | `10`        | Rounded corners                   |

### Widgets

Each widget is a separate section like `[widgets:1]`, `[widgets:2]`, etc. You can add as many as you want.

**Common options** (works on all widgets):

| Key          | Description        |
|--------------|--------------------|
| `type`       | Type of the widget |
| `fontFamily` | Font to use        |
| `fontWeight` | Font weight to use |
| `fontStyle`  | Font style to use  |
| `size`       | Font size to use   |

#### Text (`type = text`)

A simple text widget.

| Key       | Description     |
|-----------|-----------------|
| `content` | Text to display |

Available placeholders: `{username}`, `{computer}`, `{os}`, `{arch}`, `{date}`, `{time}`, `{day}`, `{uptime}`, `{ip}`,
`{cpu_cores}`, `{ram_used}`, `{ram_total}`, `{ram_available}`.

#### CPU (`type = cpu`)

Shows CPU usage percentage.

| Key         | Description                           |
|-------------|---------------------------------------|
| `content`   | Label text (e.g. `CPU`)               |
| `valueSize` | Font size of the value text           |
| `color`     | Color of the value text and usage bar |
| `showBar`   | Show usage bar                        |

#### RAM (`type = ram`)

Shows RAM usage.

| Key         | Description                           |
|-------------|---------------------------------------|
| `content`   | Label text (e.g. `Memory`)            |
| `valueSize` | Font size of the value text           |
| `color`     | Color of the value text and usage bar |
| `showBar`   | Show usage bar                        |

#### Drive (`type = drive`)

Shows disk space usage.

| Key       | Description                                                      |
|-----------|------------------------------------------------------------------|
| `drive`   | Drive letter (e.g. `C:`)                                         |
| `content` | Text template (default: `{label} ({letter}) • {used} / {total}`) |
| `color`   | Color of the value text and usage bar                            |
| `showBar` | If the usage bar should be displayed                             |

Available placeholders: `{label}`, `{letter}`, `{used}`, `{total}`, `{free}`, `{percent}`.

#### Media (`type = media`)

Shows currently playing media info.

| Key             | Description                    |
|-----------------|--------------------------------|
| `content`       | Text template (e.g. `{title}`) |
| `color`         | Color for all the texts        |
| `showArtist`    | Show artist name               |
| `showStatus`    | Show play/pause status         |
| `showThumbnail` | Show album art                 |
| `thumbnailSize` | Size of the thumbnail          |

Available placeholders: `{title}`, `{artist}`, `{status}`, `{app}`, `{position}`, `{duration}`.

#### Weather (`type = weather`)

Shows current weather.

| Key               | Description                                                                      |
|-------------------|----------------------------------------------------------------------------------|
| `country`         | Country (e.g. `Germany`)                                                         |
| `city`            | City (e.g. `Berlin`)                                                             |
| `temperatureUnit` | `celsius` or `fahrenheit`                                                        |
| `windSpeedUnit`   | `kmh` or `mph`                                                                   |
| `content`         | Text template (default: `{icon} {location} • {temp}{temp_unit} • {description}`) |
| `color`           | Color for all the text                                                           |

Available placeholders: `{icon}`, `{description}`, `{temp}`, `{temp_unit}`, `{feels_like}`, `{humidity}`, `{wind}`,
`{wind_unit}`, `{location}`.

