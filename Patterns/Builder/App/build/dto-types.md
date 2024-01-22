# Mongo DTO Class Model Builder Proposed Types

To keep only simple and appropriate types we will limit our builder to the following: 

- double / double?
- string
- object
- object[]
- byte[]
- bool / bool?
- long / long?
- decimal / decimal?
  - (Note - there is no exact equivelant data type in BSON, but mongodb uses IEEE 754)

## Notes

> By default we include ObjectId "Id" field

> For absolute precision decimal is included above


---

# Form Builder Logic

## Form Builder Types

- Index (List, includes Delete Button, search, paging)
- Detail (includes Delete Button)
- New (Insert)
- Edit

## Form Interfaces

- ILayoutPropertyLoader
    - LoadFormProps()
    - LoadFieldProps()
- IMetaDataLoader
    - Load()
- IDisplayBinder
    - MapUi()
- IDisplayGenerator
    - GetUi()
- IDataBinder
    - GetDataLogic()
- ILogicGenerator
    - GetLogic()
- ICodeGenerator
    - GetCode()

## Form Abstract Models

- FormComposer
    - LayoutPropertyLoader
        - ILayoutPropertyLoader
    - MetaLoader
        - IMetaDataLoader
    - UiComposer
        - IDisplayBinder
    - DataComposer
        - IDataBinder
    - LogicComposer
        - ILogicGenerator
    - CodeGenerator
        - ICodeGenerator

### FormComposer Structure

1. LoadLayoutProperties
1. LoadMetaData()
1. MapUiMeta()
1. GenerateUiCode()
1. GenerateDataLogic()
1. GenerateFormLogic()
1. ProduceCode()