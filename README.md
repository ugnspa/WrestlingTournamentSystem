# **Imtynių varžybų organizavimo sistema**

Ši sistema skirta **imtynių varžybų organizavimui**. 

### Pagrindiniai taikomosios srities objektai:
- **Varžybos** → **Varžybų svorio kategorija** → **Imtynininkas**

---

## **Pagrindinės sistemos naudotojų rolės:**

1. **Varžybų organizatorius**  
   Asmuo, atsakingas už varžybų ir jų svorio kategorijų kūrimą bei gali priskirti imtynininkus į tam tikras svorio kategorijas.

2. **Treneris**  
   Asmuo, kuris gali peržiūrėti savo imtynininkus.

3. **Svečias**  
   Naudotojas, kuris gali tik peržiūrėti informaciją apie renginius, svorio kategorijas ir imtynininkus. Svečias neturi teisės kurti ar redaguoti informacijos, tačiau gali gauti išsamią informaciją apie vykstančias arba planuojamas varžybas ir juose dalyvaujančius imtynininkus.

4. **Administratorius**  
   Asmuo, kuris turi visas teises sistemoje. Administratorius gali valdyti visas varžybas, svorio kategorijas ir imtynininkus bei užregistruoti nauja naudotoją.

---

## **Naudojamos technologijos:**

- **Backend**: .NET Core
- **Frontend**: React
- **Duomenų bazė**: MSSQL
- **Hostingas**: Azure

# Wrestling Tournament System API

## Overview

The Wrestling Tournament System API Responses

---

## Response Structure

All API responses follow a standard format:

```json
{
  "success": true,
  "status": 200,
  "message": "Description of the response",
  "data": {
    // The actual data returned from the API
  }
}
```

---

## Endpoints

### Tournaments

#### 1. **Get All Tournaments**

**Endpoint**: `GET /api/v1/Tournaments`

- **Description**: Retrieve a list of all tournaments.

- **Responses**:

  - **200 OK**: List of tournaments.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "List of tournaments",
      "data": [
        {
            "id": 1,
            "name": "Lithuanian Wrestling Championship 2024",
            "location": "Vilnius",
            "startDate": "2024-05-15T00:00:00",
            "endDate": "2024-05-18T00:00:00",
            "tournamentStatus": {
                "id": 1,
                "name": "Closed"
            },
            "organiserId": "id"
        },
        {
            "id": 2,
            "name": "Lithuanian Wrestling Open 2023",
            "location": "Kaunas",
            "startDate": "2023-06-20T00:00:00",
            "endDate": "2023-06-22T00:00:00",
            "tournamentStatus": {
                "id": 2,
                "name": "Registration"
            },
            "organiserId": "id"
        },
      ]
    }
    ```

---

#### 2. **Get Tournament by ID**

**Endpoint**: `GET /api/v1/Tournaments/{id}`

- **Description**: Retrieve details of a specific tournament.

- **Parameters**:

  - `id` (integer): The ID of the tournament.

- **Responses**:

  - **200 OK**: Tournament details.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Tournament details",
      "data": {
        "id": 1,
        "name": "Lithuanian Wrestling Championship 2024",
        "location": "Vilnius",
        "startDate": "2024-05-15T00:00:00",
        "endDate": "2024-05-18T00:00:00",
        "tournamentStatus": {
            "id": 1,
            "name": "Closed"
        },
        "organiserId": "id"
    }
    }
    ```
  - **404 Not Found**: Tournament not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Tournament not found",
      "data": null
    }
    ```

---

#### 3. **Delete Tournament**

**Endpoint**: `DELETE /api/v1/Tournaments/{id}`

- **Description**: Delete a specific tournament.

- **Parameters**:

  - `id` (integer): The ID of the tournament to delete.

- **Responses**:

  - **200 OK**: Tournament successfully deleted.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Tournament successfully deleted",
      "data": null
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **404 Not Found**: Tournament not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Tournament not found",
      "data": null
    }
    ```

---

#### 4. **Update Tournament**

**Endpoint**: `PUT /api/v1/Tournaments/{id}`

- **Description**: Update details of an existing tournament.

