import { updatePackageStatus } from '../apiCalls/packageApi'

export function usePackageStatus(packages, setPackages) {
  const changeStatus = async (trackingNumber, newStatus) => {
    if (!window.confirm(`Change status to ${newStatus}?`)) return

    try {
      const updated = await updatePackageStatus({ trackingNumber, status: newStatus })
      setPackages(packages.map(pkg => (pkg.trackingNumber === updated.trackingNumber ? updated : pkg)))
    } catch (err) {
      console.error('Failed to update status:', err)
    }
  }

  return { changeStatus }
}