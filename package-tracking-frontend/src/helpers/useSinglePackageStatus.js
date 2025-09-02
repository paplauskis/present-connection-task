import { updatePackageStatus } from '../apiCalls/packageApi'

export function useSinglePackageStatus(pkg, setPkg) {
  const changeStatus = async (newStatus) => {
    if (!window.confirm(`Change status to "${newStatus}"?`)) return

    try {
      const updated = await updatePackageStatus({
        trackingNumber: pkg.trackingNumber,
        status: newStatus,
      })
      setPkg(updated)
      return updated
    } catch (err) {
      console.error('Failed to update status:', err)
      throw err
    }
  }

  return { changeStatus }
}