- **Parameters**:

  - `id` (integer): The ID of the tournament to update.

- **Request Body**:
  ```json
  {
    "name": "Updated Championship Name",
    "location": "Updated Location",
    "startDate": "2024-06-15",
    "endDate": "2024-06-20",
    "StatusId": 1
  }
  ```

- **Responses**:

  - **200 OK**: Tournament updated successfully.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Tournament updated successfully",
      "data": {
        "id": 1,
        "name": "Updated Championship Name",
        "location": "Updated Location",
        "startDate": "2024-06-15",
        "endDate": "2024-06-20",
        "tournamentStatus": {
                "id": 1,
                "name": "Closed"
            },
        "organiserId": "id"
    }
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **404 Not Found**: Tournament not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Tournament not found",
      "data": null
    }
    ```

---

#### 5. **Get Tournament Statuses**

**Endpoint**: `GET /api/v1/Tournaments/Statuses`

- **Description**: Retrieve a list of all tournament statuses.

- **Responses**:

  - **200 OK**: List of tournament statuses.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "List of tournament statuses",
      "data": [
        {
          "id": 1,
          "name": "Open"
        },
        {
          "id": 2,
          "name": "Closed"
        },
        {
          "id": 3,
          "name": "Ongoing"
        }
      ]
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```


---

### Tournament Weight Categories

#### 6. **Get All Tournament Weight Categories**

**Endpoint**: `GET /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories`

- **Description**: Retrieve a list of all weight categories for a specific tournament.

- **Parameters**:

  - `tournamentId` (integer): The ID of the tournament.

- **Responses**:

  - **200 OK**: List of weight categories.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "List of weight categories",
      "data": [
        {
            "id": 1,
            "startDate": "2024-05-15T00:00:00",
            "endDate": "2024-05-18T00:00:00",
            "tournamentWeightCategoryStatus": {
                "id": 2,
                "name": "Registration"
            },
            "weightCategory": {
                "id": 1,
                "weight": 60,
                "age": "Seniors",
                "primaryCategory": true,
                "styleId": 1,
                "wrestlingStyle": {
                    "id": 1,
                    "name": "GR"
                }
            }
        },
        {
            "id": 2,
            "startDate": "2024-05-15T00:00:00",
            "endDate": "2024-05-16T00:00:00",
            "tournamentWeightCategoryStatus": {
                "id": 1,
                "name": "Closed"
            },
            "weightCategory": {
                "id": 2,
                "weight": 63,
                "age": "Seniors",
                "primaryCategory": true,
                "styleId": 1,
                "wrestlingStyle": {
                    "id": 1,
                    "name": "GR"
                }
            }
        },
      ]
    }
    ```
  - **404 Not Found**: Tournament with id was not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Not Found",
      "data": null
    }
    ```
  
    ```

---

#### 7. **Get Specific Tournament Weight Category**

**Endpoint**: `GET /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}`

- **Description**: Retrieve details of a specific weight category within a tournament.

- **Parameters**:

  - `tournamentId` (integer): The ID of the tournament.
  - `weightCategoryId` (integer): The ID of the weight category.

- **Responses**:

  - **200 OK**: Weight category details.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Weight category details",
      "data": {
        "id": 1,
        "startDate": "2024-05-15T00:00:00",
        "endDate": "2024-05-18T00:00:00",
        "tournamentWeightCategoryStatus": {
            "id": 2,
            "name": "Registration"
        },
        "weightCategory": {
            "id": 1,
            "weight": 60,
            "age": "Seniors",
            "primaryCategory": true,
            "styleId": 1,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            }
        }
      }
    }
    ```
  - **404 Not Found**: Weight category or tournament not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Weight category or tournament not found",
      "data": null
    }
    ```

---

#### 8. **Create Tournament Weight Category**

**Endpoint**: `POST /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories`

- **Description**: Create a new weight category for a specific tournament.

- **Parameters**:

  - `tournamentId` (integer): The ID of the tournament.

- **Request Body**:
  ```json
  {
    "startDate": "2024-06-10",
    "endDate": "2024-06-15"
    "fk_WeightCategoryId": 1,
  }
  ```

- **Responses**:

  - **201 Created**: Weight category successfully created.
    ```json
    {
      "success": true,
      "status": 201,
      "message": "Weight category created successfully",
      "data": {
        "id": 1,
        "startDate": "2024-05-15T00:00:00",
        "endDate": "2024-05-18T00:00:00",
        "tournamentWeightCategoryStatus": {
            "id": 2,
            "name": "Registration"
        },
        "weightCategory": {
            "id": 1,
            "weight": 60,
            "age": "Seniors",
            "primaryCategory": true,
            "styleId": 1,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            }
        }
      }
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **422 Unprocessable Entity**: Invalid data (e.g., dates out of range).
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Invalid data",
      "data": null
    }
    ```

---

#### 9. **Delete Tournament Weight Category**

**Endpoint**: `DELETE /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}`

- **Description**: Delete a specific weight category within a tournament.

- **Parameters**:

  - `tournamentId` (integer): The ID of the tournament.
  - `weightCategoryId` (integer): The ID of the weight category to delete.

- **Responses**:

  - **200 OK**: Weight category successfully deleted.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Weight category successfully deleted",
      "data": null
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **404 Not Found**: Weight category or tournament not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Weight category or tournament not found",
      "data": null
    }
    ```

