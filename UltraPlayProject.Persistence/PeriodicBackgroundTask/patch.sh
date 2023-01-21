curl -X 'PATCH' \
  'https://localhost:7176/background' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "isEnabled": true
}'