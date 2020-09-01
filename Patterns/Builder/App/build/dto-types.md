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