---

#### 10. **Update Tournament Weight Category**

**Endpoint**: `PUT /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}`

- **Description**: Update details of a specific weight category within a tournament.

- **Parameters**:

  - `tournamentId` (integer): The ID of the tournament.
  - `weightCategoryId` (integer): The ID of the weight category to update.

- **Request Body**:
  ```json
  {
    "startDate": "2024-06-12",
    "endDate": "2024-06-18"
    "statusId": 2,
  }
  ```

- **Responses**:

  - **200 OK**: Weight category updated successfully.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Weight category updated successfully",
      "data": {
        "id": 1,
        "startDate": "2024-05-15T00:00:00",
        "endDate": "2024-05-18T00:00:00",
        "tournamentWeightCategoryStatus": {
            "id": 2,
            "name": "Registration"
        },
        "weightCategory": {
            "id": 2,
            "weight": 60,
            "age": "Seniors",
            "primaryCategory": true,
            "styleId": 1,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            }
        }
      }
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **404 Not Found**: Weight category or tournament not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Weight category or tournament not found",
      "data": null
    }
    ```
  - **422 Unprocessable Entity**: Invalid data (e.g., dates out of range).
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Invalid data",
      "data": null
    }
    ```

---

### Tournament Weight Category Statuses

#### 11. **Get All Tournament Weight Category Statuses**

**Endpoint**: `GET /api/v1/TournamentWeightCategories/Statuses`

- **Description**: Retrieve a list of all tournament weight category statuses.

