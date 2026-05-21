output "id" {
  description = "Resource ID of the Static Web App."
  value       = azurerm_static_web_app.this.id
}

output "default_host_name" {
  description = "Default hostname assigned by Azure (without https://)."
  value       = azurerm_static_web_app.this.default_host_name
}

output "api_key" {
  description = "Deployment token used by CI/CD pipelines to publish the app and API."
  value       = azurerm_static_web_app.this.api_key
  sensitive   = true
}
