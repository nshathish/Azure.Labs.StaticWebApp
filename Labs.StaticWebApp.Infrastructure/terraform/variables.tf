variable "prefix" {
  description = "Base name prefix; a 3-digit random suffix is appended automatically."
  type        = string
  default     = "az204labs"
}

variable "location" {
  description = "Azure region for all resources."
  type        = string
  default     = "eastus2"
}

variable "environment" {
  description = "Deployment environment label used in tags."
  type        = string
  default     = "dev"
}

variable "sku_tier" {
  description = "Static Web App SKU tier: Free or Standard."
  type        = string
  default     = "Free"

  validation {
    condition     = contains(["Free", "Standard"], var.sku_tier)
    error_message = "sku_tier must be 'Free' or 'Standard'."
  }
}