- **Responses**:

  - **200 OK**: List of statuses.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "List of tournament weight category statuses",
      "data": [
        {
            "id": 1,
            "name": "Closed"
        },
        {
            "id": 2,
            "name": "Registration"
        },
        {
            "id": 3,
            "name": "Weigh-In"
        },
      ]
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```

---

### Weight Categories

#### 12. **Get All Weight Categories**

**Endpoint**: `GET /api/v1/TournamentWeightCategories/WeightCategories`

- **Description**: Retrieve a list of all weight categories.

- **Responses**:

  - **200 OK**: List of weight categories.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "List of weight categories",
      "data": [
        {
            "id": 1,
            "weight": 60,
            "age": "Seniors",
            "primaryCategory": true,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            }
        },
        {
            "id": 2,
            "weight": 63,
            "age": "Seniors",
            "primaryCategory": true,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            }
        },
      ]
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
---

#### 13. **Get All Wrestlers in a Tournament Weight Category**

**Endpoint**: `GET /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/Wrestlers`

- **Description**: Retrieves all wrestlers within a specific tournament and weight category.
- **Parameters**:
  - `tournamentId` (integer): The tournament's identifier.
  - `weightCategoryId` (integer): The weight category's identifier.
- **Responses**:
  - **200 OK**: Returns a list of wrestlers.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Wrestlers",
      {
            "id": 5,
            "name": "Arunas",
            "surname": "Vilkas",
            "country": "Lithuania",
            "birthDate": "1990-05-30T00:00:00",
            "photoUrl": null,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            },
            "coachName": "Coach Name",
            "coachId": "id"
        },
        {
            "id": 7,
            "name": "Mantas",
            "surname": "Petrauskas",
            "country": "Lithuania",
            "birthDate": "1992-11-22T00:00:00",
            "photoUrl": null,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            },
            "coachName": "Coach Name2",
            "coachId": "id"
        },
      ]
    }
    ```
  - **404 Not Found**: If the tournament or weight category is not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Tournament or weight category not found",
      "data": null
    }
    ```

---

#### 14. **Get Specific Wrestler in a Tournament Weight Category**

**Endpoint**: `GET /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/Wrestlers/{wrestlerId}`

- **Description**: Retrieves a specific wrestler within a tournament and weight category.
- **Parameters**:
  - `tournamentId` (integer): The tournament's identifier.
  - `weightCategoryId` (integer): The weight category's identifier.
  - `wrestlerId` (integer): The wrestler's identifier.
- **Responses**:
  - **200 OK**: Details of the requested wrestler.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Wrestler by id",
      "data": {
        "id": 5,
        "name": "Arunas",
        "surname": "Vilkas",
        "country": "Lithuania",
        "birthDate": "1990-05-30T00:00:00",
        "photoUrl": null,
        "wrestlingStyle": {
            "id": 1,
            "name": "GR"
        },
        "coachName": "Coach Name",
        "coachId": "id"
      }
    }
    ```
  - **404 Not Found**: If the wrestler, tournament, or weight category is not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Wrestler, tournament, or weight category not found",
      "data": null
    }
    ```

---

#### 15. **Create Wrestler in a Tournament Weight Category**

**Endpoint**: `POST /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/Wrestlers`

- **Description**: Adds a new wrestler to a tournament weight category.
- **Parameters**:
  - `tournamentId` (integer): The tournament's identifier.
  - `weightCategoryId` (integer): The weight category's identifier.
- **Request Body**:
  ```json
  {
  "name": "TestWrestler",
  "surname": "TestWrestler",
  "country": "Poland",
  "birthDate": "1999-10-13T14:15:50.478Z",
  "styleId": 2,
  "coachId": "id"
  }
  ```
- **Responses**:
  - **201 Created**: Wrestler created successfully.
    ```json
    {
      "success": true,
      "status": 201,
      "message": "Created Wrestler",
      "data": {
        "id": 5,
        "name": "Arunas",
        "surname": "Vilkas",
        "country": "Lithuania",
        "birthDate": "1990-05-30T00:00:00",
        "photoUrl": null,
        "wrestlingStyle": {
            "id": 1,
            "name": "GR"
        },
        "coachName": "Coach Name",
        "coachId": "id
      }
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **422 Unprocessable Entity**: Invalid data (e.g., future birthdate).
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Invalid data",
      "data": null
    }
    ```

---

#### 16. **Delete Wrestler from a Tournament Weight Category**

**Endpoint**: `DELETE /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/Wrestlers/{wrestlerId}`

- **Description**: Removes a wrestler from a tournament weight category.
- **Parameters**:
  - `tournamentId` (integer): The tournament's identifier.
  - `weightCategoryId` (integer): The weight category's identifier.
  - `wrestlerId` (integer): The wrestler's identifier.
- **Responses**:
  - **200 OK**: Wrestler successfully removed.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Wrestler successfully removed",
      "data": null
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **404 Not Found**: Wrestler, tournament, or weight category not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Wrestler, tournament, or weight category not found",
      "data": null
    }
    ```

---

#### 17. **Update Wrestler in a Tournament Weight Category**

**Endpoint**: `PUT /api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/Wrestlers/{wrestlerId}`

- **Description**: Updates the details of a wrestler in a tournament weight category.
- **Parameters**:
  - `tournamentId` (integer): The tournament's identifier.
  - `weightCategoryId` (integer): The weight category's identifier.
  - `wrestlerId` (integer): The wrestler's identifier.
