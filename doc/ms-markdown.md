# Microsoft Recommended Documentation and GFM (GitHub Flavored Markdown) samples

### Linked image

[![alt text for linked image](/elm/design/img/test-rocket-header.svg)](https://dot.net)

```md
[![alt text for linked image](/elm/design/img/test-rocket-header.svg)](https://dot.net)
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


### Angle brackets
If you use angle brackets in text in your file (for example, to denote a placeholder), you need to manually encode the angle brackets. Otherwise, Markdown thinks that they're intended to be an HTML tag.

For example, encode `<script name>` as `&lt;script name&gt;` or `\<script name>`.

Angle brackets don't have to be escaped in text formatted as inline code or in code blocks.

### Comments
Microsoft Learn supports HTML comments if you must comment out sections of your article:

```md
<!--- Here's my comment --->
```

<!--- Here's my comment - Note* this is not visible :-) --->


## Tables
The simplest way to create a table in Markdown is to use pipes and lines. To create a standard table with a header, follow the first line with dashed line:

```md
|This is   |a simple   |table header|
|----------|-----------|------------|
|table     |data       |here        |
|it doesn't|actually   |have to line up nicely!|
```

Renders as:

|This is   |a simple   |table header|
|----------|-----------|------------|
|table     |data       |here        |
|it doesn't|actually   |have to line up nicely!|

**You can align the columns by using colons:**

```md
| Fun                  | With                 | Tables          |
| :------------------- | -------------------: |:---------------:|
| left-aligned column  | right-aligned column | centered column |
| $100                 | $100                 | $100            |
| $10                  | $10                  | $10             |
| $1                   | $1                   | $1              |
```

Renders as:

| Fun                  | With                 | Tables          |
| :------------------- | -------------------: |:---------------:|
| left-aligned column  | right-aligned column | centered column |
| $100                 | $100                 | $100            |
| $10                  | $10                  | $10             |
| $1                   | $1                   | $1              |


### Data matrix tables
A data matrix table has both a header and a weighted first column, creating a matrix with an empty cell in the top left. Microsoft Learn has custom Markdown for data matrix tables:

```md
|                  |Header 1 |Header 2|
|------------------|---------|--------|
|**First column A**|Cell 1A  |Cell 2A |
|**First column B**|Cell 1B  |Cell 2B |
```

The example renders as:

|                  |Header 1 |Header 2|
|------------------|---------|--------|
|**First column A**|Cell 1A  |Cell 2A |
|**First column B**|Cell 1B  |Cell 2B |
