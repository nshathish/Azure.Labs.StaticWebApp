resource "azurerm_static_web_app" "this" {
  name                = var.name
  resource_group_name = var.resource_group_name
  location            = var.location
  sku_tier            = var.sku_tier

  # Pull-request preview environments require Standard SKU
  preview_environments_enabled = var.preview_environments_enabled

  tags = var.tags

  # azurerm 4.x has a known bug where it tries to GET app_settings during plan
  # on a resource that doesn't exist yet — ignoring prevents the spurious read error.
  lifecycle {
    ignore_changes = [app_settings]
  }
}
