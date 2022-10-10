## Introduction

ByteDance IconPark icon library in Blazor. 

> source repository: https://github.com/bytedance/iconpark
>
> project website：https://iconpark.oceanengine.com/home

**note**：IconPark System component is renamed to SystemIcon

# Usage

1. install IconPark.Blazor
2. @using IconPark
3. `<Camera />`, `<Camera  Size="24"/>`

| Property       | Type                       | Description                                                 | Default value            |
| -------------- | -------------------------- | ----------------------------------------------------------- | ------------------------ |
| Theme          | ThemeType（enum）          | theme                                                       | ThemeType.Outline        |
| Size           | int or string              | width and height                                            | 1em                      |
| StrokeWidth    | int                        | svg stroke-width                                            | 4                        |
| Fill           | List<string>               | svg fill                                                    | It depends on the theme. |
| StrokeLinecap  | StrokeLinecapType（enum）  | svg stroke-linecap. Not all icons SVG have this attribute.  | StrokeLinecapType.Round  |
| StrokeLinejoin | StrokeLinejoinType（enum） | svg stroke-linejoin. Not all icons SVG have this attribute. | StrokeLinejoinType.Round |

