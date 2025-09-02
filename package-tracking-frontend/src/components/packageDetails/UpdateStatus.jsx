import AvailableStatusDropdown from '../utils/AvailableStatusDropdown'

export default function UpdateStatus({ pkg, changeStatus }) {
  if (!pkg) return null

  const hasAvailableTransitions = pkg.availableStatusTransitions?.length > 0

  return (
    <section>
      <h2>Update Status</h2>
      {hasAvailableTransitions ? (
        <AvailableStatusDropdown
          trackingNumber={pkg.trackingNumber}
          availableStatusTransitions={pkg.availableStatusTransitions}
          onStatusChange={async (trackingNumber, newStatus) => {
            await changeStatus(newStatus);
          }}
        />
      ) : (
        <p>No available status transitions.</p>
      )}
    </section>
  )
}
