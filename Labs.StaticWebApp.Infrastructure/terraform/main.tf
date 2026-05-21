resource "random_integer" "suffix" {
  min = 100
  max = 999
}

locals {
  full_prefix = "${var.prefix}${random_integer.suffix.result}"

  common_tags = {
    environment = var.environment
    project     = "Labs.StaticWebApp"
    managed_by  = "terraform"
  }
}

resource "azurerm_resource_group" "main" {
  name     = "rg-${local.full_prefix}"
  location = var.location
  tags     = local.common_tags
}

module "static_web_app" {
  source = "./modules/static_web_app"

  name                = "swa-${local.full_prefix}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  sku_tier            = var.sku_tier
  tags                = local.common_tags
}