- **Request Body**:
  ```json
  {
  "name": "TestWrestler",
  "surname": "TestWrestler",
  "country": "Poland",
  "birthDate": "1999-10-13T14:15:50.478Z",
  "styleId": 2,
  "coachId": "id",
  "photoUrl" null
  }
  ```
- **Responses**:
  - **200 OK**: Wrestler updated successfully.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Wrestler Updated",
      "data": {
        "id": 5,
        "name": "Arunas",
        "surname": "Vilkas",
        "country": "Lithuania",
        "birthDate": "1990-05-30T00:00:00",
        "photoUrl": null,
        "wrestlingStyle": {
            "id": 1,
            "name": "GR"
        },
        "coachName": "Coach Name",
        "coachId": "id"
      }
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **422 Unprocessable Entity**: Invalid data.
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Invalid data",
      "data": null
    }
    ```
  - **404 Not Found**: Wrestler, tournament, or weight category not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Wrestler, tournament, or weight category not found",
      "data": null
    }
    ```

---

#### 18. **Get Wrestler by ID**

**Endpoint**: `GET /api/v1/Wrestlers/{id}`

- **Description**: Retrieves a wrestler by their identifier.
- **Parameters**:
  - `id` (integer): The wrestler's identifier.
- **Responses**:
  - **200 OK**: Wrestler details.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Wrestler by id",
      "data": {
        "id": 5,
        "name": "Arunas",
        "surname": "Vilkas",
        "country": "Lithuania",
        "birthDate": "1990-05-30T00:00:00",
        "photoUrl": null,
        "wrestlingStyle": {
            "id": 1,
            "name": "GR"
        },
        "coachName": "Coach Coach",
        "coachId": "id"
      }
    }
    ```
  - **404 Not Found**: Wrestler not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Wrestler not found",
      "data": null
    }
    ```

---

#### 19. **Get All Wrestlers**

**Endpoint**: `GET /api/v1/Wrestlers`

- **Description**: Retrieves all wrestlers.
- **Responses**:
  - **200 OK**: List of all wrestlers.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "All Wrestlers",
      "data": [
        {
            "id": 5,
            "name": "Arunas",
            "surname": "Vilkas",
            "country": "Lithuania",
            "birthDate": "1990-05-30T00:00:00",
            "photoUrl": null,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            },
            "coachName": "Coach",
            "coachId": "id"
        },
        {
            "id": 7,
            "name": "Mantas",
            "surname": "Petrauskas",
            "country": "Lithuania",
            "birthDate": "1992-11-22T00:00:00",
            "photoUrl": null,
            "wrestlingStyle": {
                "id": 1,
                "name": "GR"
            },
            "coachName": "Coach 2",
            "coachId": "id"
        },
      ]
    }
    ```

---

#### 20. **Get All Wrestling Styles**

**Endpoint**: `GET /api/v1/Wrestlers/WrestlingStyles`

- **Description**: Retrieves all wrestling styles.
- **Responses**:
  - **200 OK**: List of wrestling styles.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Wrestling Styles",
      "data": [
        {
            "id": 1,
            "name": "GR"
        },
        {
            "id": 2,
            "name": "FS"
        },
        {
            "id": 3,
            "name": "WW"
        },
      ]
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```

---


### Accounts

#### 21. **Register User**

**Endpoint**: `POST /api/v1/Accounts/Register`

- **Description**: Register a new user to the system.
- **Request Body**:
  ```json
  {
    "userName": "johndoe",
    "email": "john.doe@example.com",
    "name": "John",
    "surname": "Doe",
    "city": "New York",
    "password": "Password123!",
    "confirmPassword": "Password123!",
    "roleId": "admin"
  }
  ```
- **Responses**:
  - **201 Created**: User successfully registered.
    ```json
    {
      "success": true,
      "status": 201,
      "message": "User registered",
      "data": {
        "id": "new-user-id",
        "userName": "johndoe"
      }
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **422 Unprocessable Entity**: Invalid data (e.g., passwords do not match).
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Passwords do not match or username already taken",
      "data": null
    }
    ```

---

#### 22. **Login User**

**Endpoint**: `POST /api/v1/Accounts/Login`

