# Rest Patients API
## To build and run this piece of code into image and then container and be able to not change anything in postman requests use script:

```
instantLocalDeploy.ps1
```

## Don't have powershell? Bad luck, try another time. Or use commands below:

```
docker build -t patientrest:latest -f ./PatientREST/Dockerfile .
docker run -d -p 5000:8080 --name patientrest_container patientrest:latest
```