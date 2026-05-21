output "prefix" {
  description = "Full prefix used for all resource names (base + 3-digit suffix)."
  value       = local.full_prefix
}

output "resource_group_name" {
  description = "Name of the provisioned resource group."
  value       = azurerm_resource_group.main.name
}

output "static_web_app_url" {
  description = "Default HTTPS hostname of the Static Web App."
  value       = "https://${module.static_web_app.default_host_name}"
}

output "static_web_app_api_key" {
  description = "Deployment API key — use this in your CI/CD pipeline."
  value       = module.static_web_app.api_key
  sensitive   = true
}

output "static_web_app_id" {
  description = "Resource ID of the Static Web App."
  value       = module.static_web_app.id
}