- **Description**: Log in a user to the system.
- **Request Body**:
  ```json
  {
    "userName": "johndoe",
    "password": "Password123!"
  }
  ```
- **Responses**:
  - **200 OK**: User successfully logged in.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Successful login",
      "data": {
        "accessToken": "jwt-token"
      }
    }
    ```
  - **422 Unprocessable Entity**: Invalid login credentials.
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Invalid username or password",
      "data": null
    }
    ```

---

#### 23. **Get Access Token**

**Endpoint**: `POST /api/v1/Accounts/AccessToken`

- **Description**: Get a new access token using a refresh token.
- **Responses**:
  - **200 OK**: New access token issued.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Access token refreshed",
      "data": {
        "accessToken": "new-jwt-token"
      }
    }
    ```
  - **422 Unprocessable Entity**: Refresh token not found or invalid.
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Refresh token not found or session invalid",
      "data": null
    }
    ```

---

#### 24. **Logout User**

**Endpoint**: `POST /api/v1/Accounts/Logout`

- **Description**: Log out a user from the system and invalidate the refresh token.
- **Responses**:
  - **200 OK**: Successfully logged out.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Logout successful",
      "data": null
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```
  - **422 Unprocessable Entity**: Refresh token not found.
    ```json
    {
      "success": false,
      "status": 422,
      "message": "Refresh token not found",
      "data": null
    }
    ```

---

#### 25. **Get All Coaches**

**Endpoint**: `GET /api/v1/Accounts/Coaches`

- **Description**: Retrieves a list of all coaches.
- **Responses**:
  - **200 OK**: List of coaches.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Coaches",
      "data": [
        {
            "id": "id",
            "name": "Coach",
            "surname": "Coach",
            "city": "CoachCity",
            "email": "coach@coach.com"
        }
         {
            "id": "id",
            "name": "Coach1",
            "surname": "Coach1",
            "city": "CoachCity2",
            "email": "coach2@coach.com"
        }
      ]
    }
    ```

---

#### 26. **Get Coach with Wrestlers**

**Endpoint**: `GET /api/v1/Accounts/Coaches/{id}`

- **Description**: Retrieves a specific coach and their associated wrestlers.
- **Parameters**:
  - `id` (string): The coach's identifier.
- **Responses**:
  - **200 OK**: Coach details with wrestlers.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Coach with wrestlers",
      "data": {
        "id": "ffc1ce78-02de-4ff8-a160-4001e5dad762",
        "name": "Coach",
        "surname": "Coach",
        "city": "CoachCity",
        "email": "coach@coach.com",
        "wrestlers": []
      }
    }
    ```
  - **404 Not Found**: Coach not found.
    ```json
    {
      "success": false,
      "status": 404,
      "message": "Coach not found",
      "data": null
    }
    ```

---

#### 27. **Get Admin with Wrestlers**

**Endpoint**: `GET /api/v1/Accounts/Admin`

- **Description**: Retrieves admin details with associated wrestlers.
- **Responses**:
  - **200 OK**: Admin details.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Admin with Wrestlers",
      "data": {
        "id": "c661153b-2669-4502-93a1-3d1eb28f27ab",
        "name": "Admin",
        "surname": "Admin",
        "city": "AdminCity",
        "email": "admin@admin.com",
        "wrestlers": []
      }
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```

---

#### 28. **Get All Roles**

**Endpoint**: `GET /api/v1/Accounts/Roles`

- **Description**: Retrieves a list of all user roles.
- **Responses**:
  - **200 OK**: List of user roles.
    ```json
    {
      "success": true,
      "status": 200,
      "message": "Roles",
      "data": [
        {
          "id": "admin",
          "name": "Administrator"
        },
        {
          "id": "coach",
          "name": "Coach"
        }
      ]
    }
    ```
  - **401 Not Authorized**: User is not authorized.
    ```json
    {
      "success": false,
      "status": 401,
      "message": "Not authorized",
      "data": null
    }
    ```
  - **403 Forbidden**: Access is forbidden.
    ```json
    {
      "success": false,
      "status": 403,
      "message": "Forbidden access",
      "data": null
    }
    ```










