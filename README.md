# PetDesk.Challenge
## Requirements
- Redis (for mac/linux)
- Memurai (for windows) (drop-in alternative for Redis on windows)

## Setup
- Install redis/memurai
- Run app (the first time it'll take about a minute while it initializes the cache)

## Endpoints
### Distribution
`GET {host}/api/distribution`

### Best on-time performance
`GET {host}/api/performance`

### Airport recommendations
`GET {host}/api/recommendations?number=<int>` (optional, default 3)
