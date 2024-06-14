# Microsoft Recommended Documentation and GFM (GitHub Flavored Markdown) samples

### Linked image

[![alt text for linked image](elm/design/img/test-rocket-header.svg)](https://dot.net)

```md
[![alt text for linked image](elm/design/img/test-rocket-header.svg)](https://dot.net)
```

### Link to Other Repository

[TestRocket readme](https://github.com/JohnPKosh/TestRocket/blob/master/README.md)



### Alerts (Note, Tip, Important, Caution, Warning)
Alerts are a Markdown extension to create block quotes that render on Microsoft Learn with colors and icons that indicate the significance of the content.

Avoid notes, tips, and important boxes. Readers tend to skip over them. It's better to put that info directly into the article text.

If you need to use alerts, limit them to one or two per article. Multiple notes should never be next to each other in an article.

The following alert types are supported:

```md
> [!NOTE]
> Information the user should notice even if skimming.

> [!TIP]
> Optional information to help a user be more successful.

> [!IMPORTANT]
> Essential information required for user success.

> [!CAUTION]
> Negative potential consequences of an action.

> [!WARNING]
> Dangerous certain consequences of an action.
```

Displays as:

> [!NOTE]
> Information the user should notice even if skimming.

> [!TIP]
> Optional information to help a user be more successful.

> [!IMPORTANT]
> Essential information required for user success.

> [!CAUTION]
> Negative potential consequences of an action.

> [!WARNING]
> Dangerous certain consequences of an action.
