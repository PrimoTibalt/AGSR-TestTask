docker build -t patientrest:latest -f ./PatientREST/Dockerfile .
docker run -d -p 5000:8080 --name patientrest_container patientrest:latest