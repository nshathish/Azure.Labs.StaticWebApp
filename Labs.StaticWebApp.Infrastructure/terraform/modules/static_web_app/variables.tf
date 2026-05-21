variable "name" {
  description = "Name of the Static Web App resource."
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group that contains the Static Web App."
  type        = string
}

variable "location" {
  description = "Azure region for the Static Web App."
  type        = string
}

variable "sku_tier" {
  description = "SKU tier: Free or Standard."
  type        = string
  default     = "Free"
}

variable "preview_environments_enabled" {
  description = "Allow pull-request preview environments (requires Standard SKU)."
  type        = bool
  default     = false
}

variable "tags" {
  description = "Tags to apply to the Static Web App."
  type        = map(string)
  default     = {}
}

variable "allowed_cors_origins" {
  description = "Comma-separated list of origins allowed to make cross-origin requests (AllowedCorsOrigins app setting)."
  type        = string
  default     = ""
}
